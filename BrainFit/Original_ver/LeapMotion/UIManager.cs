using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public Text DiscreiptionGame_text;
    public Image Left_Select, Right_Select;
    public Sprite[] Left_Right_Select;

    [Header("ArithmeticGame Numbers")]
    [SerializeField] private TMP_Text idx;
    [SerializeField] private TMP_Text num1;
    [SerializeField] private TMP_Text num2;
    [SerializeField] private TMP_Text oper;

    [Header("RPS Game")]
    [SerializeField] private TMP_Text rps_idx;
    [SerializeField] private TMP_Text rps_quiz_text;

    public Image Computer_Quiz_img;
    public Image Player_Quiz_img;
    public Sprite[] Computer_Quiz_sp;
    public Sprite[] Player_Quiz_sp;
    [Header("ARITHMETIC Game")]
    public Image PlayerAnswer_img;
    public Sprite []PlayerAnswer_sp;
    
    public TMP_Text PlayerAnswer_text;

    [Header("Flag Game")]
    [SerializeField] private TMP_Text flag_idx;
    public Image BlueFlag_img;
    public Sprite[] BlueFlag_sp;
    public Image WhiteFlag_img;
    public Sprite[] WhiteFlag_sp;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }
    public void RPS_Game_Win(int a_quizCount)
    {
        StartCoroutine(_RPS_Game_Win(a_quizCount));
    }

    IEnumerator _RPS_Game_Win(int a_quizCount)
    {
        DiscreiptionGame_text.text = "왼쪽과 오른쪽 중 더 편한 손으로 가위바위보에 사용 할 손을 골라주세요.";
        // 게임이 시작이 되면 넘어간다. 그전까지 계속 걸림
        yield return new WaitUntil(() => AppManager.Instance.gameRunning.Equals(true));
        DiscreiptionGame_text.text = "화면을 확인하고 가위, 바위, 보 중 하나를 내서 이겨보세요.";
        while (Timer.gameCount < a_quizCount)
        {
            rps_idx.text = QuizManager.Instance.m_quizzes[Timer.gameCount].num.ToString();
            rps_quiz_text.text = QuizManager.Instance.m_quizzes[Timer.gameCount].quiz == 1 ? "가위" :
                QuizManager.Instance.m_quizzes[Timer.gameCount].quiz == 2 ? "바위" :
                    QuizManager.Instance.m_quizzes[Timer.gameCount].quiz == 3 ? "보" : "저장이 잘못됌";
            Computer_Quiz_img.sprite = Computer_Quiz_sp[QuizManager.Instance.m_quizzes[Timer.gameCount].quiz - 1];
            yield return null;
        }

        yield return null;
    }

    public void RPS_Game_Lose(int a_quizCount)
    {
        StartCoroutine(_RPS_Game_Lose(a_quizCount));
    }

    IEnumerator _RPS_Game_Lose(int a_quizCount)
    {

        DiscreiptionGame_text.text = "왼쪽과 오른쪽 중 더 편한 손으로 가위바위보에 사용 할 손을 골라주세요.";
        // 게임이 시작이 되면 넘어간다. 그전까지 계속 걸림
        yield return new WaitUntil(() => AppManager.Instance.gameRunning.Equals(true));
        DiscreiptionGame_text.text = "화면을 확인하고 가위, 바위, 보 중 하나를 내서 져보세요.";
        while (Timer.gameCount < a_quizCount)
        {
            rps_idx.text = QuizManager.Instance.m_quizzes[Timer.gameCount].num.ToString();
            rps_quiz_text.text = QuizManager.Instance.m_quizzes[Timer.gameCount].quiz == 1 ? "가위" :
                QuizManager.Instance.m_quizzes[Timer.gameCount].quiz == 2 ? "바위" :
                    QuizManager.Instance.m_quizzes[Timer.gameCount].quiz == 3 ? "보" : "저장이 잘못됌";
            Computer_Quiz_img.sprite = Computer_Quiz_sp[QuizManager.Instance.m_quizzes[Timer.gameCount].quiz - 1];
            yield return null;
        }

        yield return null;

        yield return null;
    }
    public void ChangeIMG_ForGame()
    {
        StopCoroutine(_ChangeIMG_ForGame());
        StartCoroutine(_ChangeIMG_ForGame());
    }
    IEnumerator _ChangeIMG_ForGame()
    {
        Computer_Quiz_img.gameObject.SetActive(true);
        Player_Quiz_img.gameObject.SetActive(true);
        yield return new WaitUntil(() => AppManager.Instance.gameRunning == true);
        while (AppManager.Instance.gameRunning)
        {
            Debug.Log("m_quizzes1111111111111111111111111111111 : ");
            //바위
            if (QuizManager.Instance.m_quizzes[QuizManager.Instance.m_quizCount - 1].Equals(0))
            {
                Computer_Quiz_img.sprite = Computer_Quiz_sp[0];
            }
            //가위
            else if (QuizManager.Instance.m_quizzes[QuizManager.Instance.m_quizCount - 1].Equals(1))
            {
                Computer_Quiz_img.sprite = Computer_Quiz_sp[1];
            }
            //보
            else if (QuizManager.Instance.m_quizzes[QuizManager.Instance.m_quizCount - 1].Equals(2))
            {
                Computer_Quiz_img.sprite = Computer_Quiz_sp[2];
            }



            yield return null;
        }
        yield return null;
    }
    public void ArithmeticGameStart_HandPicture(int a_quizCount)
    {
        StartCoroutine(_ArithmeticGameStart_HandPicture(a_quizCount));
    }

    IEnumerator _ArithmeticGameStart_HandPicture(int a_quizCount)
    {
        // 게임이 시작이 되면 넘어간다. 그전까지 계속 걸림
        yield return new WaitUntil(() => AppManager.Instance.gameRunning.Equals(true));

        while (Timer.gameCount < a_quizCount)
        {
        //    idx.text = QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].index.ToString();

            ShowFingerImage();

            oper.text = QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].operation == 1 ? "+" :
                QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].operation == 2 ? "-" :
                    QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].operation == 3 ? "×" :
                        QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].operation == 4 ? "÷" : "none";

            yield return null;
        }

        //idx.text = "";
        //num1.text = "";
        //num2.text = "";
        //oper.text = "";

        yield return null;
    }
    public void ChangeText_GestureState()
    {
        StopCoroutine(_ChangeText_GestureState());
        StartCoroutine(_ChangeText_GestureState());
    }
    IEnumerator _ChangeText_GestureState()
    {
        yield return new WaitUntil(() => AppManager.Instance.gameRunning == true);
        while (AppManager.Instance.gameRunning)
        {
           // Debug.Log(GestureManager_Arithmetic.Instance.L_isExtendedFinger.Count(c=>c));
           // Debug.Log(GestureManager_Arithmetic.Instance.R_isExtendedFinger.Count(c=>c));
            int Temp_finger = GestureManager_Arithmetic.Instance.L_isExtendedFinger.Count(c => c)
                + GestureManager_Arithmetic.Instance.R_isExtendedFinger.Count(c => c);
            //Debug.Log("Temp_finger : "+ Temp_finger+ "leftFingerCount :"+ leftFingerCount+ "rigthFingerCount :"+ rigthFingerCount);
            //손가락 갯수에 따라서 이미지 변경
            if (!Temp_finger.Equals(0))
            {
                PlayerAnswer_text.text = Temp_finger.ToString();
                PlayerAnswer_img.sprite = PlayerAnswer_sp[1];
            }
            else
            {
                if(!GestureManager_Arithmetic.Instance.isEnableRightHand && !GestureManager_Arithmetic.Instance.isEnableLeftHand)
                {
                    PlayerAnswer_text.text = "";
                    PlayerAnswer_img.sprite = PlayerAnswer_sp[0];
                }
                else
                {
                    PlayerAnswer_text.text = Temp_finger.ToString();
                    PlayerAnswer_img.sprite = PlayerAnswer_sp[1];
                }
            }
            yield return null;

        }
    }

    private void ShowFingerImage()
    {
        // num1
        if (QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num1 == 0)
        {
            if (!QuizManager.Instance.L_finger[0].activeSelf)
            {
                QuizManager.Instance.L_finger[0].SetActive(true);
                QuizManager.Instance.L_finger[1].SetActive(false);
                QuizManager.Instance.L_finger[2].SetActive(false);
                QuizManager.Instance.L_finger[3].SetActive(false);
                QuizManager.Instance.L_finger[4].SetActive(false);
                QuizManager.Instance.L_finger[5].SetActive(false);
            }
        }
        else if(QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num1 == 1)
        {
            if (!QuizManager.Instance.L_finger[1].activeSelf)
            {
                QuizManager.Instance.L_finger[0].SetActive(false);
                QuizManager.Instance.L_finger[1].SetActive(true);
                QuizManager.Instance.L_finger[2].SetActive(false);
                QuizManager.Instance.L_finger[3].SetActive(false);
                QuizManager.Instance.L_finger[4].SetActive(false);
                QuizManager.Instance.L_finger[5].SetActive(false);
            }
        }
        else if (QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num1 == 2)
        {
            if (!QuizManager.Instance.L_finger[2].activeSelf)
            {
                QuizManager.Instance.L_finger[0].SetActive(false);
                QuizManager.Instance.L_finger[1].SetActive(false);
                QuizManager.Instance.L_finger[2].SetActive(true);
                QuizManager.Instance.L_finger[3].SetActive(false);
                QuizManager.Instance.L_finger[4].SetActive(false);
                QuizManager.Instance.L_finger[5].SetActive(false);
            }
        }
        else if (QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num1 == 3)
        {
            if (!QuizManager.Instance.L_finger[3].activeSelf)
            {
                QuizManager.Instance.L_finger[0].SetActive(false);
                QuizManager.Instance.L_finger[1].SetActive(false);
                QuizManager.Instance.L_finger[2].SetActive(false);
                QuizManager.Instance.L_finger[3].SetActive(true);
                QuizManager.Instance.L_finger[4].SetActive(false);
                QuizManager.Instance.L_finger[5].SetActive(false);
            }
        }
        else if (QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num1 == 4)
        {
            if (!QuizManager.Instance.L_finger[4].activeSelf)
            {
                QuizManager.Instance.L_finger[0].SetActive(false);
                QuizManager.Instance.L_finger[1].SetActive(false);
                QuizManager.Instance.L_finger[2].SetActive(false);
                QuizManager.Instance.L_finger[3].SetActive(false);
                QuizManager.Instance.L_finger[4].SetActive(true);
                QuizManager.Instance.L_finger[5].SetActive(false);
            }
        }
        else if (QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num1 == 5)
        {
            if (!QuizManager.Instance.L_finger[5].activeSelf)
            {
                QuizManager.Instance.L_finger[0].SetActive(false);
                QuizManager.Instance.L_finger[1].SetActive(false);
                QuizManager.Instance.L_finger[2].SetActive(false);
                QuizManager.Instance.L_finger[3].SetActive(false);
                QuizManager.Instance.L_finger[4].SetActive(false);
                QuizManager.Instance.L_finger[5].SetActive(true);
            }
        }
   
        // num2
        if (QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num2 == 0)
        {
            if (!QuizManager.Instance.R_finger[0].activeSelf)
            {
                QuizManager.Instance.R_finger[0].SetActive(true);
                QuizManager.Instance.R_finger[1].SetActive(false);
                QuizManager.Instance.R_finger[2].SetActive(false);
                QuizManager.Instance.R_finger[3].SetActive(false);
                QuizManager.Instance.R_finger[4].SetActive(false);
                QuizManager.Instance.R_finger[5].SetActive(false);
            }
        }
        else if (QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num2 == 1)
        {
            if (!QuizManager.Instance.R_finger[1].activeSelf)
            {
                QuizManager.Instance.R_finger[0].SetActive(false);
                QuizManager.Instance.R_finger[1].SetActive(true);
                QuizManager.Instance.R_finger[2].SetActive(false);
                QuizManager.Instance.R_finger[3].SetActive(false);
                QuizManager.Instance.R_finger[4].SetActive(false);
                QuizManager.Instance.R_finger[5].SetActive(false);

            }
        }
        else if (QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num2 == 2)
        {
            if (!QuizManager.Instance.R_finger[2].activeSelf)
            {
                QuizManager.Instance.R_finger[0].SetActive(false);
                QuizManager.Instance.R_finger[1].SetActive(false);
                QuizManager.Instance.R_finger[2].SetActive(true);
                QuizManager.Instance.R_finger[3].SetActive(false);
                QuizManager.Instance.R_finger[4].SetActive(false);
                QuizManager.Instance.R_finger[5].SetActive(false);

            }
        }
        else if (QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num2 == 3)
        {
            if (!QuizManager.Instance.R_finger[3].activeSelf)
            {
                QuizManager.Instance.R_finger[0].SetActive(false);
                QuizManager.Instance.R_finger[1].SetActive(false);
                QuizManager.Instance.R_finger[2].SetActive(false);
                QuizManager.Instance.R_finger[3].SetActive(true);
                QuizManager.Instance.R_finger[4].SetActive(false);
                QuizManager.Instance.R_finger[5].SetActive(false);
            }
        }
        else if (QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num2 == 4)
        {
            if (!QuizManager.Instance.R_finger[4].activeSelf)
            {
                QuizManager.Instance.R_finger[0].SetActive(false);
                QuizManager.Instance.R_finger[1].SetActive(false);
                QuizManager.Instance.R_finger[2].SetActive(false);
                QuizManager.Instance.R_finger[3].SetActive(false);
                QuizManager.Instance.R_finger[4].SetActive(true);
                QuizManager.Instance.R_finger[5].SetActive(false);
            }
        }
        else if (QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num2 == 5)
        {
            if (!QuizManager.Instance.R_finger[5].activeSelf)
            {
                QuizManager.Instance.R_finger[0].SetActive(false);
                QuizManager.Instance.R_finger[1].SetActive(false);
                QuizManager.Instance.R_finger[2].SetActive(false);
                QuizManager.Instance.R_finger[3].SetActive(false);
                QuizManager.Instance.R_finger[4].SetActive(false);
                QuizManager.Instance.R_finger[5].SetActive(true);
            }
        }
    }
    public void ArithmeticGameStart_Numbers(int a_quizCount)
    {
        StartCoroutine(_ArithmeticGameStart_Numbers(a_quizCount));
    }

    IEnumerator _ArithmeticGameStart_Numbers(int a_quizCount)
    {
        // 게임이 시작이 되면 넘어간다. 그전까지 계속 걸림
        yield return new WaitUntil(() => AppManager.Instance.gameRunning.Equals(true));

        // 문제 갯수로 확인하는게 좋을듯..
        while (Timer.gameCount < a_quizCount)   // 0 ~ 5
        {
            idx.text = QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].index.ToString();
            num1.text = QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num1.ToString();
            num2.text = QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].num2.ToString();

            oper.text = QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].operation == 1 ? "+" :
                QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].operation == 2 ? "-" :
                    QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].operation == 3 ? "×" :
                        QuizManager.Instance.m_arithmetic_quizzes[Timer.gameCount].operation == 4 ? "÷" : "none";

            yield return null;
        }

        idx.text = "";
        num1.text = "";
        num2.text = "";
        oper.text = "";

        yield return null;
    }

    public void AskQuiz_BlueFlagWhiteFlag(int a_quizCount)
    {
        StartCoroutine(_AskQuiz_BlueFlagWhiteFlag(a_quizCount));
    }

    IEnumerator _AskQuiz_BlueFlagWhiteFlag(int a_quizCount)
    {
        // 게임이 시작이 되면 넘어간다. 그전까지 계속 걸림
        yield return new WaitUntil(() => AppManager.Instance.gameRunning.Equals(true));

        // 문제 갯수로 확인
        while (Timer.gameCount < a_quizCount)   // 0 ~ 5
        {
            flag_idx.text = QuizManager.Instance.m_bwFlag_quizzes[Timer.gameCount].index.ToString();

            // 현재 문제 활성화 중이면 스킵
            if (QuizManager.Instance.flagQuizText[QuizManager.Instance.m_bwFlag_quizIndex[Timer.gameCount]].activeSelf)
            {
                yield return null;
                continue;
            }
            else
            {
                if (Timer.gameCount != 0)
                {
                    QuizManager.Instance.flagQuizText[QuizManager.Instance.m_bwFlag_quizIndex[Timer.gameCount - 1]].SetActive(false);
                }

                QuizManager.Instance.flagQuizText[QuizManager.Instance.m_bwFlag_quizIndex[Timer.gameCount]].SetActive(true);
                yield return null;
            }

            yield return null;
        }

        QuizManager.Instance.flagQuizText[QuizManager.Instance.m_bwFlag_quizIndex[Timer.gameCount - 1]].SetActive(false);

        yield return null;
    }
}
