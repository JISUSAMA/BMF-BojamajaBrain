using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DementiaGame3_DataManager : MonoBehaviour
{

    public DementiaGame3_UIManager UIManager;

    public List<Dictionary<string,string>> object_Data;
    public int QuestionNum;//문제의 정답
    public string QusetionStr; //물체의 정답

    public List<string> Object_Data_OB_List; //물체 이름 
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
        Set_ObjectDataList();
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        SetUIGrup.instance.TimeToScore(25);
        yield return null;
    }
  
    void Set_ObjectDataList()
    {
        StopCoroutine("_Set_ObjectDataList");
        StartCoroutine("_Set_ObjectDataList");
    }
    IEnumerator _Set_ObjectDataList()
    {
        QuestionNum = UnityEngine.Random.Range(0,UIManager.ObjectSpriteKinds.Length);
        string RandObject_str = object_Data[QuestionNum]["object"];
      
        //랜덤 문제 4개 
        while (Object_Data_OB_List.Count < 4)
        {
            if (!Object_Data_OB_List.Count.Equals(0))
            {
                int randQuestion = UnityEngine.Random.Range(0, UIManager.ObjectSpriteKinds.Length);
                RandObject_str = object_Data[randQuestion]["object"];
                if (!Object_Data_OB_List.Contains(RandObject_str))
                {
                    Object_Data_OB_List.Add(RandObject_str);
                }
            }
            else
            {
                Object_Data_OB_List.Add(RandObject_str);
                QusetionStr = RandObject_str;
            }
            yield return null;
        }
        GameAppManager.instance.GetShuffleList(Object_Data_OB_List);
        UIManager.ObjectImg.sprite = UIManager.ObjectSpriteKinds[QuestionNum];
        for (int i = 0; i < UIManager.ObjectText.Length; i++)
        {
            UIManager.ObjectText[i].text = Object_Data_OB_List[i];
        }
        yield return null;
    }
 
}
