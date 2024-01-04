using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame2_DataManager : MonoBehaviour
{

    public DementiaGame2_UIManager UIManager;
    public List<Dictionary<string, string>> proverb_data;
    public string Proverb_List; //속담 문제 4개 
    public List<string> ProverbSTR_List; //문제 번호 4개 ->해당 속담의 고유 번호를 담음
   public int QuestionNum;
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
        //TimerManager_sc.FindWord_sec_Timer(20,10,5);
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        SetUIGrup.instance.TimeToScore(25);
        yield return null;

    }

    void SetProverb()
    {
        StopCoroutine("_Set_ProverbList");
        StartCoroutine("_Set_ProverbList");
    }
    IEnumerator _Set_ProverbList()
    {
        QuestionNum = UnityEngine.Random.Range(0,UIManager.ProverbSpriteKinds.Length);
        string RandVerb_str =proverb_data[QuestionNum]["proverb"];
        //랜덤 문제 4개 넣기 
        while (ProverbSTR_List.Count < 4)
        {
            if (ProverbSTR_List.Count != 0)
            {
                int random = UnityEngine.Random.Range(0, UIManager.ProverbSpriteKinds.Length);
                RandVerb_str = proverb_data[random]["proverb"];
                if (!ProverbSTR_List.Contains(RandVerb_str))
                {
                    ProverbSTR_List.Add(RandVerb_str);
                }
            }
            else
            {
                ProverbSTR_List.Add(RandVerb_str);
                Proverb_List = RandVerb_str;
            }
            yield return null;
        }
        GameAppManager.instance.GetShuffleList(ProverbSTR_List);

        UIManager.ProverbImage.sprite = UIManager.ProverbSpriteKinds[QuestionNum];
        for(int i =0; i<UIManager.ProverbText.Length; i++)
        {
            UIManager.ProverbText[i].text = ProverbSTR_List[i];
        }
    
        yield return null;
    }
  
}
