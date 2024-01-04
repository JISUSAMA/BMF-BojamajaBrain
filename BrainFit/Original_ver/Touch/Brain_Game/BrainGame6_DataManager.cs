using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame6_DataManager : MonoBehaviour
{
    public BrainGame6_UIManager UIManager;
    public Touch_TimerManager TimeManager_sc;
    //빨강,노랑,주황,초록,파랑,남색,보라, //회색, 검정, 갈색, 하늘색
    public List<string> Random_Color_Lsit;
    public List<string> Correct_Color_Lsit;
    [SerializeField] List<string> Worst_Color_Lsit;

    [SerializeField] List<Dictionary<string, string>> color_data;
    string[] ColorData_Head;

    public static BrainGame6_DataManager instance { get; private set; }
    /*
       빨강, 파랑, 노랑, 초록, 검정 이 다섯 개의 단어가 5*5 판 속에 무작위로 배열되어 있다.
       이 중에서 글자와 색깔이 맞지 않는 5개의 단어를 찾아 터치하면 완료되는 게임이다.
       1) 제대로 된 단어를 잘못 터치하면 실패
       2) 시간 내 완료하지 못하면 실패
    */
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        color_data = CSVReader.Read("ColorList"); //낱말찾기
        ColorData_Head = new string[] { "Color", "Code" };
    }
    private void Start()
    {
        StopCoroutine(_GameStart());
        StartCoroutine(_GameStart());
    }
    IEnumerator _GameStart()
    {
        Initialization();
        MakeRandomColors();
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        TimeManager_sc.FindWord_sec_Timer(25, 10, 5);
        while (UIManager.Find_Correct_i < 5)
        {
            if (!GameAppManager.instance.playBool) { break; }
            yield return null;
        }
        if (UIManager.Find_Correct_i == 5)
        {
            TimeManager_sc.Question_Success();
        }
        //게임끝
        yield return null;
    }
    void Initialization()
    {
        for(int i=0; i<UIManager.QuestionGrup_Btn.Length; i++) { UIManager.QuestionGrup_Btn[i].interactable = true; }
     
        Random_Color_Lsit.Clear(); //랜덤으로 글자 또는 수를 다시 넣어줌
        Correct_Color_Lsit.Clear();
        Worst_Color_Lsit.Clear();
    }

    public void MakeRandomColors()
    {
        int random = Random.Range(0, color_data.Count);
        int random2 = Random.Range(0, color_data.Count);
        string ramdom_str = color_data[random]["Color"] + "," + color_data[random]["Code"];
        string ramdom_str2 = color_data[random]["Color"] + "," + color_data[random2]["Code"];
        while (Correct_Color_Lsit.Count < 5)
        {
            if (Correct_Color_Lsit.Count != 0)
            {
                if (!Correct_Color_Lsit.Contains(ramdom_str))
                {
                    Correct_Color_Lsit.Add(color_data[random]["Color"] + "," + color_data[random]["Code"]);
                }
            }
            else
            {
                Correct_Color_Lsit.Add(color_data[random]["Color"] + "," + color_data[random]["Code"]);
            }
            random = Random.Range(0, color_data.Count);
            ramdom_str = color_data[random]["Color"] + "," + color_data[random]["Code"];
        }
        while (Worst_Color_Lsit.Count < 20)
        {
            random = Random.Range(0, color_data.Count);
            random2 = Random.Range(0, color_data.Count);
            if (random != random2)
            {
                ramdom_str2 = color_data[random]["Color"] + "," + color_data[random2]["Code"];
                if (Worst_Color_Lsit.Count != 0)
                {
                    if (!Correct_Color_Lsit.Contains(ramdom_str2))
                    {
                        Worst_Color_Lsit.Add(color_data[random]["Color"] + "," + color_data[random2]["Code"]);
                    }
                }
                else
                {
                    Worst_Color_Lsit.Add(color_data[random]["Color"] + "," + color_data[random2]["Code"]);
                }
            }
        }

        //리스트에 올바른 목록 담기
        for (int i = 0; i < 5; i++) Random_Color_Lsit.Add(Correct_Color_Lsit[i]);
        //리스트에 틀린 목록 담기
        for (int i = 0; i < 20; i++) Random_Color_Lsit.Add(Worst_Color_Lsit[i]);
        //랜덤으로 재배열
        GameAppManager.instance.GetShuffleList(Random_Color_Lsit);
        for (int i = 0; i < Random_Color_Lsit.Count; i++) UIManager.ColorData(i);
    }

}
