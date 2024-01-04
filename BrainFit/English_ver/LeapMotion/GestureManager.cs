using Leap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestureManager : MonoBehaviour
{
    public static GestureManager Instance { get; private set; }

    private bool L_isExtendedThumb;
    private bool L_isExtendedIndex;
    private bool L_isExtendedMiddle;
    private bool L_isExtendedRing;
    private bool L_isExtendedPinky;

    private bool R_isExtendedThumb;
    private bool R_isExtendedIndex;
    private bool R_isExtendedMiddle;
    private bool R_isExtendedRing;
    private bool R_isExtendedPinky;

    bool adaptation_time = true;
    bool protection_time = true;

    [Header("GuidLine")]
    public float leftTime_left_1 = 2f;
    public float leftTime_left_2 = 2f;
    public float leftTime_left_3 = 2f;

    //public GameObject[] guidLineObj;
    //public TMP_Text[] guidLineText;

#if ENABLE_LOG
    public TMP_Text l_thumb;
    public TMP_Text l_index;
    public TMP_Text l_middle;
    public TMP_Text l_ring;
    public TMP_Text l_pinky;

    public TMP_Text r_thumb;
    public TMP_Text r_index;
    public TMP_Text r_middle;
    public TMP_Text r_ring;
    public TMP_Text r_pinky;
#endif

    public bool isSelectedLeftHand = false;
    public bool isSelectedRightHand = false;
    public bool isEnableLeftHand = false;
    public bool isEnableRightHand = false;

    public GameObject lefthand;
    public GameObject righthand;

    SkinnedMeshRenderer lefthand_renderer;
    SkinnedMeshRenderer righthand_renderer;

    //private int prev_Game = 0; // Complete Game Number
    public int cur_Game = 1;     // Current Game Number
    public int rps = -99;        // 가위,바위,보 정보

    enum Option
    {
        EMPTY,
        SCISSORS,
        ROCK,
        PAPER
    }

    public int RPS
    {
        get
        {
            return rps;
        }
        set
        {
            rps = value;
        }
    }
    public int GameCount
    {
        get
        {
            return cur_Game;
        }
        set
        {
            cur_Game = value;
        }
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;

        lefthand_renderer = lefthand.GetComponent<SkinnedMeshRenderer>();
        righthand_renderer = righthand.GetComponent<SkinnedMeshRenderer>();
        lefthand_renderer.material.shader = Shader.Find("Standard");
        righthand_renderer.material.shader = Shader.Find("Standard");

        lefthand_renderer.material.SetFloat("_Mode", 0.0f);    // Opaque
        righthand_renderer.material.SetFloat("_Mode", 0.0f);   // Opaque
    }

    void Start()
    {
        SceneManager.sceneLoaded += LoadsceneEvent;
    }
    private void LoadsceneEvent(Scene sceen, LoadSceneMode mode)
    {
       // Debug.Log("씬 바뀜");
        isSelectedLeftHand = false;
        isSelectedRightHand = false;
        isEnableLeftHand = false;
        isEnableRightHand = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!AppManager.Instance.gameRunning.Equals(true)) { return; }  // 게임시작

        if (QuizManager.Instance.m_quizCount <= GameCount)
        {
            GameCount = 1;
            AppManager.Instance.OnRoundEnd();
            return;
        }

        //if (adaptation_time)    // 적응시간
        //{
        //    if (protection_time)
        //    {
        //        protection_time = false;
        //        StopAllCoroutines();
        //        StartCoroutine(_Adaptation_Time(2f)); // adaptation_time = false 동작
        //    }
        //    return;
        //}

        // 나중에 문제에 타이머 주기 : 30초든 40초든 커스텀으로
        // 아니면 한 문제당 5초? 주기

        if (isEnableLeftHand && isSelectedLeftHand)
        {
            #region 왼손
            // 왼손!!
            // =========================================================================================================
            // 가위!
            if (L_isExtendedThumb && L_isExtendedIndex && !L_isExtendedMiddle && !L_isExtendedRing && !L_isExtendedPinky)
            {
                Debug.Log("왼손 가위1!!");
                //leftTime_left_1 -= Time.deltaTime * 0.8f;
                //guidLineText[0].text = "왼손 가위!!";/* + leftTime_left_1.ToString("N0") + "초";*/
                UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[0];
                //guidLineObj[0].SetActive(true);
          
                //if (leftTime_left_1 < 0f)
                //{
                //    leftTime_left_1 = 2f;
                //    guidLineObj[0].SetActive(false);
                //    adaptation_time = true; // 적응시간

                //    QuizManager.Instance.WinGame(cur_Game++, (int)Option.SCISSORS);
                //}

                // 가위 결정 : 현재 가위바위보 중에 경우의 수
                RPS = (int)Option.SCISSORS;
            }// 가위2!
            else if (!L_isExtendedThumb && L_isExtendedIndex && L_isExtendedMiddle && !L_isExtendedRing && !L_isExtendedPinky)
            {
                Debug.Log("왼손 가위2!!");
                //guidLineText[0].text = "왼손 가위2!!";
                UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[0];
               // guidLineObj[0].SetActive(true);
            
                RPS = (int)Option.SCISSORS;
            }
            else
            {
                Debug.Log("왼손가위없음");
               // UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[3];
               // guidLineObj[0].SetActive(false);
             
                //leftTime_left_1 = 2f;
            }

            // 바위!
            if (!L_isExtendedThumb && !L_isExtendedIndex && !L_isExtendedMiddle && !L_isExtendedRing && !L_isExtendedPinky)
            {
                Debug.Log("왼손 바위!!");
                //leftTime_left_2 -= Time.deltaTime * 0.8f;
                //guidLineText[1].text = "왼손 바위!!";/* + leftTime_left_2.ToString("N0") + "초";*/
                UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[1];
                // 엄검소지 펴고, 나머지접음
                // 2초 지속시간 딜레이
                //guidLineObj[1].SetActive(true);
                //if (leftTime_left_2 < 0f)
                //{
                //    leftTime_left_2 = 2f;
                //    guidLineObj[1].SetActive(false);
                //    adaptation_time = true; // 적응시간

                //    QuizManager.Instance.WinGame(cur_Game++, (int)Option.SCISSORS);
                //}
                RPS = (int)Option.ROCK;
            }
            else
            {
                Debug.Log("왼손바위없음");
              //  UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[3];
               // guidLineObj[1].SetActive(false);
                //leftTime_left_2 = 2f;
            }

            // 보!
            if (L_isExtendedThumb && L_isExtendedIndex && L_isExtendedMiddle && L_isExtendedRing && L_isExtendedPinky)
            {
                Debug.Log("왼손 보!");
                //leftTime_left_3 -= Time.deltaTime * 0.8f;
                // guidLineObj[2].SetActive(true);
                UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[2];
               // guidLineText[2].text = "왼손 보!!";/* + leftTime_left_3.ToString("N0") + "초";*/

                //if (leftTime_left_3 < 0f)
                //{
                //    leftTime_left_3 = 2f;
                //    guidLineObj[2].SetActive(false);
                //    adaptation_time = true; // 적응시간

                //    QuizManager.Instance.WinGame(cur_Game++, (int)Option.SCISSORS);
                //}
                RPS = (int)Option.PAPER;
            }
            else
            {
                Debug.Log("왼손보없음");
              //  UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[3];
                //guidLineObj[2].SetActive(false);
                //leftTime_left_3 = 2f;
            }
            ////==============================================================================================================
            #endregion
        }
        else if (!isEnableLeftHand && isSelectedLeftHand)
        {
            Debug.Log("왼손선택후 손이없음");
            UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[3];
            RPS = (int)Option.EMPTY;
        }

        if (isEnableRightHand && isSelectedRightHand)
        {
            #region 오른손
            // 오른손!!
            //==============================================================================================================
            // 가위!!
            if (R_isExtendedThumb && R_isExtendedIndex && !R_isExtendedMiddle && !R_isExtendedRing && !R_isExtendedPinky)
            {
                //leftTime_left_1 -= Time.deltaTime * 0.8f;
                //guidLineObj[0].SetActive(true);
                //guidLineText[0].text = "오른손 가위!!";
                UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[0];
                //if (leftTime_left_1 < 0f)
                //{
                //    leftTime_left_1 = 2f;
                //    guidLineObj[0].SetActive(false);
                //    adaptation_time = true; // 적응시간

                //    QuizManager.Instance.WinGame(cur_Game++, (int)Option.SCISSORS);
                //}
                RPS = (int)Option.SCISSORS;
            }
            else if (!R_isExtendedThumb && R_isExtendedIndex && R_isExtendedMiddle && !R_isExtendedRing && !R_isExtendedPinky)
            {
               // guidLineObj[0].SetActive(true);
               // guidLineText[0].text = "오른손 가위!!";
                UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[0];
                RPS = (int)Option.SCISSORS;
            }
            else
            {
                Debug.Log("오른손가위없음");
                //guidLineObj[0].SetActive(false);
                //UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[3];
                //leftTime_left_1 = 2f;
            }

            // 바위!
            if (!R_isExtendedThumb && !R_isExtendedIndex && !R_isExtendedMiddle && !R_isExtendedRing && !R_isExtendedPinky)
            {
                //leftTime_left_2 -= Time.deltaTime * 0.8f;
                //guidLineObj[1].SetActive(true);
                //guidLineText[1].text = "오른손 바위!!";
                UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[1];

                //if (leftTime_left_2 < 0f)
                //{
                //    leftTime_left_2 = 2f;
                //    guidLineObj[1].SetActive(false);
                //    adaptation_time = true; // 적응시간

                //    QuizManager.Instance.WinGame(cur_Game++, (int)Option.SCISSORS);
                //}
                RPS = (int)Option.ROCK;
            }
            else
            {
                Debug.Log("오른손바위없음");
               // UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[3];
               // guidLineObj[1].SetActive(false);
                //leftTime_left_2 = 2f;
            }

            // 보!
            if (R_isExtendedThumb && R_isExtendedIndex && R_isExtendedMiddle && R_isExtendedRing && R_isExtendedPinky)
            {
                //leftTime_left_3 -= Time.deltaTime * 0.8f;
                //guidLineObj[2].SetActive(true);
                //guidLineText[2].text = "오른손 보!!";
                UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[2];
                //if (leftTime_left_3 < 0f)
                //{
                //    leftTime_left_3 = 2f;
                //    guidLineObj[2].SetActive(false);
                //    adaptation_time = true; // 적응시간

                //    QuizManager.Instance.WinGame(cur_Game++, (int)Option.SCISSORS);
                //}
                RPS = (int)Option.PAPER;
            }
            else
            {
                Debug.Log("오른손보없음");
               // guidLineObj[2].SetActive(false);
               // UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[3];
                //leftTime_left_3 = 2f;
            }
            #endregion
        }
        else if (!isEnableRightHand && isSelectedRightHand)
        {
            Debug.Log("오른손선택후 손이없음");
            UIManager.Instance.Player_Quiz_img.sprite = UIManager.Instance.Player_Quiz_sp[3];
            RPS = (int)Option.EMPTY;
        }
    }
    //IEnumerator _Adaptation_Time(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    adaptation_time = false;
    //    protection_time = true;
    //}

    public void SetExtendedGesture(string _finger)
    {
        switch (_finger)
        {
            case "L_THUMB":
#if ENABLE_LOG
                l_thumb.text = "왼손엄지핌";
#endif
                L_isExtendedThumb = true;
                break;
            case "L_INDEX":
#if ENABLE_LOG
                l_index.text = "왼손검지핌";
#endif
                L_isExtendedIndex = true;
                break;
            case "L_MIDDLE":
#if ENABLE_LOG
                l_middle.text = "왼손중지핌";
#endif
                L_isExtendedMiddle = true;
                break;
            case "L_RING":
#if ENABLE_LOG
                l_ring.text = "왼손약지핌";
#endif
                L_isExtendedRing = true;
                break;
            case "L_PINKY":
#if ENABLE_LOG
                l_pinky.text = "왼손소지핌";
#endif
                L_isExtendedPinky = true;
                break;
            /////////////////////////////////
            case "R_THUMB":
#if ENABLE_LOG
                r_thumb.text = "오른손엄지핌";
#endif
                R_isExtendedThumb = true;
                break;
            case "R_INDEX":
#if ENABLE_LOG
                r_index.text = "오른손검지핌";
#endif
                R_isExtendedIndex = true;
                break;
            case "R_MIDDLE":
#if ENABLE_LOG
                r_middle.text = "오른손중지핌";
#endif
                R_isExtendedMiddle = true;
                break;
            case "R_RING":
#if ENABLE_LOG
                r_ring.text = "오른손약지핌";
#endif
                R_isExtendedRing = true;
                break;
            case "R_PINKY":
#if ENABLE_LOG
                r_pinky.text = "오른손소지핌";
#endif
                R_isExtendedPinky = true;
                break;
        }
    }

    public void SetFoldGesture(string _finger)
    {
        switch (_finger)
        {
            case "L_THUMB":
#if ENABLE_LOG
                l_thumb.text = "왼손엄지접음";
#endif
                L_isExtendedThumb = false;
                break;
            case "L_INDEX":
#if ENABLE_LOG
                l_index.text = "왼손검지접음";
#endif
                L_isExtendedIndex = false;
                break;
            case "L_MIDDLE":
#if ENABLE_LOG
                l_middle.text = "왼손중지접음";
#endif
                L_isExtendedMiddle = false;
                break;
            case "L_RING":
#if ENABLE_LOG
                l_ring.text = "왼손약지접음";
#endif
                L_isExtendedRing = false;
                break;
            case "L_PINKY":
#if ENABLE_LOG
                l_pinky.text = "왼손소지접음";
#endif
                L_isExtendedPinky = false;
                break;

            /////////////////////////////////
            case "R_THUMB":
#if ENABLE_LOG
                r_thumb.text = "오른손엄지접음";
#endif
                R_isExtendedThumb = false;
                break;
            case "R_INDEX":
#if ENABLE_LOG
                r_index.text = "오른손검지접음";
#endif
                R_isExtendedIndex = false;
                break;
            case "R_MIDDLE":
#if ENABLE_LOG
                r_middle.text = "오른손중지접음";
#endif
                R_isExtendedMiddle = false;
                break;
            case "R_RING":
#if ENABLE_LOG
                r_ring.text = "오른손약지접음";
#endif
                R_isExtendedRing = false;
                break;
            case "R_PINKY":
#if ENABLE_LOG
                r_pinky.text = "오른손소지접음";
#endif
                R_isExtendedPinky = false;
                break;
        }
    }
    public string GetSelectedHand()
    {
        if (isSelectedLeftHand)
        {
            return "왼손";
        }
        else if (isSelectedRightHand)
        {
            return "오른손";
        }
        else
        {
            return "선택되지않았음";
        }
    }


}
