using Leap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestureManager_Flag : MonoBehaviour
{
    public static GestureManager_Flag Instance { get; private set; }

    //public GameObject[] guidLineObj;
    public TMP_Text[] guidLineText;

    public int cur_Game = 1;     // Current Game Number

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
}
