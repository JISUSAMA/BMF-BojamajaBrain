using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame1_DataManager : MonoBehaviour
{

    public DementiaGame1_UIManager UIManager;
    [SerializeField] int word_randNum_list; //엑셀안에 있는 문제번호
    public List<string> Question_word_list; //정답 4글자

    [SerializeField] List<Dictionary<string, string>> data;
    List<Dictionary<int, string>> number;
    [SerializeField] List<string> AnswerString_list;
    public int SubmitAnswerNum_i = 0;
    public int blank1 = -1, blank2 = -1; //빈칸의 위치 

    public bool playBool = false;
    public float AnswerTimer_f; //15초

    public static DementiaGame1_DataManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        data = CSVReader.Read("FourWordList"); //낱말찾기
    }
    private void Start()
    {
        StopCoroutine(_GameStart());
        StartCoroutine(_GameStart());

    }
    IEnumerator _GameStart()
    {
        Set_Question_Data();
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        SetUIGrup.instance.TimeToScore(25);
        yield return null;
    }

    //엑셀안의 단어 중에서 20가지를 골라서 리스트안에 넣어줌
   public void Set_Question_Data()
    {
        int rand = Random.Range(0, data.Count-1);
        Debug.Log("data Num : " + data.Count);
        Debug.Log("rand Num : " + rand);
  
        word_randNum_list = rand; //출제하는 문제의 번호를 랜덤으로 받음
 
        //rand(문제 번호)/submitAnswerNum은 보기 정답의 번호/Blank1=1 or 2 /Blank2 = 2 or 3/ AnswerString 보기 안에들어가는 string  
        if (rand.Equals(0)) { SubmitAnswerNum_i = 2; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "이,상", "중,구", "구,벽", "이,로" }; }
        else if (rand.Equals(1)) { SubmitAnswerNum_i = 3; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "중,생", "명,려", "산,해", "재,치" }; }
        else if (rand.Equals(2)) { SubmitAnswerNum_i = 1; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "전,결", "기,락", "명,영", "이,자" }; }
        else if (rand.Equals(3)) { SubmitAnswerNum_i = 4; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "대,리", "구,오", "차,로", "문,답" }; }
        else if (rand.Equals(4)) { SubmitAnswerNum_i = 2; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "이,가", "십,계", "삼,육", "칠,오" }; }
        else if (rand.Equals(5)) { SubmitAnswerNum_i = 4; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "치,박", "계,명", "이,로", "하,동" }; }
        else if (rand.Equals(6)) { SubmitAnswerNum_i = 1; blank1 = 0; blank2 = 2; AnswerString_list = new List<string> { "구,일", "박,중", "개,명", "인,인" }; }
        else if (rand.Equals(7)) { SubmitAnswerNum_i = 2; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "급,일", "성,가", "진,래", "집,사" }; }
        else if (rand.Equals(8)) { SubmitAnswerNum_i = 3; blank1 = 0; blank2 = 2; AnswerString_list = new List<string> { "이,전", "잔,구", "작,삼", "이,이" }; }
        else if (rand.Equals(9)) { SubmitAnswerNum_i = 1; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "산,수", "춘,치", "치,랑", "이,구" }; }
        else if (rand.Equals(10)) { SubmitAnswerNum_i = 4; blank1 = 0; blank2 = 2; AnswerString_list = new List<string> { "사,사", "미,필", "치,초", "팔,미" }; }
        else if (rand.Equals(11)) { SubmitAnswerNum_i = 2; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "라,육", "거,득", "이,파", "필,계" }; }
        else if (rand.Equals(12)) { SubmitAnswerNum_i = 3; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "명,치", "구,판", "로,생", "이,패" }; }
        else if (rand.Equals(13)) { SubmitAnswerNum_i = 1; blank1 = 0; blank2 = 2; AnswerString_list = new List<string> { "동,서", "명,명", "유,구", "삼,사" }; }
        else if (rand.Equals(14)) { SubmitAnswerNum_i = 2; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "일,초", "발,중", "명,계", "숙,사" }; }
        else if (rand.Equals(15)) { SubmitAnswerNum_i = 4; blank1 = 0; blank2 = 2; AnswerString_list = new List<string> { "작,고", "명,출", "감,고", "신,불" }; }
        else if (rand.Equals(16)) { SubmitAnswerNum_i = 1; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "심,체", "이,락", "명,추", "감,초" }; }
        else if (rand.Equals(17)) { SubmitAnswerNum_i = 2; blank1 = 0; blank2 = 2; AnswerString_list = new List<string> { "이,명", "일,정", "자,유", "국,평" }; }
        else if (rand.Equals(18)) { SubmitAnswerNum_i = 3; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "치,타", "이,명", "고,락", "구,초" }; }
        else if (rand.Equals(19)) { SubmitAnswerNum_i = 4; blank1 = 1; blank2 = 3; AnswerString_list = new List<string> { "락,명", "계,삼", "치,초", "석,조" }; }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        //// QuestionList(정답)에 4개의 단어 한자리 씩 담아줌 
        for (int i = 0; i < 4; i++)
            Question_word_list.Add(data[word_randNum_list][(i + 1) + "word"]);
        //// QuestionList(정답)에 텍스트 한자리 씩 뿌려줌
        for (int i = 0; i < UIManager.Word_Text.Length; i++)
            UIManager.Word_Text[i].text = Question_word_list[i];

        //// 빈칸 초기화
        for (int i = 0; i < UIManager.Word_Blank.Length; i++)
            UIManager.Word_Blank[i].SetActive(false);

        ////문제에 따른 Word_Blank의 위치에 맞춰서 블랭크 이미지를 활성화 시켜줌
        UIManager.Word_Blank[blank1].SetActive(true);
        UIManager.Word_Blank[blank2].SetActive(true);

        ////보기안에 텍스트 넣어줌
        for (int j = 0; j < 4; j++)
            UIManager.CheckAnswer_text[j].text = AnswerString_list[j];
        //  TimerManager_sc.FindWord_sec_Timer(8,5,2);
      
    }

 
}
