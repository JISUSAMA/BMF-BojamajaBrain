using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame1_DataManager : MonoBehaviour
{
    public Touch_TimerManager TimerManager_sc;
    public DementiaGame1_UIManager UIManager;
    [SerializeField] int word_randNum_list; //엑셀안에 있는 문제번호
  //  public List<string> Question_word_list; //정답 4글자

//    [SerializeField] List<Dictionary<string, string>> data;
//    List<Dictionary<int, string>> number;
//    [SerializeField] List<string> AnswerString_list;

    public int blank1 = -1, blank2 = -1; //빈칸의 위치 

    public bool playBool = false;
    public float AnswerTimer_f; //15초
    public string[] Quiz_answer = { "CCHI", "NI", "ZU", "UCC", "PO", "TO", "TAT", "OTA", "USH", "OO", "HRO", "SHR", "PUM", "MP", "PKI", "KIN", "ATO", "MA", "OM", "TOM" };
    public string[] Quiz1_answer = { "CCHI", "NI", "ZU", "UCC" };
    public string[] Quiz2_answer = { "PO", "TO", "TAT", "OTA" };
    public string[] Quiz3_answer = { "USH", "OO", "HRO", "SHR" };
    public string[] Quiz4_answer = { "PUM", "MP", "PKI", "KIN" };
    public string[] Quiz5_answer = { "ATO", "MA", "OM", "TOM" };
    public string thisQuizAnswer;
    public List<string> random_wrong_answer;

    public static DementiaGame1_DataManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
      //  data = CSVReader.Read("FourWordList"); //낱말찾기
    }
    private void Start()
    {
        StopCoroutine(_GameStart());
        StartCoroutine(_GameStart());

    }
    IEnumerator _GameStart()
    {
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        DementiaGame1_Play();
        yield return null;
    }

    public void DementiaGame1_Play()
    {
        StopCoroutine("_DementiaGame1_Play");
        StartCoroutine("_DementiaGame1_Play");
    }
    IEnumerator _DementiaGame1_Play()
    {
        //랜덤으로 나와야할 것, 문제 5개 중에 1개
        int Quiz_Num = Random.Range(0, UIManager.QuizNum_Grup.Length);
        UIManager.QuizNum_Grup[Quiz_Num].SetActive(true);

        //빈칸 4개중 한개 
        int QuizBlank_Num = Random.Range(0, 4);//현재 문제의 빈칸 정답

        if (Quiz_Num.Equals(0)) 
        { 
            UIManager.Quiz1_Blank_ob[QuizBlank_Num].SetActive(true); 
            thisQuizAnswer = Quiz1_answer[QuizBlank_Num];
            UIManager.BlankAnswer_ob = UIManager.Quiz1_Blank_ob[QuizBlank_Num].transform.GetChild(0).gameObject; 
        }
        else if (Quiz_Num.Equals(1)) 
        { 
            UIManager.Quiz2_Blank_ob[QuizBlank_Num].SetActive(true); 
            thisQuizAnswer = Quiz2_answer[QuizBlank_Num];
            UIManager.BlankAnswer_ob = UIManager.Quiz2_Blank_ob[QuizBlank_Num].transform.GetChild(0).gameObject;
        }
        else if (Quiz_Num.Equals(2)) 
        { 
            UIManager.Quiz3_Blank_ob[QuizBlank_Num].SetActive(true); 
            thisQuizAnswer = Quiz3_answer[QuizBlank_Num];
            UIManager.BlankAnswer_ob = UIManager.Quiz3_Blank_ob[QuizBlank_Num].transform.GetChild(0).gameObject;
        }
        else if (Quiz_Num.Equals(3)) 
        { 
            UIManager.Quiz4_Blank_ob[QuizBlank_Num].SetActive(true); 
            thisQuizAnswer = Quiz4_answer[QuizBlank_Num];
            UIManager.BlankAnswer_ob = UIManager.Quiz4_Blank_ob[QuizBlank_Num].transform.GetChild(0).gameObject;
        }
        else if (Quiz_Num.Equals(4))
        { 
            UIManager.Quiz5_Blank_ob[QuizBlank_Num].SetActive(true); 
            thisQuizAnswer = Quiz5_answer[QuizBlank_Num];
            UIManager.BlankAnswer_ob = UIManager.Quiz5_Blank_ob[QuizBlank_Num].transform.GetChild(0).gameObject;
        }
   
        int random;
        // 선택지 4개 _포함 지문
        while (random_wrong_answer.Count < 4)
        {
            random = Random.Range(0, Quiz_answer.Length);
            string str = Quiz_answer[random];
            if (random_wrong_answer.Count != 0)
            {
                if (!random_wrong_answer.Contains(str))
                {
                    random_wrong_answer.Add(str);
                }
            }
            else
            {
                random_wrong_answer.Add(thisQuizAnswer);
            }

            yield return null;
        }
        GameAppManager.instance.GetShuffleList(random_wrong_answer);
        //보기안에 텍스트 넣어줌
        for (int j = 0; j < 4; j++)
            UIManager.CheckAnswer_text[j].text = random_wrong_answer[j];

    yield return null;
    }

}
