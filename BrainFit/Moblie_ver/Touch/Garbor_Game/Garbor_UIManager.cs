using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Garbor_UIManager : MonoBehaviour
{
    public GameObject ChooseLevel;
    public GameObject Tutorial;

    public Text Title_text;
    public Text SubTitle_text;
    public bool GamePlay = false;
    public float StartTime = 0;//현재기록
    public float BestTime = 0; //최고기록
    int level; //단계 
    public int order = 0; //같은 짝의 갯수
    public int correctCount = 0; //맞춘 문제의 갯수
    public int Total_correctCount = 0; //맞춰야하는 전체 갯수

    [System.Serializable]
    public struct Garbor_Result
    {
        public GameObject ResultPopup_ob;
        public GameObject[] DataGrup;
        [Header("최초 기록")]
        public Text FirstData;
        [Header("최신 기록")]
        public Text BeforData;
        public Text NewData;

        [Space(10)]
        public GameObject ReStart;
        public GameObject NextLevel;
        public GameObject Exit;
    }
    [Space(10)]
    [Header("게임 관련---------------------------")]
    public GameObject[] LevelGrups;
    public Sprite[] GarborIMG_sp;
    public Sprite[] GarborState_sp;

    public List<int> GarborImg_num_list;
    public List<int> order_list;

    public List<Button> order_btn_list;
    public List<Button> order_Clear_list;
    public List<Button> order_Fail_list;


    [System.Serializable]
    public struct Garbor_Level1
    {
        public Button[] Level_Btns;
    }
    [System.Serializable]
    public struct Garbor_Level2
    {
        public Button[] Level_Btns;
    }
    [System.Serializable]
    public struct Garbor_Level3
    {
        public Button[] Level_Btns;
    }

    public Garbor_Level1 level1;
    public Garbor_Level2 level2;
    public Garbor_Level3 level3;
    public Garbor_Result result;

    private void Awake()
    {
        Garbor_PlayStart();
       // PlayerPrefs.DeleteAll();
    }
    public void Garbor_PlayStart()
    {
        if (PlayerPrefs.GetString("TutorialPlay").Equals("true"))
        {
            Tutorial.SetActive(true);
        }
        else
        {
            Tutorial.SetActive(false);
            ChooseLevel.SetActive(true);
        }
        Initialization();


    }
    void Initialization()
    {
        StartTime = 0;
        correctCount = 0;
        GarborImg_num_list.Clear();
        order_list.Clear();
        order_btn_list.Clear();
        order_Fail_list.Clear();

        if (level.Equals(1))
        {
            if (PlayerPrefs.HasKey("Garbor1_data"))
            {
                BestTime = PlayerPrefs.GetFloat("Garbor1_data");
            }
        }
        else if (level.Equals(2))
        {
            if (PlayerPrefs.HasKey("Garbor2_data"))
            {
                BestTime = PlayerPrefs.GetFloat("Garbor2_data");
            }
        }
        else if (level.Equals(3))
        {
            if (PlayerPrefs.HasKey("Garbor3_data"))
            {
                BestTime = PlayerPrefs.GetFloat("Garbor3_data");
            }
        }
    }
    void OnClick_Level_create_list(int leb)
    {
        Initialization(); //초기화
        LevelGrups[leb - 1].SetActive(true);
        level = leb;
        switch (level)
        {
            case 1:
                order = 2;
                Total_correctCount = order * 16;
                GarborImg_Count(32);
                break;

            case 2:
                order = 3;
                Total_correctCount = order * 16;
                GarborImg_Count(48);
                break;

            case 3:
                order = 4;
                Total_correctCount = order * 16;
                GarborImg_Count(64);
                break;
        }
        Title_text.text = $"같은 그림 찾기 ({level}단계)".ToString();
        SubTitle_text.text = $"같은 그림을 {order}개씩 찾아서 터치하세요.".ToString();
        StartCoroutine(_PlayStart());
    }
    IEnumerator _PlayStart()
    {

        GamePlay = true;
        while (GamePlay)
        {
            StartTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    void GarborImg_Count(int num)
    {
        int random_num;
        while (GarborImg_num_list.Count < num)
        {
            random_num = UnityEngine.Random.Range(0, GarborIMG_sp.Length);
            if (!GarborImg_num_list.Count.Equals(0))
            {
                if (!GarborImg_num_list.Contains(random_num))
                {
                    for (int i = 0; i < order; i++)
                    {
                        GarborImg_num_list.Add(random_num);
                    }
                }
            }
            else
            {
                for (int i = 0; i < order; i++)
                {
                    GarborImg_num_list.Add(random_num);
                }
            }
        }
        GameAppManager.instance.GetShuffleList(GarborImg_num_list);
        //UI 설정해주기 
        for (int j = 0; j < num; j++)
        {
            if (level.Equals(1))
            {
                level1.Level_Btns[j].gameObject.GetComponent<Image>().sprite
             = GarborIMG_sp[GarborImg_num_list[j]];
            }
            else if (level.Equals(2))
            {
                level2.Level_Btns[j].gameObject.GetComponent<Image>().sprite
         = GarborIMG_sp[GarborImg_num_list[j]];
            }
            else if (level.Equals(3))
            {
                level3.Level_Btns[j].gameObject.GetComponent<Image>().sprite
         = GarborIMG_sp[GarborImg_num_list[j]];
            }
        }
        Metch_GarborIMG();
    }

    public void Metch_GarborIMG()
    {
        StopCoroutine("_Metch_GarborIMG");
        StartCoroutine("_Metch_GarborIMG");
    }
    IEnumerator _Metch_GarborIMG()
    {
        //게임중이면
        while (correctCount != Total_correctCount)
        {
            if (order_list.Count == order)
            {
                correctCount += order;
                for (int i = 0; i < order_btn_list.Count; i++)
                {
                    //버튼의 색상 다시 돌리기
                    order_btn_list[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = GarborState_sp[2];
                    order_btn_list[i].interactable = false;
                }
                order_list.Clear();
                order_btn_list.Clear();
            }
            yield return null;
        }
        //게임끝
        Garbor_PlayFinish();
        SceneSoundCtrl.Instance.GameSuccessSound();
    }
    public void OnClick_GarborImg()
    {
        string EventButtonName = EventSystem.current.currentSelectedGameObject.name;
        Button eventButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        int EventButtonNum = int.Parse(EventButtonName);
        if (order_list.Count != 0)
        {
            if (order_list.Contains(GarborImg_num_list[EventButtonNum]))
            {
                order_list.Add(GarborImg_num_list[EventButtonNum]);
                eventButton.interactable = false;
                order_btn_list.Add(eventButton);
                eventButton.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                eventButton.gameObject.transform.GetChild(0).GetComponent<Image>().sprite
                    = GarborState_sp[1];
                SceneSoundCtrl.Instance.ButtonClick_sound();
            }
            else
            {
                SceneSoundCtrl.Instance.GameFailSound();
                eventButton.interactable = false;
                order_btn_list.Add(eventButton);
                Debug.Log("실패!!!!!!!!!!!!");
                StartCoroutine(_order_fail());
            }
        }
        else
        {
            SceneSoundCtrl.Instance.ButtonClick_sound();
            if (order_btn_list.Count != 0)
            {
                order_btn_list.Clear();
            }
            order_list.Add(GarborImg_num_list[EventButtonNum]);
            order_btn_list.Add(eventButton);
            eventButton.interactable = false;
            //버튼 색상
            eventButton.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            eventButton.gameObject.transform.GetChild(0).GetComponent<Image>().sprite
                = GarborState_sp[1];
        }
    }
    IEnumerator _order_Clear()
    {
        order_list.Clear();
        //버튼의 색상 다시 돌리기
        for (int i = 0; i < order_Fail_list.Count; i++)
        {
            order_Fail_list[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            order_Fail_list[i].interactable = true;
        }
        order_Fail_list.Clear();
        yield return null;
    }
    IEnumerator _order_fail()
    {
        order_list.Clear();
        //버튼 색상 빨강으로 바꾸기
        for (int i = 0; i < order_btn_list.Count; i++)
        {
            order_Fail_list.Add(order_btn_list[i]); //실패한 버튼 넣어줌
        }
        //실패한 버튼 색상 변경해주기
        for (int i = 0; i < order_Fail_list.Count; i++)
        {
            order_Fail_list[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            order_Fail_list[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite
                = GarborState_sp[0];
        }
        yield return new WaitForSeconds(1f);
        //버튼의 색상 다시 돌리기
        for (int i = 0; i < order_Fail_list.Count; i++)
        {
            order_Fail_list[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            order_Fail_list[i].interactable = true;
        }
        order_Fail_list.Clear();
        yield return null;
    }
   
    public void OnClick_ChooseLevel(int level)
    {
        SceneSoundCtrl.Instance.ButtonClick_sound();
        OnClick_Level_create_list(level);
        ChooseLevel.SetActive(false);
    }
    public void NextTutorial(GameObject next)
    {
        SceneSoundCtrl.Instance.ButtonClick_sound();
        next.gameObject.SetActive(true);
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
    }
    public void Garbor_PlayFinish()
    {
        AdsManager.instance.Show_AD_interstitial();
        GamePlay = false;
        result.ResultPopup_ob.SetActive(true);

        if (level.Equals(3))
        {
            result.NextLevel.SetActive(false);
        }
       
        if (level.Equals(1))
        {
            //기록 갱신
            if (!PlayerPrefs.HasKey("Garbor1_data"))
            {
                First_Data();
                PlayerPrefs.SetFloat("Garbor1_data", StartTime);//최초기록
            }
            else
            {
                Second_Data();
                if (StartTime < BestTime)
                {
                    //최고 기록 갱신
                    PlayerPrefs.SetFloat("Garbor1_data", StartTime);
                }
            }
            //
            for (int i = 0; i < level1.Level_Btns.Length; i++)
            {
                level1.Level_Btns[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                level1.Level_Btns[i].interactable = true;
            }
        }
        else if (level.Equals(2))
        {
            //기록 갱신
            if (!PlayerPrefs.HasKey("Garbor2_data"))
            {
                First_Data();
                PlayerPrefs.SetFloat("Garbor2_data", StartTime);//최초기록
            }
            else
            {
                Second_Data();
                if (StartTime < BestTime)
                {
                    //최고 기록 갱신
                    PlayerPrefs.SetFloat("Garbor2_data", StartTime);
                }
            }
            //
            for (int i = 0; i < level2.Level_Btns.Length; i++)
            {
                level2.Level_Btns[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                level2.Level_Btns[i].interactable = true;
            }
        }
        else if (level.Equals(3))
        {
            //기록 갱신
            if (!PlayerPrefs.HasKey("Garbor3_data"))
            {
                First_Data();
                PlayerPrefs.SetFloat("Garbor3_data", StartTime);//최초기록
            }
            else
            {
                Second_Data();
                if (StartTime < BestTime)
                {
                    //최고 기록 갱신
                    PlayerPrefs.SetFloat("Garbor3_data", StartTime);
                }
            }
            //
            for (int i = 0; i < level3.Level_Btns.Length; i++)
            {
                level3.Level_Btns[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                level3.Level_Btns[i].interactable = true;
            }
        }
    }
    void First_Data()
    {
        result.DataGrup[0].SetActive(true);
        result.DataGrup[1].SetActive(false);
        Split_TimeData(result.FirstData);
    }
    void Second_Data()
    {
        result.DataGrup[0].SetActive(false);
        result.DataGrup[1].SetActive(true);
        Split_TimeData(result.BeforData, result.NewData);
    }
    void Split_TimeData(Text FIRST)
    {
        string firstT_str = TimeSpan.FromSeconds(StartTime).ToString("mm\\:ss\\:ff");
        string[] firstT_list = firstT_str.Split(':');
        //최초기록
        if (!firstT_list[0].Equals("00"))
            FIRST.text = $"{firstT_list[0]}분 {firstT_list[1]}.{firstT_list[2]}초".ToString(); //최고기록
        else
            FIRST.text = $"{firstT_list[1]}.{firstT_list[2]}초".ToString(); //최고기록
    }
    void Split_TimeData(Text BEST,Text NEW)
    {
        string bestT_str = TimeSpan.FromSeconds(BestTime).ToString("mm\\:ss\\:ff");
        string[] bestT_list = bestT_str.Split(':');

        string newT_str = TimeSpan.FromSeconds(StartTime).ToString("mm\\:ss\\:ff");
        string[] newT_list = newT_str.Split(':');

        Debug.Log(bestT_list[0]);
        //최고기록
        if (!bestT_list[0].Equals("00"))
            BEST.text = $"{bestT_list[0]}분 {bestT_list[1]}.{bestT_list[2]}초".ToString(); //최고기록
        else
            BEST.text = $"{bestT_list[1]}.{newT_list[2]}초".ToString(); //최고기록
        //이번기록
        if (!newT_list[0].Equals("00"))
            NEW.text = $"{newT_list[0]}분 {newT_list[1]}초.{newT_list[2]}".ToString(); //최고기록
        else
            NEW.text = $"{newT_list[1]}.{newT_list[2]}초".ToString(); //최고기록
    }
    public void OnClick_Exit()
    {
        SceneSoundCtrl.Instance.ButtonClick_sound();
        GameAppManager.instance.LoadScene_Name("8.ChooseGame");
    }
    public void OnClick_NextLevel()
    {
        result.ResultPopup_ob.SetActive(false);
        if (level != 3)
        {
            OnClick_Level_create_list(level + 1);
        }
        SceneSoundCtrl.Instance.ButtonClick_sound();
    }
    public void OnClick_Retry()
    {
        result.ResultPopup_ob.SetActive(false);
        OnClick_Level_create_list(level);
        SceneSoundCtrl.Instance.ButtonClick_sound();
    }
}
