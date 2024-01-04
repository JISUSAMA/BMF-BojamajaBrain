using Leap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestureManager_Arithmetic : MonoBehaviour
{
    public static GestureManager_Arithmetic Instance { get; private set; }

    public bool[] L_isExtendedFinger;
    public bool[] R_isExtendedFinger;
    public bool isEnableLeftHand = false;
    public bool isEnableRightHand = false;
    [Header("GuidLine")]
    public float leftTime_left_1 = 2f;
    public float leftTime_left_2 = 2f;
    public float leftTime_left_3 = 2f;

    public GameObject[] guidLineObj;
    public TMP_Text[] guidLineText;

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

    public int cur_Game = 1;     // Current Game Number

    public int fingerCount;      // 최소 0 ~ 10 개

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

        fingerCount = 0;

        L_isExtendedFinger = Enumerable.Repeat(false, 5).ToArray();
        R_isExtendedFinger = Enumerable.Repeat(false, 5).ToArray();
    }

    // Start is called before the first frame update
    void Start()
    {

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
    }

    public void SetExtendedGesture(string _finger)
    {
        switch (_finger)
        {
            case "L_THUMB":
#if ENABLE_LOG
                l_thumb.text = "왼손엄지핌";
#endif
                //L_isExtendedThumb = true;
                L_isExtendedFinger[0] = true;
               // L_isExtendedFinger_str[0] = "true";
                break;
            case "L_INDEX":
#if ENABLE_LOG
                l_index.text = "왼손검지핌";
#endif
                //L_isExtendedIndex = true;
                L_isExtendedFinger[1] = true;
              //  L_isExtendedFinger_str[1] = "true";
                break;
            case "L_MIDDLE":
#if ENABLE_LOG
                l_middle.text = "왼손중지핌";
#endif
                //L_isExtendedMiddle = true;
                L_isExtendedFinger[2] = true;
              //  L_isExtendedFinger_str[2] = "true";
                break;
            case "L_RING":
#if ENABLE_LOG
                l_ring.text = "왼손약지핌";
#endif
                //L_isExtendedRing = true;
                L_isExtendedFinger[3] = true;
              //  L_isExtendedFinger_str[3] = "true";
                break;
            case "L_PINKY":
#if ENABLE_LOG
                l_pinky.text = "왼손소지핌";
#endif
                //L_isExtendedPinky = true;
                L_isExtendedFinger[4] = true;
               // L_isExtendedFinger_str[4] = "true";
                break;
            /////////////////////////////////
            case "R_THUMB":
#if ENABLE_LOG
                r_thumb.text = "오른손엄지핌";
#endif
                //R_isExtendedThumb = true;
                R_isExtendedFinger[0] = true;
              //  R_isExtendedFinger_str[0] = "true";
                break;
            case "R_INDEX":
#if ENABLE_LOG
                r_index.text = "오른손검지핌";
#endif
                //R_isExtendedIndex = true;
                R_isExtendedFinger[1] = true;
               // R_isExtendedFinger_str[1] = "true";
                break;
            case "R_MIDDLE":
#if ENABLE_LOG
                r_middle.text = "오른손중지핌";
#endif
                //R_isExtendedMiddle = true;
                R_isExtendedFinger[2] = true;
                //R_isExtendedFinger_str[2] = "true";
                break;
            case "R_RING":
#if ENABLE_LOG
                r_ring.text = "오른손약지핌";
#endif
                //R_isExtendedRing = true;
                R_isExtendedFinger[3] = true;
              //  R_isExtendedFinger_str[3] = "true";
                break;
            case "R_PINKY":
#if ENABLE_LOG
                r_pinky.text = "오른손소지핌";
#endif
                //R_isExtendedPinky = true;
                R_isExtendedFinger[4] = true;
                //R_isExtendedFinger_str[4] = "true";
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
                //L_isExtendedThumb = false;
                L_isExtendedFinger[0] = false;
               // L_isExtendedFinger_str[0] = "false";
                break;
            case "L_INDEX":
#if ENABLE_LOG
                l_index.text = "왼손검지접음";
#endif
                //L_isExtendedIndex = false;
                L_isExtendedFinger[1] = false;
              //  L_isExtendedFinger_str[1] = "false";
                break;
            case "L_MIDDLE":
#if ENABLE_LOG
                l_middle.text = "왼손중지접음";
#endif
                //L_isExtendedMiddle = false;
                L_isExtendedFinger[2] = false;
               // L_isExtendedFinger_str[2] = "false";
                break;
            case "L_RING":
#if ENABLE_LOG
                l_ring.text = "왼손약지접음";
#endif
                //L_isExtendedRing = false;
                L_isExtendedFinger[3] = false;
               // L_isExtendedFinger_str[3] = "false";
                break;
            case "L_PINKY":
#if ENABLE_LOG
                l_pinky.text = "왼손소지접음";
#endif
                //L_isExtendedPinky = false;
                L_isExtendedFinger[4] = false;
               // L_isExtendedFinger_str[4] = "false";
                break;

            /////////////////////////////////
            case "R_THUMB":
#if ENABLE_LOG
                r_thumb.text = "오른손엄지접음";
#endif
                //R_isExtendedThumb = false;
                R_isExtendedFinger[0] = false;
              //  R_isExtendedFinger_str[0] = "false";
                break;
            case "R_INDEX":
#if ENABLE_LOG
                r_index.text = "오른손검지접음";
#endif
                //R_isExtendedIndex = false;
                R_isExtendedFinger[1] = false;
              //  R_isExtendedFinger_str[1] = "false";
                break;
            case "R_MIDDLE":
#if ENABLE_LOG
                r_middle.text = "오른손중지접음";
#endif
                //R_isExtendedMiddle = false;
                R_isExtendedFinger[2] = false;
                //R_isExtendedFinger_str[2] = "false";
                break;
            case "R_RING":
#if ENABLE_LOG
                r_ring.text = "오른손약지접음";
#endif
                //R_isExtendedRing = false;
                R_isExtendedFinger[3] = false;
                //R_isExtendedFinger_str[3] = "false";
                break;
            case "R_PINKY":
#if ENABLE_LOG
                r_pinky.text = "오른손소지접음";
#endif
                //R_isExtendedPinky = false;
                R_isExtendedFinger[4] = false;
                //R_isExtendedFinger_str[4] = "false";
                break;
        }
    }
}
