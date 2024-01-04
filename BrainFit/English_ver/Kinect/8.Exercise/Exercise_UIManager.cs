using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class Exercise_UIManager : MonoBehaviour
{
    public static Exercise_UIManager instance { get; private set; }

    public GameObject stickmanObj;
    public Slider orderSlider;
    public Slider timerSlider;
    public Text videoTimerText;
    public Text exerciseOrderText;  //키넥트운동순서텍스트
    public Text topTitleNameText;
    public Text bottomTitleNameText;

    public Text[] exerciseNameText;
    public Image[] checkImg;
    public Text[] scoreText;
    public Sprite[] checkSprite;    //체크텍스쳐 통과/실패

    public GameObject playBtn;
    public GameObject pauseBtn;


    public int execiseOrder;    //키넥트운동 순서
    public bool execiseCheckStart;  //운동체크 시작 여부 


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        //PlayerPrefs.SetString("EP_UserNAME", "황태량");
        //PlayerPrefs.SetString("EP_UserBrithDay", "19880721");
        //PlayerPrefs.SetString("CARE_PlayMode", "PlayerMode");
        KinectTimer.instance.StartTime();   //시작 카운트

        //Debug.Log("---" + PlayerPrefs.GetString("CARE_KinectPassState1"));
        //DataFileSave();
    }

    // Update is called once per frame
    void Update()
    {
        if (KinectTimer.instance.kinectExerciseStart.Equals(true))
        {
            KinectTimer.instance.VideoPlayTime(60f, timerSlider, videoTimerText);
            exerciseOrderText.text = execiseOrder.ToString();
            orderSlider.value = (float)execiseOrder / 6;
            topTitleNameText.text = DataReadWrite.instance.KinectExerciseKindName(execiseOrder);
            bottomTitleNameText.text = DataReadWrite.instance.KinectExerciseKindName(execiseOrder);
        }
    }

    //결과 화면 정보 뿌려주기
    public void ResultScoreShow()
    {
        _ResultScoreShow("CARE_KinectPassState1", 0);
        _ResultScoreShow("CARE_KinectPassState2", 1);
        _ResultScoreShow("CARE_KinectPassState3", 2);
        _ResultScoreShow("CARE_KinectPassState4", 3);
        _ResultScoreShow("CARE_KinectPassState5", 4);
        _ResultScoreShow("CARE_KinectPassState6", 5);

        if (PlayerPrefs.GetString("CARE_PlayMode") != "GuestMode")
        {
            DataFileSave(); //키넥트 운동 파일에 저장
            DayDataFileSave();
        }

    }

    void _ResultScoreShow(string _prefName, int _index)
    {
        string exerciseName = exerciseNameText[_index].text;
        if (PlayerPrefs.GetString(_prefName).Equals("Yes"))
        {
            exerciseNameText[_index].text = "<color=#000000>" + exerciseName + "</color>";
            checkImg[_index].sprite = checkSprite[0];

            //17*4 + 16*2 =100;
            if (_prefName.Equals("CARE_KinectPassState1")
                || _prefName.Equals("CARE_KinectPassState2"))
            {
                scoreText[_index].text = "<color=#000000>" + "16점" + "</color>";
            }
            else
            {
                scoreText[_index].text = "<color=#000000>" + "17점" + "</color>";
            }

        }
        else
        {
            exerciseNameText[_index].text = "<color=#9F8484>" + exerciseName + "</color>";
            checkImg[_index].sprite = checkSprite[1];
            scoreText[_index].text = "<color=#9F8484>" + "실패" + "</color>";
        }
    }


    //재생버튼
    public void PlayButtonClick()
    {
        playBtn.SetActive(false);
        pauseBtn.SetActive(true);

        VideoHandler.instance.PauseVideo(); //영상 일시정지
        Time.timeScale = 0; //일시정지
    }

    //일시정지 버튼
    public void PauseButtonClick()
    {
        pauseBtn.SetActive(false);
        playBtn.SetActive(true);

        VideoHandler.instance.PlayVideo();  //영상 재생
        Time.timeScale = 1; //재생
    }


    public void BackButton()
    {
        KinectExerciseUserData.instance.UserKinectExerciseDataInit();   //데이터 초기화
        SceneManager.LoadScene("7.ExerciseReport");
    }

    public void HomeButtonClick()
    {
        SceneManager.LoadScene("5.UserExercise");
    }

    //키넥트 운동 파일에 저장하기
    public void DataFileSave()
    {
        StartCoroutine(_DataFileSave());
    }

    IEnumerator _DataFileSave()
    {
        string name = PlayerPrefs.GetString("CARE_KinectMode") + "_" +
            PlayerPrefs.GetString("EP_UserNAME") + "_" + PlayerPrefs.GetString("EP_UserBrithDay");
        string newFilePath = Application.persistentDataPath + "/Date/KinectExercise/" + name + ".csv";
        FileInfo fileinfo = new FileInfo(newFilePath);

        //파일 있는지 체크
        if (!fileinfo.Exists)
        {
            //파일 없음
            File.Create(newFilePath);   // 파일 생성
        }

        yield return new WaitForSeconds(4);

        List<Dictionary<string, object>> data;
        data = CSVReader.Read_("KinectExercise/" + name);

        StreamWriter writer = new StreamWriter(newFilePath);
        writer.WriteLine("횟수,운동1,운동2,운동3,운동4,운동5,운동6,총점");

        for (int i = 0; i < data.Count + 1; i++)
        {
            //기존 갯수만큼 데이터 써준다
            if (i <= data.Count - 1)
            {
                writer.WriteLine(data[i]["횟수"] + "," +
                    data[i]["운동1"] + "," +
                    data[i]["운동2"] + "," +
                    data[i]["운동3"] + "," +
                    data[i]["운동4"] + "," +
                    data[i]["운동5"] + "," +
                    data[i]["운동6"] + "," +
                    data[i]["총점"]);
            }
            //마지막 새로운 데이터를 써준다.
            else if (i.Equals(data.Count))
            {
                int total = ScoreSave("CARE_KinectPassState1") + ScoreSave("CARE_KinectPassState2") +
                    ScoreSave("CARE_KinectPassState3") + ScoreSave("CARE_KinectPassState4") +
                    ScoreSave("CARE_KinectPassState5") + ScoreSave("CARE_KinectPassState6");

                writer.WriteLine((i + 1).ToString() + "," +
                    ScoreSave("CARE_KinectPassState1") + "," +
                    ScoreSave("CARE_KinectPassState2") + "," +
                    ScoreSave("CARE_KinectPassState3") + "," +
                    ScoreSave("CARE_KinectPassState4") + "," +
                    ScoreSave("CARE_KinectPassState5") + "," +
                    ScoreSave("CARE_KinectPassState6") + "," +
                    total);
            }
        }

        writer.Flush();
        //This closes the file
        writer.Close();

        data = CSVReader.Read_("KinectExercise/" + name);
    }
    public void DayDataFileSave()
    {
        StartCoroutine(_DayDataFileSave());
    }

    IEnumerator _DayDataFileSave()
    {
        string name = "Day" + "_" +
                    PlayerPrefs.GetString("EP_UserNAME") + "_" + PlayerPrefs.GetString("EP_UserBrithDay");
        string newFilePath = Application.persistentDataPath + "/Date/KinectExercise/" + name + ".csv";
        FileInfo fileinfo = new FileInfo(newFilePath);

        //파일 있는지 체크
        if (!fileinfo.Exists)
        {
            //파일 없음
            File.Create(newFilePath);   // 파일 생성
        }

        yield return new WaitForSeconds(4);

        List<Dictionary<string, object>> data;
        data = CSVReader.Read_("KinectExercise/" + name);

        StreamWriter writer = new StreamWriter(newFilePath);
        //
        Debug.Log("운동횟수 :" + data.Count);
        writer.WriteLine("운동횟수,운동일자");

        if (!data.Count.Equals(0))
        {
            for (int i = 0; i < data.Count + 1; i++)
            {
                //기존 갯수만큼 데이터 써준다
                if (i <= data.Count - 1)
                {
                    writer.WriteLine(data[i]["운동횟수"] + "," +
                        data[i]["운동일자"]);
                }
                //마지막 새로운 데이터를 써준다.
                else if (i.Equals(data.Count))
                {
                    string Before_day_date = data[data.Count - 1]["운동일자"].ToString();
                    string Now_day_date = DateTime.Now.ToString("yyyy-MM-dd");
                    Debug.Log("운동일자 :" + Before_day_date + "  " + Now_day_date);
                    if (!Before_day_date.Equals(Now_day_date))
                    {
                        writer.WriteLine((i + 1).ToString() + "," +
                        DateTime.Now.ToString("yyyy-MM-dd"));
                    }

                }
            }
        }
        else
        {
            writer.WriteLine((data.Count + 1).ToString() + "," +
                  DateTime.Now.ToString("yyyy-MM-dd"));
        }
        writer.Flush();
        //This closes the file
        writer.Close();

        data = CSVReader.Read_("KinectExercise/" + name);
    }
    //데이터 점수로 변환
    int ScoreSave(string _prefName)
    {
        int score = 0;
        if (PlayerPrefs.GetString(_prefName).Equals("Yes"))
        {
            //17*4 + 16*2 =100;
            if (_prefName.Equals("CARE_KinectPassState1")
                || _prefName.Equals("CARE_KinectPassState2"))
            {
                score = 16;
            }
            else
            {
                score = 17;
            }
        }
        else
        {
            score = 0;
        }
        return score;
    }

}