using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text countText;
    public static int gameCount;
    public static float playTime;
    public static int fingercount;
    void Start()
    {
        // 함수 등록
        AppManager.Instance.onTimerStart += TimerStart;
        fingercount = 0;

    }

    private void OnDisable()
    {
        // 함수 제거
        AppManager.Instance.onTimerStart -= TimerStart;
    }

    public void TimerStart(int a_quizCount, float a_time)
    {
        //Debug.Log("Timer Start!");
        StartCoroutine(_TimerStart(a_quizCount, a_time));
    }

    IEnumerator _TimerStart(int a_quizCount, float a_time)
    {
        // 3, 2, 1 .... > 문제당 제한시간
        float m_time = a_time;
        AppManager.Instance.setUIGrup.AnswerTimeSlider.maxValue = a_time; //현재 문제 정답 제출 제한 시간
        for (int i = 0; i < a_quizCount; i++)
        {
            gameCount = i;  // UIManager 전달용 : ex) 0 ~ 4

            while (m_time > 0)
            {
                m_time -= Time.deltaTime;
                playTime = m_time;
                //남은 제한 시간 표시
                AppManager.Instance.setUIGrup.AnswerTimeSlider.value = m_time;
                AppManager.Instance.setUIGrup.AnswerTime_text.text = m_time.ToString("N0");

                //countText.text = m_time.ToString("N2");
                // 원하는 제한시간때 마다 출력
                yield return null;
            }
            if (m_time < 0)
            {
             //   QuizManager.Instance.Question_Finish(1); 
            }
            // 여기서 승패 결정
            if (AppManager.Instance.gameMode == 0)  // 이기기
            {
                QuizManager.Instance.WinGame(GestureManager.Instance.GameCount = i + 1, GestureManager.Instance.RPS);
            }
            else if (AppManager.Instance.gameMode == 1)  // 지기
            {
                QuizManager.Instance.LoseGame(GestureManager.Instance.GameCount = i + 1, GestureManager.Instance.RPS);
            }
            else if (AppManager.Instance.gameMode == 2)  // 사칙연산 숫자
            {
                GetFingerCount();
                QuizManager.Instance.Arithmetic_Game_Numbers(GestureManager_Arithmetic.Instance.GameCount = i + 1, fingercount);
                fingercount = 0;
            }
            else if (AppManager.Instance.gameMode == 3)  // 사칙연산 손그림
            {
                GetFingerCount();
                QuizManager.Instance.Arithmetic_Game_HandPicture(GestureManager_Arithmetic.Instance.GameCount = i + 1, fingercount);
                fingercount = 0;
            }
            else if (AppManager.Instance.gameMode == 4) // 청기백기
            {
                QuizManager.Instance.Answer_BlueFlagWhiteFlag(GestureManager_Flag.Instance.GameCount = i + 1);
                QuizManager.Instance.BLeft = "BC";
                QuizManager.Instance.WRight = "WC";
            }

            yield return new WaitUntil(() => AppManager.Instance.setUIGrup.Check == false);
            m_time = a_time;
        }


        gameCount = a_quizCount;    // UIManager while 통과용 : ex ) 5
       // countText.text = "";
        GameAppManager.instance.CurrentQusetionNumber += 1;

        //true: 성공 //false: 실패  
        if (!AppManager.Instance.quiz_score.Equals(0))
            PlayerPrefs.SetString("GameClearState", "true");
        else PlayerPrefs.SetString("GameClearState", "false");

        GameAppManager.instance.GamePlayScore.Add(AppManager.Instance.quiz_score);
        GameAppManager.instance.GameLoadScene();
        yield return null;
    }

    public void GetFingerCount()
    {
        StartCoroutine(_GetFingerCount());
    }

    IEnumerator _GetFingerCount()
    {
        foreach (var l_fingerCount in GestureManager_Arithmetic.Instance.L_isExtendedFinger)
        {
            if (l_fingerCount)
            {
                fingercount++;
            }
        }

        foreach (var r_fingerCount in GestureManager_Arithmetic.Instance.R_isExtendedFinger)
        {
            if (r_fingerCount)
            {
                fingercount++;
            }
        }

        Debug.Log("fingercount: " + fingercount);

        yield return null;
    }
}
