using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance { get; private set; }

    public int quizCount;   // 문제 갯수
    public int sec_per_quizCount;   // 제한 시간
    public int quiz_score;
    public UnityEvent onReady = null;   // 준비 할 때, 이벤트
    public UnityEvent onStart = null;   // 시작 할 때, 이벤트
    public UnityEvent onEnd = null;     // 끝났을 때, 이벤트
    public UnityAction<int, float> onTimerStart = null;

    //public TMP_Text countText;
    public SetUIGrup setUIGrup;
    public bool gameRunning { get; set; }
    public int gameMode = 0;

    enum GameMode
    {
        WINGAME,
        LOSEGAME,
        ARITHMETIC_NUMBERS,
        ARITHMETIC_PICTURE,
        BLUE_FLAG_AND_WHITE_FLAG
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;

        if (SceneManager.GetActiveScene().name == "1_ROCK_PAPER_SCISSORS_WIN")
        {
            Debug.Log("WINGAME");
            gameMode = (int)GameMode.WINGAME;
        }
        else if (SceneManager.GetActiveScene().name == "2_ROCK_PAPER_SCISSORS_LOSE")
        {
            Debug.Log("LOSEGAME");
            gameMode = (int)GameMode.LOSEGAME;
        }
        else if (SceneManager.GetActiveScene().name == "3_ARITHMETIC_GAME_NUMBERS")
        {
            Debug.Log("ARITHMETIC_NUMBERS");
            gameMode = (int)GameMode.ARITHMETIC_NUMBERS;
        }
        else if (SceneManager.GetActiveScene().name == "4_ARITHMETIC_GAME_HANDPICTURE")
        {
            Debug.Log("ARITHMETIC_GAME_HANDPICTURE");
            gameMode = (int)GameMode.ARITHMETIC_PICTURE;
        }
        else if (SceneManager.GetActiveScene().name == "5_BLUE_FLAG_AND_WHITE_FLAG")
        {
            Debug.Log("BLUE_FLAG_AND_WHITE_FLAG");
            gameMode = (int)GameMode.BLUE_FLAG_AND_WHITE_FLAG;
        }

        gameRunning = false;

        //현재 문제의 번호 UI Setting
        setUIGrup.QuestionNum_text.text = ((GameAppManager.instance.CurrentQusetionNumber + 1).ToString()) + "/" + "5";
        setUIGrup.QuestionNum_Slider.maxValue = 5;
        setUIGrup.QuestionNum_Slider.value = GameAppManager.instance.CurrentQusetionNumber + 1;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnRoundStart()
    {
        Debug.Log("시작!");
        StartCoroutine(_OnRoundStart());
    }

    IEnumerator _OnRoundStart()
    {
        //현재 진행중인 게임의 점수
        quiz_score = 0;
        // float leftTime = 3f;
        // countText.text = "";
        // WaitForSeconds ws = new WaitForSeconds(1f);
        //
        // // 카운트를 이미지 교체로 바꾸기
        // if (onReady != null) { onReady.Invoke(); }  // 문제 생성
        //
        // // 시작 전 준비하기
        // while (leftTime > 0f)
        // {
        //     // 3, 2, 1
        //     countText.text = leftTime.ToString();
        //     yield return ws;    // 1초
        //     leftTime -= 1;
        // }
        // countText.text = "";
        
        // 카운트를 이미지 교체로 바꾸기
         if (onReady != null) { onReady.Invoke(); }  // 문제 생성
        if (onStart != null) { onStart.Invoke();}  // 3초 타이머 시작 : 총 15초, 2초 타이머 시작 : 총 10초

        gameRunning = true;

        onTimerStart.Invoke(quizCount, sec_per_quizCount);   // 문제 갯수, 문제당 제한시간

        yield return null;
    }

    public void OnRoundEnd()
    {
        Debug.Log("끝!");
        StartCoroutine(_OnRoundEnd());

    }

    IEnumerator _OnRoundEnd()
    {
        // 끝낼 때 데이터 저장 및 관리
        gameRunning = false;

        if (onEnd != null) { onEnd.Invoke(); }  // 결과화면이든 뭐든 끝나고나서 생성

        yield return null;
    }
    public void OnClick_StartBtn()
    {
        if (SceneManager.GetActiveScene().name.Equals("1_ROCK_PAPER_SCISSORS_WIN")
           || SceneManager.GetActiveScene().name.Equals("2_ROCK_PAPER_SCISSORS_LOSE"))
        {
            string t = GestureManager.Instance.GetSelectedHand();
            if (t == "선택되지않았음")
            {
                // 손을 선택해 주세요. 프린트
            }
            else
            {
                OnRoundStart();
            }
        }
        else
        {
            OnRoundStart();
        }
    }
}
