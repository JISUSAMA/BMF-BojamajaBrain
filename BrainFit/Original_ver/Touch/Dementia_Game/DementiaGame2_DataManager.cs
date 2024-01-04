using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame2_DataManager : MonoBehaviour
{
    public Touch_TimerManager TimerManager_sc;
    public DementiaGame2_UIManager UIManager;
    public List<Dictionary<string, string>> proverb_data;
    public List<string> Proverb_List; //속담 문제 4개 
    public List<string> ProverbNum_List; //문제 번호 4개 ->해당 속담의 고유 번호를 담음
    public List<string> ProverbNum_Shuffle; //섞인 문제의 번호
     
    public int AnswerQuestionCount = 0;
    public static DementiaGame2_DataManager instance { get; private set; }
    /// <summary>
    /// 속담이 주어지고 주어진 속담과 관련 있는 그림과 연결하는 게임으로 한 페이지 당 4개의 속담과 그림이 주어 진다.
    /// 알맞은 속담과 글을 라인으로 연결하는 게임
    /// 1) 시간 내 완료하지 못하면 실패
    /// 2) 잘못된 항목 연결하면 실패
    /// </summary>
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;

        proverb_data = CSVReader.Read("ProverbList"); //낱말찾기
        
    }

    private void Start()
    {
        StopCoroutine(_GameStart());
        StartCoroutine(_GameStart());
    }
    IEnumerator _GameStart()
    {
        SetProverb();//랜덤으로 4개의 문제 선택 하고 리스트 안에 넣어줌
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        TimerManager_sc.FindWord_sec_Timer(20, 10, 5);
        yield return null;
    }
    void SetProverb()
    {
        StopCoroutine("_Set_ProverbList");
        StartCoroutine("_Set_ProverbList");
    }
    IEnumerator _Set_ProverbList()
    {
        //랜덤으로 4문제를 뽑아서 숫자리스트와 속담리스트 안에 넣어줌
        while (Proverb_List.Count < 4)
        {
            int randomNum = UnityEngine.Random.Range(0, proverb_data.Count - 1);
            string RandVerb_str = proverb_data[randomNum]["proverb"];
            if (Proverb_List.Count != 0)
            {
                if (!Proverb_List.Contains(RandVerb_str))
                {
                    Proverb_List.Add(proverb_data[randomNum]["proverb"]);
                    ProverbNum_List.Add(proverb_data[randomNum]["number"]);
                    ProverbNum_Shuffle.Add(proverb_data[randomNum]["number"]);
                }
            }
            else
            {
                Proverb_List.Add(proverb_data[randomNum]["proverb"]);
                ProverbNum_List.Add(proverb_data[randomNum]["number"]);
                ProverbNum_Shuffle.Add(proverb_data[randomNum]["number"]);
            }
            yield return null;
        }
        GameAppManager.instance.GetShuffleList(ProverbNum_Shuffle); //뽑은 속담 관련 숫자 리스트를 랜덤으로 섞음 
        //UI상에 뿌려줌
        for (int j = 0; j < 4; j++)
        {
            int Num_i = Int32.Parse(ProverbNum_Shuffle[j]);
            UIManager.ProverbText[j].text = Proverb_List[j];//속담을 넣어줌
            UIManager.ProverbImage[j].sprite = UIManager.ProverbSpriteKinds[Num_i-1];//속담에 맞는 이미지를 넣어줌
        }
        yield return null;
    }
  
}
