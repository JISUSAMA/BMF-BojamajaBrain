using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame2_DataManager : MonoBehaviour
{
    public Touch_TimerManager TimerManager_sc;
    public DementiaGame2_UIManager UIManager;
    //public List<Dictionary<string, string>> proverb_data;
    //public List<string> Proverb_List; //속담 문제 4개 
    public List<int> ProverbNum_List; //문제 번호 4개 ->해당 속담의 고유 번호를 담음
    public List<int> ProverbNum_Shuffle; //섞인 문제의 번호
     
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
        //proverb_data = CSVReader.Read("ProverbList"); //낱말찾기

    }
    private void Start()
    {
        StopCoroutine(_GameStart());
        StartCoroutine(_GameStart());

    }
    IEnumerator _GameStart()
    {
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        DementiaGame2_Play();
        yield return null;
    }

    public void DementiaGame2_Play()
    {
        StopCoroutine("_DementiaGame2_Play");
        StartCoroutine("_DementiaGame2_Play");
    }
    IEnumerator _DementiaGame2_Play()
    {
        SetProverb();//랜덤으로 4개의 문제 선택 하고 리스트 안에 넣어줌
        TimerManager_sc.FindWord_sec_Timer(20,10,5);
        yield return null;
    }

    void SetProverb()
    {
        StopCoroutine("_Set_ProverbList");
        StartCoroutine("_Set_ProverbList");
    }
    IEnumerator _Set_ProverbList()
    {
        //문제 종류 3가지 중에 하나 골라서 해당 스프라이트 관련 그림자랑 이미지 가져오기 
        int Random_Question_number = UnityEngine.Random.Range(0, 4);
       
        //랜덤으로 4문제를 뽑아서 숫자리스트와 속담리스트 안에 넣어줌
        while (ProverbNum_List.Count < 4)
        {
            int randomNum = UnityEngine.Random.Range(0, 4);
            if (ProverbNum_List.Count != 0)
            {
                if (!ProverbNum_List.Contains(randomNum))
                {
                   // Proverb_List.Add(randomNum);
                    ProverbNum_List.Add(randomNum);
                    ProverbNum_Shuffle.Add(randomNum);
                }
            }
            else
            {
              //  Proverb_List.Add(randomNum);
                ProverbNum_List.Add(randomNum);
                ProverbNum_Shuffle.Add(randomNum);
            }
            yield return null;
        }
        GameAppManager.instance.GetShuffleList(ProverbNum_Shuffle); //뽑은 속담 관련 숫자 리스트를 랜덤으로 섞음 
        //UI상에 뿌려줌
        for (int j = 0; j < 4; j++)
        {
            //첫번째 문제 이미지 목록
            if (Random_Question_number.Equals(0))
            {
                UIManager.ProverbImage[j].sprite = UIManager.Sprite_Kinds_1[ProverbNum_List[j]];//속담에 맞는 이미지를 넣어줌
                UIManager.Proverb_shadow[j].sprite = UIManager.Sprite_shadowKinds_1[ProverbNum_Shuffle[j]];//속담을 넣어줌
            }
            else if (Random_Question_number.Equals(1))
            {
                UIManager.ProverbImage[j].sprite = UIManager.Sprite_Kinds_2[ProverbNum_List[j]];//속담에 맞는 이미지를 넣어줌
                UIManager.Proverb_shadow[j].sprite = UIManager.Sprite_shadowKinds_2[ProverbNum_Shuffle[j]];//속담을 넣어줌
            }
            else if (Random_Question_number.Equals(2))
            {
                UIManager.ProverbImage[j].sprite = UIManager.Sprite_Kinds_3[ProverbNum_List[j]];//속담에 맞는 이미지를 넣어줌
                UIManager.Proverb_shadow[j].sprite = UIManager.Sprite_shadowKinds_3[ProverbNum_Shuffle[j]];//속담을 넣어줌
            }
            else if (Random_Question_number.Equals(3))
            {
                UIManager.ProverbImage[j].sprite = UIManager.Sprite_Kinds_3[ProverbNum_List[j]];//속담에 맞는 이미지를 넣어줌
                UIManager.Proverb_shadow[j].sprite = UIManager.Sprite_shadowKinds_3[ProverbNum_Shuffle[j]];//속담을 넣어줌
            }
        }
        yield return null;
    }
  
}
