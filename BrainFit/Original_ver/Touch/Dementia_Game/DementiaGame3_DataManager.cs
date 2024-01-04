using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DementiaGame3_DataManager : MonoBehaviour
{
    public Touch_TimerManager TimerManager_sc; 
    public DementiaGame3_UIManager UIManager;

    public List<Dictionary<string,string>> object_Data;
    public List<string> Object_Data_SuffleNum;

    public List<string> Object_Data_OB; //물체 이름 
    public List<string> Object_Data_Num;
    public int AnswerQuestionCount = 0; //맞춘 정답의 갯수
    /// <summary>
    /// 사물을 나타내는 그림과 해당 글자를 연결하는 게임. 총 네 개의 그림과 글자 쌍이 주어진다.
    /// 1) 시간 내 완료하지 못하면 실패
    /// 2) 잘못된 항목 연결하면 실패
    /// </summary>
    /// 
    public static DementiaGame3_DataManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;
        object_Data = CSVReader.Read("ObjectList"); //낱말찾기
    }
    private void Start()
    {
        StopCoroutine(_GameStart());
        StartCoroutine(_GameStart());

    }
    IEnumerator _GameStart()
    {
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        DementiaGame3_Play();
        yield return null;
    }
    public void DementiaGame3_Play()
    {
        StopCoroutine("_DementiaGame3_Play");
        StartCoroutine("_DementiaGame3_Play");
    }
    IEnumerator _DementiaGame3_Play()
    {
        Set_ObjectDataList();
        TimerManager_sc.FindWord_sec_Timer(20, 10, 5);
        UIManager.DrawLine_Start();
       
        yield return null;
    }

    void Set_ObjectDataList()
    {
        StopCoroutine("_Set_ObjectDataList");
        StartCoroutine("_Set_ObjectDataList");
    }
    IEnumerator _Set_ObjectDataList()
    {
        while (Object_Data_Num.Count < 4)
        {
            int randomNum = UnityEngine.Random.Range(0, object_Data.Count - 1);
            string randomNum_str = object_Data[randomNum]["number"];
            if (!Object_Data_Num.Equals(0))
            {
                if (!Object_Data_Num.Contains(randomNum_str))
                {
                    Object_Data_Num.Add(object_Data[randomNum]["number"]);
                    Object_Data_OB.Add(object_Data[randomNum]["object"]);
                    Object_Data_SuffleNum.Add(object_Data[randomNum]["number"]);
                }
            }
            else
            {
                Object_Data_Num.Add(object_Data[randomNum]["number"]);
                Object_Data_OB.Add(object_Data[randomNum]["object"]);
                Object_Data_SuffleNum.Add(object_Data[randomNum]["number"]);
            }
            yield return null;
        }
        GameAppManager.instance.GetShuffleList(Object_Data_SuffleNum);
        for(int i =0; i<4; i++)
        {
            int Num_i =Int32.Parse(Object_Data_SuffleNum[i]);
            UIManager.ObjectImage[i].sprite = UIManager.ObjectSpriteKinds[Num_i-1];
            UIManager.ObjectText[i].text = Object_Data_OB[i];
        }
        yield return null;
    }
 
}
