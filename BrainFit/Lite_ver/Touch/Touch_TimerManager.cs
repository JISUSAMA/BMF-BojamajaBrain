using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Touch_TimerManager : MonoBehaviour
{
    public float AnswerTimer_f; //15초
    [Header("정답 이미지")]
    public GameObject AnswerOb;
    public Image AnswerImg;
    [Header("타이머")]
    public Sprite[] AnswerImg_sprite;
    public Slider AnswerTimer_slider; //타이머
    public Text AnswerTimer_text;
    [Header("단계")]
    public Text QuestNumText;
    public Slider QuestSlider;

    int thisGameScore;
    //걸린 시간에 따른 현재 게임의 점수와 시간
    float startT, score1, score2; //기준 시간
    int get_score1, get_score2, get_score3; //시간에 따른 점수 
    public void FindWord_sec_Timer(float time, float s1, float s2)
    {
        GameAppManager.instance.playBool = true;
        Debug.Log(GameAppManager.instance.GameKind);
        //현재 문제의 번호 UI Setting
        if (GameAppManager.instance.GameKind.Equals("Brain"))
        {
            QuestNumText.text = (GameAppManager.instance.CurrentQusetionNumber + 1).ToString() + "/7";
            QuestSlider.value = GameAppManager.instance.CurrentQusetionNumber + 1;
        }
        else if (GameAppManager.instance.GameKind.Equals("Dementia"))
        {
            QuestNumText.text = (GameAppManager.instance.CurrentQusetionNumber + 1).ToString() + "/8";
            QuestSlider.value = GameAppManager.instance.CurrentQusetionNumber + 1;
        }

        AnswerTimer_f = time;
        startT = time;
        score1 = s1;
        score2 = s2;
        StopCoroutine("_FindWord_sec_Timer");
        StartCoroutine("_FindWord_sec_Timer");
    }
    IEnumerator _FindWord_sec_Timer()
    {
        thisGameScore = 0;
        AnswerTimer_slider.maxValue = AnswerTimer_f;
        AnswerTimer_slider.value = AnswerTimer_f;
        while (GameAppManager.instance.playBool)
        {
            yield return new WaitForSeconds(0.5f);
            while (AnswerTimer_f > 0)
            {
                AnswerTimer_f -= Time.deltaTime;
                AnswerTimer_slider.value = AnswerTimer_f;
                AnswerTimer_text.text = AnswerTimer_f.ToString("N0");
                if (!GameAppManager.instance.playBool) break;
                yield return null;
            }
            if (AnswerTimer_f <= 0)
            {
                AnswerTimer_f = 0;
                AnswerTimer_text.text = " ";
                Question_Fail();
            }
        }
      
        yield return null;
    }
    public void Question_Success()
    {
        ScoreSet();
        StopCoroutine("_Question_Success");
        StartCoroutine("_Question_Success");

    }
    public void ScoreSet()
    {
        //게임의 종류에 따라서 점수 달라짐
        if (GameAppManager.instance.GameKind.Equals("Brain"))
        {
            //14*5 + 15*2 =100점
            if (SceneManager.GetActiveScene().name.Equals("BrainGame2")
                ||SceneManager.GetActiveScene().name.Equals("BrainGame5"))
            {
                get_score1 = 15;
                get_score2 = 11;
                get_score3 = 9;
            }
            else
            {
                get_score1 = 14;
                get_score2 = 10;
                get_score3 = 8;
            }
        }
        else if (GameAppManager.instance.GameKind.Equals("Dementia"))
        {
            //12*3 + 13*5 =100점
            if (SceneManager.GetActiveScene().name.Equals("DementiaGame1")
                || SceneManager.GetActiveScene().name.Equals("DementiaGame5")
                || SceneManager.GetActiveScene().name.Equals("DementiaGame6"))
            {
                get_score1 = 12;
                get_score2 = 9;
                get_score3 = 6;
            }
            else
            {
                get_score1 = 13;
                get_score2 = 10;
                get_score3 = 8;
            }
        }
      
        //점수 Set
        if (AnswerTimer_f > score1) { GameAppManager.instance.CurrentGameScore += get_score1; thisGameScore = get_score1; Debug.Log("get_score1; " + thisGameScore); }
        else if (AnswerTimer_f <= score1 && AnswerTimer_f > score2) { GameAppManager.instance.CurrentGameScore += get_score2; thisGameScore = get_score2; Debug.Log("get_score2; " + thisGameScore); }
        else if (AnswerTimer_f <= score2 && AnswerTimer_f > 0) { GameAppManager.instance.CurrentGameScore += get_score3; thisGameScore = get_score3; Debug.Log("get_score3; " + thisGameScore); }
      
    }
    IEnumerator _Question_Success()
    {
        Debug.Log("thisGamsScore; " + thisGameScore);
        SceneSoundCtrl.Instance.GameSuccessSound();
        PlayerPrefs.SetString("GameClearState", "true");
        GameAppManager.instance.GamePlayTime.Add(startT - AnswerTimer_f);
        GameAppManager.instance.GamePlayScore.Add(thisGameScore);
        AnswerOb.SetActive(true);
        AnswerImg.sprite = AnswerImg_sprite[0];
        GameAppManager.instance.playBool = false;
        yield return new WaitForSeconds(1.5f);
        AnswerOb.SetActive(false);
        GameAppManager.instance.CurrentQusetionNumber += 1;
        GameAppManager.instance.GameLoadScene();
        yield return null;
    }
    public void Question_Fail()
    {
        StopCoroutine("_Question_Fail");
        StartCoroutine("_Question_Fail");
    }
    IEnumerator _Question_Fail()
    {
        SceneSoundCtrl.Instance.GameFailSound();
        PlayerPrefs.SetString("GameClearState", "false");
        GameAppManager.instance.GamePlayTime.Add(AnswerTimer_f);
        GameAppManager.instance.GamePlayScore.Add(thisGameScore);
        AnswerOb.SetActive(true);
        AnswerImg.sprite = AnswerImg_sprite[1];
        GameAppManager.instance.playBool = false;
        yield return new WaitForSeconds(1.5f);
        AnswerOb.SetActive(false);
        GameAppManager.instance.CurrentQusetionNumber += 1;
        GameAppManager.instance.GameLoadScene();
        yield return null;
    }
}
