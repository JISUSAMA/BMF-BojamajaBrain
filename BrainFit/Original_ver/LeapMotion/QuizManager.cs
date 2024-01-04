using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class RPS_Quiz
{
    public int num;
    public int quiz;
    public int winOrlose;

    public RPS_Quiz() { /*초기화*/ }

    // 순서대로 저장
    public RPS_Quiz(int a_num, int a_quiz, int a_winOrlose)
    {
        num = a_num;
        quiz = a_quiz;
        winOrlose = a_winOrlose;
    }
}

[System.Serializable]
public class ARITHMETIC_Quiz
{
    public int index;
    public int num1;
    public int num2;
    public int operation;
    public int result;
    public int winOrlose;

    public ARITHMETIC_Quiz() { /*초기화*/ }

    // 순서대로 저장
    public ARITHMETIC_Quiz(int a_index, int a_num1, int a_num2, int a_operation, int _result, int a_winOrlose)
    {
        index = a_index;
        num1 = a_num1;
        num2 = a_num2;
        operation = a_operation;
        result = _result;
        winOrlose = a_winOrlose;

        Debug.Log("result: "+ result+ "num1: "+ num1+ "num2: "+ num2+ "index: " + index);
    }
}

[System.Serializable]
public class BlueFlagAndWhiteFlag
{
    public int index;
    public string quiz;
    public int winOrlose;

    public BlueFlagAndWhiteFlag() { /*초기화*/ }

    // 순서대로 저장
    public BlueFlagAndWhiteFlag(int a_index, string a_quiz, int a_winOrlose)
    {
        index = a_index;
        quiz = a_quiz;
        winOrlose = a_winOrlose;
    }
}

public class QuizManager : MonoBehaviour
{
    // 퀴즈 리스트
    public List<RPS_Quiz> m_quizzes;
    public List<ARITHMETIC_Quiz> m_arithmetic_quizzes;
    // B > 청기, W > 백기, U > 올려, D > 내려, N 내리지마, O 올리지마
    public List<BlueFlagAndWhiteFlag> m_bwFlag_quizzes;
    public List<int> m_bwFlag_quizIndex;
    public int m_quizCount;   // 랜덤 문제 갯수 조절

    public static QuizManager Instance { get; private set; }

    public TMP_Text winOrlose_text;

    [Header("Arithmetic HandPicture")]
    public GameObject[] L_finger;
    public GameObject[] R_finger;

    [Header("Flag Quiz")]
    public GameObject[] flagQuizText;
    public string BLeft;
    public string WRight;
    public TMP_Text BLeft_Text;
    public TMP_Text WRight_Text;
    private int flagQuizindex;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }

    // 이기는 - 가위, 바위, 보 : 1, 2, 3
    void Start()
    {

    }

    // 사칙연산 문제 생성
    public void Arithmetic_RandomQuiz(int a_quizCount)
    {
        int[] number;
        Image[] images;
        int oper;
        int res;

        m_quizCount = a_quizCount;
        m_arithmetic_quizzes.Clear();
        // 5를 제외한 1~4 까지 사칙연산 고르고 문제 내기
        for (int i = 0; i < a_quizCount; i++)
        {
            oper = UnityEngine.Random.Range(1, 5);
            number = Get_Arithmetic_Values(oper);
            res = GetResult(number, oper);

            m_arithmetic_quizzes.Add(new ARITHMETIC_Quiz(i, number[0], number[1], oper, res, 0));
        }
    }
    // 스위치문 으로 +, -, *, / 생성
    private int[] Get_Arithmetic_Values(int a_operation)
    {
        // a_operation > 1 : + , 2 : - , 3 : * , 4 : /
        // arr[0] : num1, arr[1] : num2
        int[] arr = new int[2];

        switch (a_operation)
        {
            case 1: // 더하기
                arr = GetValuesPlusQuiz(arr);
                break;
            case 2: // 빼기
                arr = GetValuesMinusQuiz(arr);
                break;
            case 3: // 곱하기
                arr = GetValuesMultiplyQuiz(arr);
                break;
            case 4: // 나누기
                arr = GetValuesDivideQuiz(arr);
                break;
            default:
                break;
        }

        return arr;
    }
    //--------------------------- 사칙연산 종류별로 문제 가져오기 -------------------------
    private int[] GetValuesPlusQuiz(int[] a_arr)
    {
        if (AppManager.Instance.gameMode == 2)
        {
            a_arr[0] = UnityEngine.Random.Range(1, 11);    // 1 ~ 10
            a_arr[1] = UnityEngine.Random.Range(0, Math.Abs(a_arr[0] - 10) + 1);    // 0 ~ 9
        }
        else if (AppManager.Instance.gameMode == 3)
        {
            a_arr[0] = UnityEngine.Random.Range(1, 6);    // 1 ~ 5
            a_arr[1] = UnityEngine.Random.Range(0, 6);    // 0 ~ 5
        }

        return a_arr;
    }
    private int[] GetValuesMinusQuiz(int[] a_arr)
    {
        if (AppManager.Instance.gameMode == 2)
        {
            a_arr[0] = UnityEngine.Random.Range(1, 11);    // 1 ~ 10
            a_arr[1] = UnityEngine.Random.Range(0, Math.Abs(a_arr[0] - 1));    // 0 ~ 9
        }
        else if (AppManager.Instance.gameMode == 3)
        {
            a_arr[0] = UnityEngine.Random.Range(1, 6);    // 1 ~ 5
            a_arr[1] = UnityEngine.Random.Range(0, a_arr[0] + 1);    // 0 ~ 9
        }

        return a_arr;
    }
    private int[] GetValuesMultiplyQuiz(int[] a_arr)
    {
        // gameMode >
        // 0 : 이기기게임
        // 1 : 지는게임
        // 2 : 사칙연산게임 숫자
        // 3 : 사칙연산게임 손가락
        if (AppManager.Instance.gameMode == 2)
        {
            a_arr[0] = UnityEngine.Random.Range(1, 11);    // 1 ~ 10

            if (a_arr[0] == 1)  // 1
            {
                //a_arr[1] = 10;    // 최대값
                a_arr[1] = UnityEngine.Random.Range(1, 11);
            }
            else if (a_arr[0] == 2)      // 2
            {
                //a_arr[1] = 5;
                a_arr[1] = UnityEngine.Random.Range(1, 6);
            }
            else if (a_arr[0] == 3)      // 3
            {
                //a_arr[1] = 3;
                a_arr[1] = UnityEngine.Random.Range(1, 4);
            }
            else if (a_arr[0] == 4)      // 4
            {
                //a_arr[1] = 2;
                a_arr[1] = UnityEngine.Random.Range(1, 3);
            }
            else if (a_arr[0] == 5)      // 5
            {
                //a_arr[1] = 2;
                a_arr[1] = UnityEngine.Random.Range(1, 3);
            }
            else  // 6 ~ 10
            {
                a_arr[1] = 1;
            }
        }
        else if (AppManager.Instance.gameMode == 3)
        {
            a_arr[0] = UnityEngine.Random.Range(1, 6);    // 1 ~ 5

            if (a_arr[0] == 1)  // 1
            {
                a_arr[1] = UnityEngine.Random.Range(1, 6);  // 1 ~ 5 최대값 설정
            }
            else if (a_arr[0] == 2)      // 2
            {
                a_arr[1] = UnityEngine.Random.Range(1, 6);  // 1 ~ 5
            }
            else if (a_arr[0] == 3)      // 3
            {
                a_arr[1] = UnityEngine.Random.Range(1, 4);  // 1 ~ 3
            }
            else if (a_arr[0] == 4)      // 4
            {
                a_arr[1] = UnityEngine.Random.Range(1, 3);  // 1 ~ 2
            }
            else if (a_arr[0] == 5)      // 5
            {
                a_arr[1] = UnityEngine.Random.Range(1, 3); // 1 ~ 2
            }
        }

        return a_arr;
    }
    private int[] GetValuesDivideQuiz(int[] a_arr)
    {
        List<int> divisor;

        if (AppManager.Instance.gameMode == 2)  // 사칙연산 숫자
        {
            a_arr[0] = UnityEngine.Random.Range(1, 11); // 첫번째 인자   1 ~ 10        
            divisor = GetDivisors(a_arr[0]);

            int index = UnityEngine.Random.Range(0, divisor.Count);

            a_arr[1] = divisor[index];
        }
        else if (AppManager.Instance.gameMode == 3) // 사칙연산 손가락
        {
            a_arr[0] = UnityEngine.Random.Range(1, 6); // 첫번째 인자   1 ~ 5        
            divisor = GetDivisors(a_arr[0]);

            int index = UnityEngine.Random.Range(0, divisor.Count);

            a_arr[1] = divisor[index];
        }

        return a_arr;
    }
    //----------------------------------------------------------------------------------
    // 약수 구하기
    private List<int> GetDivisors(int a_num)
    {
        List<int> divisors = new List<int>();

        for (int i = 1; i <= a_num; i++)
        {
            if (a_num % i == 0)
            {
                divisors.Add(i);
            }
        }
        return divisors;
    }

    private int GetResult(int[] ar_number, int a_oper)
    {
        int res = -99;
        switch (a_oper)
        {
            case 1:     // +
                res = ar_number[0] + ar_number[1];
                break;
            case 2:     // -
                res = ar_number[0] - ar_number[1];
                break;
            case 3:     // *
                res = ar_number[0] * ar_number[1];
                break;
            case 4:     // /
                res = ar_number[0] / ar_number[1];
                break;
            default:
                Debug.Log("결과값이 없음");
                break;
        }

        return res;
    }

    // Ready Event 에서 가위바위보 랜덤퀴즈 생성
    public void RPS_RandomQuiz(int a_quizCount)
    {
        m_quizCount = a_quizCount;
        m_quizzes.Clear();
        for (int i = 0; i < a_quizCount; i++)
        {
            m_quizzes.Add(new RPS_Quiz(i + 1, UnityEngine.Random.Range(1, 4), 0));
        }
    }

    // 이기기 가위 바위 보 승패 결정 함수
    public void WinGame(int _num, int a_RPS)
    {
        AppManager.Instance.setUIGrup.Check = true;
        if (a_RPS == 0)
        {
            Debug.Log("결과가 뭐든 상관없이 졌음");
            Question_Finish(1);
            return;
        }

        int a_num;
        a_num = _num - 1;

        // 1 : 가위, 2 : 바위, 3 : 보 => a_RPS
        //m_quizzes[a_num].winOrlose
        if (m_quizzes[a_num].quiz == 1)         // 문제가 가위
        {
            if (a_RPS == 1)         // 가위
            {   // 비기고
                m_quizzes[a_num].winOrlose = -1;
                winOrlose_text.text = "비김";
                Question_Finish(1);
              
           
            }
            else if (a_RPS == 2)    // 바위
            {   // 이기고
                m_quizzes[a_num].winOrlose = 1;
                winOrlose_text.text = "이김";
                AddScore(4);
                Question_Finish(0);
            }
            else if (a_RPS == 3)    // 보
            {   // 지고
                m_quizzes[a_num].winOrlose = 0;
                winOrlose_text.text = "짐";

                Question_Finish(1);
            }
        }
        else if (m_quizzes[a_num].quiz == 2)    // 문제가 바위
        {
            if (a_RPS == 1)         // 가위
            {   // 지고
                m_quizzes[a_num].winOrlose = 0;
                winOrlose_text.text = "짐";
                Question_Finish(1);
            }
            else if (a_RPS == 2)    // 바위
            {   // 비기고
                m_quizzes[a_num].winOrlose = -1;
                winOrlose_text.text = "비김";
                Question_Finish(1);
            }
            else if (a_RPS == 3)    // 보
            {   // 이기고
                m_quizzes[a_num].winOrlose = 1;
                winOrlose_text.text = "이김";
                AddScore(4);
                Question_Finish(0);
            }
        }
        else if (m_quizzes[a_num].quiz == 3)    // 문제가 보
        {
            if (a_RPS == 1)         // 가위
            {   // 이기고
                m_quizzes[a_num].winOrlose = 1;
                winOrlose_text.text = "이김";
                AddScore(4);
                Question_Finish(0);
            }
            else if (a_RPS == 2)    // 바위
            {   // 지고
                m_quizzes[a_num].winOrlose = 0;
                winOrlose_text.text = "짐";
                Question_Finish(1);
            }
            else if (a_RPS == 3)    // 보
            {   // 비기고
                m_quizzes[a_num].winOrlose = -1;
                winOrlose_text.text = "비김";
                Question_Finish(1);
            }
        }



    }

    // 지기 가위 바위 보 승패 결정 함수
    public void LoseGame(int _num, int a_RPS)
    {
        AppManager.Instance.setUIGrup.Check = true;
        if (a_RPS == 0)
        {
            Debug.Log("결과가 뭐든 상관없이 졌음");
            Question_Finish(1);
            return;
        }

        int a_num;
        a_num = _num - 1;
        // 1 : 가위, 2 : 바위, 3 : 보 => a_RPS
        //m_quizzes[a_num].winOrlose
        if (m_quizzes[a_num].quiz == 1)         // 문제가 가위
        {
            if (a_RPS == 1)         // 가위
            {   // 비기고
                m_quizzes[a_num].winOrlose = -1;
                winOrlose_text.text = "비김";
                Question_Finish(1);
            }
            else if (a_RPS == 2)    // 바위
            {   // 이기고
                m_quizzes[a_num].winOrlose = 1;
                winOrlose_text.text = "짐";
                Question_Finish(1);
            }
            else if (a_RPS == 3)    // 보
            {   // 지고
                m_quizzes[a_num].winOrlose = 0;
                winOrlose_text.text = "이김";
                AddScore(4);
                Question_Finish(0);
            }
        }
        else if (m_quizzes[a_num].quiz == 2)    // 문제가 바위
        {
            if (a_RPS == 1)         // 가위
            {   // 지고
                m_quizzes[a_num].winOrlose = 0;
                winOrlose_text.text = "이김";
                AddScore(4);
                Question_Finish(0);
            }
            else if (a_RPS == 2)    // 바위
            {   // 비기고
                m_quizzes[a_num].winOrlose = -1;
                winOrlose_text.text = "비김";
                Question_Finish(1);
            }
            else if (a_RPS == 3)    // 보
            {   // 이기고
                m_quizzes[a_num].winOrlose = 1;
                winOrlose_text.text = "짐";
                Question_Finish(1);
            }
        }
        else if (m_quizzes[a_num].quiz == 3)    // 문제가 보
        {
            if (a_RPS == 1)         // 가위
            {   // 이기고
                m_quizzes[a_num].winOrlose = 1;
                winOrlose_text.text = "짐";
                Question_Finish(1);
            }
            else if (a_RPS == 2)    // 바위
            {   // 지고
                m_quizzes[a_num].winOrlose = 0;
                winOrlose_text.text = "이김";
                AddScore(4);
                Question_Finish(0);
            }
            else if (a_RPS == 3)    // 보
            {   // 비기고
                m_quizzes[a_num].winOrlose = -1;
                winOrlose_text.text = "비김";
                Question_Finish(1);
            }
        }
    }

    // 사칙연산 숫자 결과 결정 함수
    public void Arithmetic_Game_Numbers(int a_num, int a_res)
    {
        AppManager.Instance.setUIGrup.Check = true;
        int m_num;
        m_num = a_num - 1;

        Debug.Log("a_res : " + a_res);

        if (m_arithmetic_quizzes[m_num].result == a_res)    // 정답 이라면?
        {
            m_arithmetic_quizzes[m_num].winOrlose = 1;
            AddScore(4);
            Question_Finish(0);
        }
        else
        {
            m_arithmetic_quizzes[m_num].winOrlose = 0;
            Question_Finish(1);
        }
    }

    // 사칙연산 손가락 결과 결정 함수
    public void Arithmetic_Game_HandPicture(int a_num, int a_res)
    {
        AppManager.Instance.setUIGrup.Check = true;
        int m_num;
        m_num = a_num - 1;

        Debug.Log("a_res : " + a_res);

        if (m_arithmetic_quizzes[m_num].result == a_res)    // 정답 이라면?
        {
            m_arithmetic_quizzes[m_num].winOrlose = 1;
            Question_Finish(0);
            AddScore(4);
        }
        else
        {
            Question_Finish(1);
            m_arithmetic_quizzes[m_num].winOrlose = 0;
        }
    }

    // 퀴즈 생성 5개
    public void CreateQuiz_BlueFlagWhiteFlag(int a_quizCount)
    {
        // index, left, right, result, winOrlose
        m_quizCount = a_quizCount;

        m_bwFlag_quizzes.Clear();
        m_bwFlag_quizIndex.Clear();
        for (int i = 0; i < a_quizCount; i++)
        {
            flagQuizindex = UnityEngine.Random.Range(0, flagQuizText.Length); //지문 선택            
            //CompareRandomFlagName = flagQuizText[flagQuizindex].name;  // 비교할 문제 텍스트 저장 > ex) BUWU..
            Debug.Log("idx : " + (i + 1) + ", quiz : " + flagQuizText[flagQuizindex].name);
            m_bwFlag_quizIndex.Add(flagQuizindex);
            m_bwFlag_quizzes.Add(new BlueFlagAndWhiteFlag(i + 1, flagQuizText[flagQuizindex].name, 0));
        }
    }

    public void Answer_BlueFlagWhiteFlag(int a_num)
    {
        AppManager.Instance.setUIGrup.Check = true;
        int m_num;
        m_num = a_num - 1;

        string a_res;

        // 기본 BCWC
        // 정답길이가 2개일 때, 4개일 때 / 손동작은 무조건 4개로 나옴

        a_res = BLeft + WRight;

        MatchingAnswer(m_num, a_res);
    }

    private void MatchingAnswer(int a_num, string a_res)
    {
        // a_res 의 경우의 수
        // Up, Down 모든 경우와
        // BC, WC 가운데 위치했을 경우
        // 비어있지는 않음.

        // 정답비교에 수정이 필요한 경우의 수
        // BU, WU, BD, WD
        // BNWN, WNBN, BOWO, WOBO,
        // BNWU, BOWU, WUBN, WUBO,
        // BNWD, BOWD, WDBN, WDBO,
        // WNBU, WOBU, BUWN, BUWO,
        // WNBD, WOBD, BDWN, BDWO,
        // BN, BO, WN, WO
     
        if (a_res == "BCWU")
        {
            // BNWU, BOWU, WUBN, WUBO, WU
            if ( m_bwFlag_quizzes[a_num].quiz == "WU")
            {
                AddScore(4);
                m_bwFlag_quizzes[a_num].winOrlose = 0;
                Question_Finish(0);
            }
            else
            {
                m_bwFlag_quizzes[a_num].winOrlose = 1;
                Question_Finish(1);
            }
        }
        else if (a_res == "BCWD")
        {
            // BNWD, BOWD, WDBN, WDBO, WD
            if ( m_bwFlag_quizzes[a_num].quiz == "WD" )
            {
                AddScore(4);
                m_bwFlag_quizzes[a_num].winOrlose = 0;
                Question_Finish(0);
            }
            else
            {
                m_bwFlag_quizzes[a_num].winOrlose = 1;
                Question_Finish(1);
            }
        }
        else if (a_res == "BUWC")
        {
            // WNBU, WOBU, BUWN, BUWO, BU
            if ( m_bwFlag_quizzes[a_num].quiz == "BU")
            {
                AddScore(4);
                m_bwFlag_quizzes[a_num].winOrlose = 0;
                Question_Finish(0);
            }
            else
            {
                m_bwFlag_quizzes[a_num].winOrlose = 1;
                Question_Finish(1);
            }
        }
        else if (a_res == "BDWC")
        {
            // WNBD, WOBD, BDWN, BDWO, BD
            if (m_bwFlag_quizzes[a_num].quiz == "BD" )
            {
                AddScore(4);
                m_bwFlag_quizzes[a_num].winOrlose = 0;
                Question_Finish(0);
            }
            else
            {
                m_bwFlag_quizzes[a_num].winOrlose = 1;
                Question_Finish(1);
            }
        }
        //2022-06-15
        else if (a_res == "BDWD")
        {
            // BDWD, WDBD
            if (m_bwFlag_quizzes[a_num].quiz == "BDWD" || m_bwFlag_quizzes[a_num].quiz == "WDBD")
            {
                AddScore(4);
                m_bwFlag_quizzes[a_num].winOrlose = 0;
                Question_Finish(0);
            }
            else
            {
                m_bwFlag_quizzes[a_num].winOrlose = 1;
                Question_Finish(1);
            }

        }
        else if (a_res == "BDWU")
        {
            // BDWD, WDBD
            if (m_bwFlag_quizzes[a_num].quiz == "BDWU" || m_bwFlag_quizzes[a_num].quiz == "WUBD")
            {
                AddScore(4);
                m_bwFlag_quizzes[a_num].winOrlose = 0;
                Question_Finish(0);
            }
            else
            {
                m_bwFlag_quizzes[a_num].winOrlose = 1;
                Question_Finish(1);
            }
        }
        else if (a_res == "BUWD")
        {
            //BUWD , WDBU
            if (m_bwFlag_quizzes[a_num].quiz == "BUWD" || m_bwFlag_quizzes[a_num].quiz == "WDBU")
            {
                AddScore(4);
                m_bwFlag_quizzes[a_num].winOrlose = 0;
                Question_Finish(0);
            }
            else
            {
                m_bwFlag_quizzes[a_num].winOrlose = 1;
                Question_Finish(1);
            }
        }
        else if (a_res == "BUWU")
        {
            // BDWD, WDBD
            if (m_bwFlag_quizzes[a_num].quiz == "BUWU" || m_bwFlag_quizzes[a_num].quiz == "WUBU")
            {
                m_bwFlag_quizzes[a_num].winOrlose = 0;
                AddScore(4);
                Question_Finish(0);
            }
            else
            {
                m_bwFlag_quizzes[a_num].winOrlose = 1;
                Question_Finish(1);
            }
        }
        else
        {
            if (Timer.playTime <= 0)
            {
                m_bwFlag_quizzes[a_num].winOrlose = 1;
                Question_Finish(1);
            }
        }

    }
    //성공 0 실패 1
    public void Question_Finish(int clearState)
    {
        StopCoroutine("_Question_Finish");
        StartCoroutine("_Question_Finish", clearState);


    }
    IEnumerator _Question_Finish(int clearState)
    {
        Debug.Log("clearState  :" + clearState);
        if (clearState.Equals(0))
        {
            AppManager.Instance.setUIGrup.State_img.sprite = AppManager.Instance.setUIGrup.state_sp[clearState];
            SceneSoundCtrl.Instance.GameSuccessSound();
        }
        else
        {
            SceneSoundCtrl.Instance.GameFailSound();
            AppManager.Instance.setUIGrup.State_img.sprite = AppManager.Instance.setUIGrup.state_sp[clearState];
        }

        AppManager.Instance.setUIGrup.QuestionState.SetActive(true);
        yield return new WaitForSeconds(3f);
        AppManager.Instance.setUIGrup.QuestionState.SetActive(false);
        AppManager.Instance.setUIGrup.Check = false;
        yield return null;
    }

    public void AddScore(int quiz_score)
    {
        GameAppManager.instance.CurrentGameScore += quiz_score;
        AppManager.Instance.quiz_score += quiz_score;
    }
}
