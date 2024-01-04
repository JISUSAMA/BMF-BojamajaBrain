using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame4_DataManager : MonoBehaviour
{
    //양손으로 다른 그림 그리기
    public Touch_TimerManager TimerManager_sc;
    public BrainGame4_UIManager UIManager;
    public DrawCtrl[] ShapeGrup;
    public int QuestionLeft_Num, QuestionRight_Num;
    public bool DrawLine_GameRun;
    //checkP1, checkP2 = left  checkP3, checkP4 = right  
    public bool checkPoint1, checkPoint2, checkPoint3, checkPoint4 = false;
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
        UIManager.DrawLine_Start();
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        TimerManager_sc.FindWord_sec_Timer(18, 10, 5);
        //게임끝
        yield return null;
    }

}
