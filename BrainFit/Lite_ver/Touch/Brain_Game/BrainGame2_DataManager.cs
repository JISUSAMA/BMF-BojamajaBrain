using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame2_DataManager : MonoBehaviour
{

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
    void Initialization()
    {
        problem_list.Clear();
        QuestionValue_list.Clear();
        UIManager.AnswerValue = "0";
        UIManager.AnswerNum.text = "0";
    }

    IEnumerator _GameStart()
    {
        // yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        Initialization();
        //문제 번호 고르기
        GetMenuList(); //메뉴 리스트
        UIManager.SetTextDataText();
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true);
        SetUIGrup.instance.TimeToScore(20);
        //게임끝
        yield return null;
    }

    public void GetMenuList()
    {
        Problem_number = Random.Range(0, 3);
        if (Problem_number.Equals(0))
        {
         
            problem_list = new List<string>
            { "김치찌개 2 + 공기밥 2 = ?", "갈비찜 1 + 공기밥 2 + 계란말이 1 = ?", "된장찌개 2 + 공기밥 3 = ?", "된장찌개 3 + 계란말이 2 + 공기밥 1 = ?" ,"된장찌개 1 + 계란말이 1 + 공기밥 1 = ?"};
            QuestionValue_list = new List<string> { "13000", "11500", "13000", "21000", "8500" };
        }
        else if (Problem_number.Equals(1))
        {
        
            problem_list = new List<string>
            { "깁밥 2 + 순대 1 + 튀김 1 = ?", "떡볶이 2 + 순대 1 + 튀김 1 = ?", "라면 2 + 김밥 2 + 튀김 1 = ?", "라면 1 + 순대 3 + 김밥 1 = ?" ,"깁밥 3 + 라면 2 + 튀김 1 = ?"};
            QuestionValue_list = new List<string> {"9500", "8500", "15500", "13000", "18500" };
        }
        else if (Problem_number.Equals(2))
        {
            problem_list = new List<string>
            { "광어초밥 1 + 계란초밥 2 = ?", "연어초밥 2 + 우동 1 = ?", "우동 2 + 새우초밥 2 = ?", "광어초밥 1 + 계란초밥 1 + 우동 1 = ?" ,"새우초밥 2 + 광어초밥 2 = ?"};
            QuestionValue_list = new List<string> { "16000", "15000", "7000", "11500", "15000" };
        }

    }


}
