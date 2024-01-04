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

    //public bool playBool = false;
    //public float AnswerTimer_f; //15초
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
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        BrainGame6_Play();
        yield return null;
    }
    void Initialization()
    {
        for(int i=0; i<UIManager.QuestionGrup_Btn.Length; i++) { UIManager.QuestionGrup_Btn[i].interactable = true; }
     
        Random_Color_Lsit.Clear(); //랜덤으로 글자 또는 수를 다시 넣어줌
        Correct_Color_Lsit.Clear();
        Worst_Color_Lsit.Clear();
    }

    public void BrainGame6_Play()
    {
        StopCoroutine("_BrainGame6_Play");
        StartCoroutine("_BrainGame6_Play");
    }
    IEnumerator _BrainGame6_Play()
    {
        Initialization();
        MakeRandomColors();
        TimeManager_sc.FindWord_sec_Timer(25,10,5);
        while (UIManager.Find_Correct_i < 4)
        {
            if (!GameAppManager.instance.playBool) { break; }
            yield return null;
        }
        if (UIManager.Find_Correct_i == 4)
        {
            TimeManager_sc.Question_Success();
        }
        //게임끝
        yield return null;
    }
    public void MakeRandomColors()
    {
        int random = Random.Range(0, color_data.Count - 1);
        int random2 = Random.Range(0, color_data.Count - 1);
        string ramdom_str = color_data[random]["Color"] + "," + color_data[random]["Code"];
        string ramdom_str2 = color_data[random]["Color"] + "," + color_data[random2]["Code"];
        while (Correct_Color_Lsit.Count < 4)
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
            random = Random.Range(0, color_data.Count - 1);
            ramdom_str = color_data[random]["Color"] + "," + color_data[random]["Code"];
        }
        while (Worst_Color_Lsit.Count < 21)
        {
            random = Random.Range(0, color_data.Count - 1);
            random2 = Random.Range(0, color_data.Count - 1);
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
        for(int i =0; i<4; i++) Random_Color_Lsit.Add(Correct_Color_Lsit[i]);
        //리스트에 틀린 목록 담기
        for (int i = 0; i < 21; i++) Random_Color_Lsit.Add(Worst_Color_Lsit[i]);
        //랜덤으로 재배열
        GameAppManager.instance.GetShuffleList(Random_Color_Lsit);
        for (int i = 0; i < Random_Color_Lsit.Count; i++) UIManager.ColorData(i);

    }
    //public void Question_Success()
    //{
    //    StopCoroutine("_Question_Success");
    //    StartCoroutine("_Question_Success");
    //}
    //IEnumerator _Question_Success()
    //{
    //    UIManager.AnswerOb.SetActive(true);
    //    UIManager.AnswerImg.sprite = UIManager.AnswerImg_sprite[0];
    //    playBool = false;
    //    yield return new WaitForSeconds(1.5f);
    //    UIManager.AnswerOb.SetActive(false);
    //    Touch_GameManager.instance.CurrentQusetionNumber += 1;
    //    Touch_GameManager.instance.Game_LoadScene();
    //    yield return null;
    //}
    //public void Question_Fail()
    //{
    //    StopCoroutine("_Question_Fail");
    //    StartCoroutine("_Question_Fail");
    //}
    //IEnumerator _Question_Fail()
    //{
    //    UIManager.AnswerOb.SetActive(true);
    //    UIManager.AnswerImg.sprite = UIManager.AnswerImg_sprite[1];
    //    playBool = false;
    //    yield return new WaitForSeconds(1.5f);
    //    UIManager.AnswerOb.SetActive(false);
    //    Touch_GameManager.instance.CurrentQusetionNumber += 1;
    //    Touch_GameManager.instance.Game_LoadScene();
    //    yield return null;
    //}
    //public void FindWord_sec_Timer(float time)
    //{
    //    AnswerTimer_f = time;
    //    playBool = true;
    //    StopCoroutine("_FindWord_sec_Timer");
    //    StartCoroutine("_FindWord_sec_Timer");
    //}
    //IEnumerator _FindWord_sec_Timer()
    //{
    //    UIManager.AnswerTimer_slider.maxValue = AnswerTimer_f;
    //    UIManager.AnswerTimer_slider.value = AnswerTimer_f;
    //    while (playBool)
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //        while (AnswerTimer_f > 0)
    //        {
    //            AnswerTimer_f -= Time.deltaTime;
    //            UIManager.AnswerTimer_slider.value = AnswerTimer_f;
    //            UIManager.AnswerTimer_text.text = AnswerTimer_f.ToString("N0") + " 초";
    //            if (!playBool) break;
    //            yield return null;
    //        }
    //        if (AnswerTimer_f <= 0)
    //        {
    //            AnswerTimer_f = 0;
    //            UIManager.AnswerTimer_text.text = " ";
    //            Question_Fail();
    //        }
    //    }

    //    //점수 Set
    //    if (AnswerTimer_f > 10) { Touch_GameManager.instance.CurrentGameScore = 20; }
    //    else if (AnswerTimer_f <= 10 && AnswerTimer_f > 5) { Touch_GameManager.instance.CurrentGameScore = 10; }
    //    else if (AnswerTimer_f <= 5 && AnswerTimer_f > 0) { Touch_GameManager.instance.CurrentGameScore = 5; }
    //    yield return null;
    //}
}
