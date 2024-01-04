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
    public string[] LeapMotion_SceneName;

    [Header("Touch")]
    //이름명, 생년월일, 기록날짜
    public List<int> rand_GameNumber; //게임 순서 번호 저장
    public List<string> QuestionStringList; //게임씬 이름 저장
    public int[] Game_Order;
    public int Brain_QusetionCount = 7; //전체 문제 종류
    public int Dementia_QusetionCount = 8; //전체 문제 종류

    [Header("Leap Motion")]
    public List<string> LeapMotion_PlayList;
    public List<int> LeapMotion_PlayNumList;

    [Header("Global")]
    public string GameKind; //현재 플레이 중인 게임의 종류 "Brain" " "Dementia" "Leap" "Kenect"
    public int CurrentQusetionNumber = 0; //문제 번호
    public int CurrentGameScore = 0; //플레이 중인 게임의 총점
    public bool playBool = false;

    public SetUIGrup setUI;
    public List<float> GamePlayTime; //게임 플레이 타임 
    public List<int> GamePlayScore; //게임 플레이 점수 

    public bool play_touchGame = false;
    public List<Dictionary<string, object>> Save_CSVData;

    public string info_Name;
    public string info_Birth;
    public string info_PlayDate;
    public string info_GameKind;
    public string info_Score;
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
        LeapMotion_SceneName = new string[] { "1_ROCK_PAPER_SCISSORS_WIN", "2_ROCK_PAPER_SCISSORS_LOSE", "3_ARITHMETIC_GAME_NUMBERS", "4_ARITHMETIC_GAME_HANDPICTURE", "5_BLUE_FLAG_AND_WHITE_FLAG" };
        BrainName = new string[] { "Words Search", "Guess Food Price", "Draw 2 different shapes", "Ordering numbers", "Match Colors And Words", "Find Wrong Words", "Mirror Image Of Clock" };
        DementiaName = new string[] { "Fill in the blank with the words", "Find the Correct Shadow", "Match objects and words", "Match Fractions", "Seek and Find", "Find 5 differences", "Maze", "Calculate numbers" };
        LeapName = new string[] { "Win at Rock, Paper, Scissors", "Lose at Rock, Paper, Scissors", "Calculate the numbers Ⅰ", "Calculate the numbers Ⅱ", "Blue down, White down" };
    }

    private void Start()
    {
        UserDataInit();
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
            case "Real":
                if (CurrentQusetionNumber < LeapMotion_PlayList.Count)
                {
                    if (CurrentQusetionNumber.Equals(0))
                        LoadingSceneManager.LoadScene(LeapMotion_PlayList[CurrentQusetionNumber]);
                    //SceneManager.LoadScene(LeapMotion_PlayList[CurrentQusetionNumber]);
                    else
                    {
                        if (SceneManager.GetActiveScene().name.Equals("8.BreakTime"))
                            SceneManager.LoadScene(LeapMotion_PlayList[CurrentQusetionNumber]);
                        else
                            SceneManager.LoadScene("8.BreakTime");
                    }
                }
                else
                {
                    SceneManager.LoadScene("9.GameFinish");
                }
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
            //끝!
            Finish_SaveGameData();
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
        //
        LeapMotion_PlayList.Clear();
        LeapMotion_PlayNumList.Clear();
    }

    public void LoadScene_Name(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void UserDataInit()
    {
        info_Name = PlayerPrefs.GetString("EP_UserNAME"); //플레이어 이름
        info_Birth = PlayerPrefs.GetString("EP_UserBrithDay"); //플레이어 생일

        Save_CSVData = CSVReader.Read_file("SaveDataList");

        for (int i = 0; i < Save_CSVData.Count; i++)
        {
            //현재 사용하고있는 유저와 이름과 생일이 같은 데이터의 자료를 가져옴
            if (Save_CSVData[i]["Name"].Equals(PlayerPrefs.GetString("EP_UserNAME"))
                && Save_CSVData[i]["Birth"].Equals(PlayerPrefs.GetString("EP_UserNAME")))
            {

            }
        }
    }

    public void Finish_SaveGameData()
    {
        info_Name = PlayerPrefs.GetString("EP_UserNAME"); //플레이어 이름
        info_Birth = PlayerPrefs.GetString("EP_UserBrithDay"); //플레이어 생일

        //오늘 날짜
        DateTime today = DateTime.Now;
        info_PlayDate = today.ToString("MM/dd/yyyy HH:mm");

        info_GameKind = GameKind;  //게임 플레이 종류
        info_Score = CurrentGameScore.ToString();

        Set_SaveDataFile(info_Name, info_Birth, info_PlayDate, info_GameKind, info_Score);
    }
    public void Set_SaveDataFile(string name, string birth, string playdate, string gamekind, string total_score)
    {
        string path = Application.persistentDataPath + "/SaveDataList.csv";
        Debug.Log(path);
        // This text is added only once to the file.
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                string n = "Name,Birth,PlayDate,GameKind,Score";
                sw.WriteLine(n);
            }
        }
        else
        {
            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                string n = info_Name + "," + info_Birth + "," + info_PlayDate + "," + info_GameKind + "," + info_Score;
                sw.WriteLine(n);
            }
        }
        // Open the file to read from.
        using (StreamReader sr = File.OpenText(path))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                Debug.Log(line);
            }
        }
    }
}
