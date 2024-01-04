using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame4_DataManager : MonoBehaviour
{

    public BrainGame4_UIManager UIManager;
  //  public DrawCtrl[] ShapeGrup;
    public int QuestionLeft_Num, QuestionRight_Num;
    public bool DrawLine_GameRun;
    //checkP1, checkP2 = left  checkP3, checkP4 = right  
    public bool L_checkPoint1, L_checkPoint2, L_checkPoint3, L_checkPoint4, L_checkPoint5 = false;
    public bool R_checkPoint1, R_checkPoint2, R_checkPoint3, R_checkPoint4, R_checkPoint5 = false;
    public static BrainGame4_DataManager instance { get; private set; }
    /*
        동그라미, 세모, 네모 이 세가지 도형이 양쪽에 다르게 그려져 있고 제한 시간 내에 양손으로 두 가지 다른 도형의 선을 따라 그림을 완성하면 되는 게임
        1) 시간 내 완료하지 못하면 실패
        2) 움직임을 가이드하는 원 밖을 벗어나면 실패
   */
    void Awake()
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
        UIManager.Set_QuestionShape();
       
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        SetUIGrup.instance.TimeToScore(30);
        UIManager.DrawLine_Start();
        //게임끝
        yield return null;

    }

}
