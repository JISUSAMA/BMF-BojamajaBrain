using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame7_DataManager : MonoBehaviour
{

    public BrainGame7_UIManager UIManager;
    public List<int> QuestionNumberList;
    public List<int> Question_answer; //문제가 틀린문제인지 맞는문제인지 확인 0:올바른 1:틀린
 //   public string[] Answer_StrList;
    public static BrainGame7_DataManager instance { get; private set; }
    public void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }
    private void Start()
    {
        StopCoroutine(_GameStart());
        StartCoroutine(_GameStart());
    }
    IEnumerator _GameStart()
    {
        Choose_Random_Question();
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        SetUIGrup.instance.TimeToScore(30);
        yield return null;
    }
 
    public void Choose_Random_Question()
    {
        StopCoroutine("_Choose_Random_Question");
        StartCoroutine("_Choose_Random_Question");
    }
    IEnumerator _Choose_Random_Question()
    {
        while (QuestionNumberList.Count < 6)
        {
            int randomNum = UnityEngine.Random.Range(0, 40);
            if (QuestionNumberList.Count != 0)
            {
                if (!QuestionNumberList.Contains(randomNum))
                {
                    QuestionNumberList.Add(randomNum);
                }
            }
            else { QuestionNumberList.Add(randomNum); }
            yield return null;
        }
        //데이터 값에 따라서 이미지를 뿌려줌
        for (int i = 0; i < 5; i++)
        {
            UIManager.Question_img_left[i].sprite = UIManager.LeftIMG_sprite[QuestionNumberList[i]];
            int randome_Correct_Wrong = UnityEngine.Random.Range(0, 2);
            Question_answer.Add(randome_Correct_Wrong);
            if (randome_Correct_Wrong.Equals(0))
                UIManager.Question_img_right[i].sprite = UIManager.RightIMG_Correct[QuestionNumberList[i]];
            else UIManager.Question_img_right[i].sprite = UIManager.RightIMG_Wrong[QuestionNumberList[i]];
        }
    }


}
