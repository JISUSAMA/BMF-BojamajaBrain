using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame5_DataManager : MonoBehaviour
{
    [SerializeField] List<Dictionary<string, string>> data;
    public List<string> Order_word; //비교하는 리스트
    public List<string> Order_Input_word; //입력받은 단어\
    public List<string> PieceStringList;

    public BrainGame5_UIManager UIManager;
    public string[] OrderHead;

    public static BrainGame5_DataManager instance { get; private set; }
    /*
        25개 칸에 숫자가 무작위로 배열되어 있음
        숫자는 아라비아 숫자와 한글 숫자 두 가지 중 하나로 표기될 수 있으며 이를 순서대로 터치해서 25까지 완료하면 완성되는 게임
        1) 순서와 다른 숫자를 터치하면 실패
        2) 시간 내 완료하지 못하면 실패     
    */
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;

        data = CSVReader.Read("NumberList"); //낱말찾기
        OrderHead = new string[] { "KR", "NUM" };

    }
    private void Start()
    {
        StopCoroutine(_GameStart());
        StartCoroutine(_GameStart());

    }
    //초기화
    void Initialization()
    {
        UIManager.CurrnetOrderNum = -1;
        Order_word.Clear(); //랜덤으로 글자 또는 수를 다시 넣어줌
        PieceStringList.Clear();
        for (int i = 0; i < UIManager.NumberBtn.Length; i++) { UIManager.NumberBtn[i].interactable = true; }

    }
    IEnumerator _GameStart()
    {
        Initialization();
        Choose_Question_Word();
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        SetUIGrup.instance.TimeToScore(65);
        //게임끝
        yield return null;
    }
  
    void Choose_Question_Word()
    {
        for (int i = 0; i < 25; i++)
        {
            string str = OrderHead[Random.Range(0, 2)];
            Order_word.Add(data[i][str]);
        }

        while (PieceStringList.Count < 25)
        {
            string rand_piecee = Order_word[Random.Range(0, Order_word.Count)];
            if (PieceStringList.Count != 0)
            {
                if (!PieceStringList.Contains(rand_piecee))
                {
                    PieceStringList.Add(rand_piecee);
                }
            }
            else
            {
                PieceStringList.Add(rand_piecee);
            }
        }
        for (int i = 0; i < UIManager.NumberBtn_text.Length; i++)
        {
            UIManager.NumberBtn_text[i].text = PieceStringList[i];
        }
    }


}

