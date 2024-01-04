using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;


public class GameAppManager : MonoBehaviour
{
    [Header("GameTile_Kor")]
    public string[] BrainName;
    public string[] DementiaName;
    public string[] LeapName;
    public string[] kinectName;

    [Header("GameTile_Eng")]
    public string[] Brain_gameName;
    public string[] Dementia_gameName;
 //   public string[] LeapMotion_SceneName;

    [Header("Touch")]
    //이름명, 생년월일, 기록날짜
    public List<int> rand_GameNumber; //게임 순서 번호 저장
    public List<string> QuestionStringList; //게임씬 이름 저장
    public int[] Game_Order;
    public int Brain_QusetionCount = 7; //전체 문제 종류
    public int Dementia_QusetionCount = 8; //전체 문제 종류


    [Header("Global")]
    public string GameKind; //현재 플레이 중인 게임의 종류 "Brain" " "Dementia" "Leap" "Kenect"
    public int CurrentQusetionNumber = 0; //문제 번호
    public int CurrentGameScore = 0; //플레이 중인 게임의 총점
    public bool playBool = false;
    [Space( 15f)]
    public SetUIGrup setUI;
    public List<string> GamePlayMedal;
    public List<float> GamePlayTime; //게임 플레이 타임 
    public List<int> GamePlayScore; //게임 플레이 점수 
    [Space(15f)]
    public bool play_touchGame = false;
    public List<Dictionary<string, object>> Save_CSVData;
    //[Space(15f)]
    //public string info_Name;
    //public string info_Birth;
    //public string info_PlayDate;
    //public string info_GameKind;
    //public string info_Score;
    public static GameAppManager instance { get; private set; }
    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Brain_gameName = new string[] { "BrainGame1", "BrainGame2", "BrainGame4", "BrainGame5", "BrainGame6", "BrainGame7", "BrainGame8" };
        Dementia_gameName = new string[] { "DementiaGame1", "DementiaGame2", "DementiaGame3", "DementiaGame4", "DementiaGame5", "DementiaGame6", "DementiaGame7", "DementiaGame8" };

        BrainName = new string[] { "표를 보고 단어 찾아 터치하기", "음식값 계산하기", "양손으로 다른 그림 그리기", "숫자판 순서대로 터치하기", "단어 색깔 맞추기", "반전 틀린 글자 찾기", "거울 시계 시간 맞추기" };
        DementiaName = new string[] { "기억회상 낱말 찾기", "속담과 그림 연결하기", "사물과 글자 연결하기", "분수와 그림 연결하기", "숨은 그림 찾기", "틀린 그림 찾기", "미로 찾기", "연속 숫자 계산하기" };
        //튜토리얼 진행 여부
        if (PlayerPrefs.HasKey("TutorialPlay")) { PlayerPrefs.GetString("TutorialPlay"); }
        else { PlayerPrefs.SetString("TutorialPlay", "true"); }
    }

    private void Start()
    {
      //  UserDataInit();
    }

    public void GameLoadScene()
    {
        switch (GameKind)
        {
            case "Brain":
                TouchGame_LoadScene();
                break;
            case "Dementia":
                TouchGame_LoadScene();
                break;
        }

    }

    public List<T> GetShuffleList<T>(List<T> _list)
    {
        for (int i = _list.Count - 1; i > 0; i--)
        {
            int rnd = UnityEngine.Random.Range(0, i);

            T temp = _list[i];
            _list[i] = _list[rnd];
            _list[rnd] = temp;
        }
        return _list;
    }

    void TouchGame_LoadScene()
    {
        if (CurrentQusetionNumber < QuestionStringList.Count)
        {
            if (CurrentQusetionNumber.Equals(0))
            {
                LoadingSceneManager.LoadScene(QuestionStringList[CurrentQusetionNumber]);
                //SceneManager.LoadScene(QuestionStringList[CurrentQusetionNumber]);
            }
            
            else
            {
                if (SceneManager.GetActiveScene().name.Equals("8.BreakTime"))
                    SceneManager.LoadScene(QuestionStringList[CurrentQusetionNumber]);
                else
                    SceneManager.LoadScene("8.BreakTime");
            }
        }
        else
        {
            SceneManager.LoadScene("9.GameFinish");
        }
    }
    public void Initialization()
    {
        GameKind = ""; //현재 플레이 중인 게임의 종류 "Brain" " "Dementia" "Leap" "Kenect"
        CurrentQusetionNumber = 0; //문제 번호
        CurrentGameScore = 0; //플레이 중인 게임의 총점
        playBool = false;
        rand_GameNumber.Clear();
        QuestionStringList.Clear();
        GamePlayTime.Clear();
        GamePlayScore.Clear();
        CurrentGameScore = 0;

    }

    public void LoadScene_Name(string name)
    {
        SceneManager.LoadScene(name);
    }

}
