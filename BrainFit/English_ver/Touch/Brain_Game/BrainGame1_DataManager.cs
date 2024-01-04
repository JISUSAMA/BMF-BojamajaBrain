using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame1_DataManager : MonoBehaviour
{
    [Header("BrainGame1-Find Word And Touch")]
    public Touch_TimerManager TimerManager_sc;  
    public BrainGame1_UIManager UIManager; 
    [SerializeField] public string[] BrainGame1_Word_list = { "PEN", "TENT", "HOUSE", "EGG", "DAY", "TREE", "ZOO", "FOG", "QUEEN", "SAFETY", "HAT", "CON", "SPY", "HEN", "TANK"
    ,"FOOD","CLASS","MOST","JEALOUS","FRUIT","FALL","CRAFT","FAIL","WIRE","TURKEY","SUMMER","WINTER","MENU","MAIL","INK"};
    public List<string> BrainGame1_Piece_str_list;
    public List<string> QusetionPieceWord_list;
    int PieceBtnCount_int =25;

    public static BrainGame1_DataManager instance { get; private set; }
    /// <summary>
    /// 5*5 표 안에 배열된 글자들 속에서 아래 예시에서 요구하는 단어를 순서대로 터치하여 완성하기
    /// 1) 잘못된 글자 터치하면 실패
    /// 2) 순서 틀리면 실패
    /// 3) 제한 시간 넘기면 실패
    /// </summary>
    void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        BrainGame1_Piece_str_list = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "Y", "Z" };
    }
    private void Start()
    {
        StopCoroutine(_GameStart());
        StartCoroutine(_GameStart());

    }
    IEnumerator _GameStart()
    {
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        BrainGame1_Play();
        yield return null;
    }
    //초기화
    void Initialization()
    {
        UIManager.Click_num = -1;
        UIManager.QuestionWord_text.text = "";
        UIManager.FindWord_text.text = "";
        QusetionPieceWord_list.Clear();
    }
    public void BrainGame1_Play()
    {
        StopCoroutine("_BrainGame1_Play");
        StartCoroutine("_BrainGame1_Play");
    }
    IEnumerator _BrainGame1_Play()
    {
        Initialization();
        Choose_Random_Word();
        yield return null;
    }

    public void Choose_Random_Word()
    {
        GameAppManager.instance.GetShuffleList(BrainGame1_Piece_str_list);
        int rand_num = Random.Range(0, BrainGame1_Word_list.Length);
        UIManager.QuestionWord_text.text = BrainGame1_Word_list[rand_num];
        for (int i = 0; i < UIManager.QuestionWord_text.text.Length; i++)
        {
            QusetionPieceWord_list.Add(UIManager.QuestionWord_text.text.Substring(0, i + 1));
        }
  
        for (int i = 0; i < PieceBtnCount_int; i++)
        {
            UIManager.Piece_btn_text[i].text = BrainGame1_Piece_str_list[i];
        }
        TimerManager_sc.FindWord_sec_Timer(15,10,5);
    }
   
}
