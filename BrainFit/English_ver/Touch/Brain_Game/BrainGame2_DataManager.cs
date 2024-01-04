using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame2_DataManager : MonoBehaviour
{
    public Touch_TimerManager TimerManager_sc;
    public BrainGame2_UIManager UIManager;

    public List<string> problem_list;
    public List<string> QuestionValue_list;

    public int Problem_number; //문제 번호 
    public int Question_number; //문제 번호 

    public static BrainGame2_DataManager instance { get; private set; }
    /*
        식당에 방문한 당신
        메뉴판이 걸려 있다.
        당신과 친구가 먹은 음식의 값을 계산해서 얼마인지 입력하라
        1) 제한 시간 초과 시 실패
        2) 잘못 된 숫자 입력 시 실패
        3) 숫자 수정 가능
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
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        BrainGame2_Play();
        yield return null;
    }
    void Initialization()
    {
        problem_list.Clear();
        QuestionValue_list.Clear();
        UIManager.AnswerValue = "0";
        UIManager.AnswerNum.text = "0";
    }
    public void BrainGame2_Play()
    {
        StopCoroutine("_BrainGame2_Play");
        StartCoroutine("_BrainGame2_Play");
    }
    IEnumerator _BrainGame2_Play()
    {
        Initialization();
 
        //문제 번호 고르기
        GetMenuList(); //메뉴 리스트
        UIManager.SetTextDataText();
        TimerManager_sc.FindWord_sec_Timer(25,10,2);
        //게임끝
        yield return null;
    }
    public void GetMenuList()
    {
        Problem_number = Random.Range(0, 3);
        if (Problem_number.Equals(0))
        {
            problem_list = new List<string>
            { "Kimchi Soup 2 + Rice 2 = ?", "Steamed Beef 1 + Rice 2 + Egg Roll 1 = ?", "Miso Soup 2 + Rice 3 = ?", "Miso Soup 3 + Egg Roll 2 + Rice 1 = ?" ,"Miso Soup 1 + Egg Roll 1 + Rice 1 = ?"};
            QuestionValue_list = new List<string> { "13000", "11500", "13000", "21000", "8500" };
        }
        else if (Problem_number.Equals(1))
        {
        
            problem_list = new List<string>
            { "Kimbab 2 + Soon-dae 1 + Fries 1 = ?", "Ttokbokki 2 + Soon-dae 1 + Fries 1 = ?", "Ramen 2 + Kimbab 2 + Fries 1 = ?", "Ramen 1 + Soon-dae 3 + Kimbab 1 = ?" ,"Kimbab 3 + Ramen 2 + Fries 1 = ?"};
            QuestionValue_list = new List<string> {"11500", "10500", "11500", "17000", "14500" };
        }
        else if (Problem_number.Equals(2))
        {
            problem_list = new List<string>
            { "Hirame Sushi 2 + Tamago Sushi 3 = ?", "Sake Sushi 5 + Udong = ?", "Udong 2 + Ebi Sushi 2 = ?", "Hirame Sushi 1 + Tamago Sushi 1 + Udong 1 = ?" ,"Ebi Sushi 2 + Hirame Sushi 2 = ?"};
            QuestionValue_list = new List<string> { "26500", "36000", "7000", "11500", "15000" };
        }

    }


}
