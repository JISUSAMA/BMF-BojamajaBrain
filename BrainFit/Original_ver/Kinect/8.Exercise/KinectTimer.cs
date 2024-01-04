using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KinectTimer : MonoBehaviour
{
    public static KinectTimer instance { get; private set; }

    public GameObject stickmanObj;  //키넥트뼈대
    public GameObject finalPanel;   //최종판넬
    public GameObject[] resultPanel;    //결과판넬(Pass/Fail)
    public GameObject startCountImgParent;  //시작카운트판넬
    public Image startCountImg;  //시작카운트이미지
    public Text[] nextCountText;    //다음넘어가는카운트이미지(pass/fail)
    public Sprite[] startTimeSpri;  //카운트텍스쳐


    public bool kinectExerciseStart;    //운동시작 

    float currTime; //현재 시간


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {

    }

    //운동 카운트 다운 타이머
    public void StartTime()
    {
        StartCoroutine(_StartTime());
    }

    IEnumerator _StartTime()
    {
        stickmanObj.SetActive(false);
        yield return new WaitForSeconds(1f);

        startCountImgParent.SetActive(true);
        startCountImg.sprite = startTimeSpri[2];
        yield return new WaitForSeconds(1f);

        startCountImg.sprite = startTimeSpri[3];
        yield return new WaitForSeconds(1f);

        startCountImg.sprite = startTimeSpri[4];
        yield return new WaitForSeconds(1f);

        startCountImgParent.SetActive(false);
        stickmanObj.SetActive(true);
        kinectExerciseStart = true; // 카운트다운 했음
    }


    //다음운동 카운트 다운 타이머
    public void NextStartTime(GameObject _panel, Text _countdown)
    {
        StartCoroutine(_NextStartTime(_panel, _countdown));
    }

    IEnumerator _NextStartTime(GameObject _panel, Text _countdown)
    {
        stickmanObj.SetActive(false); //결과창 나와야해서 뼈대 비활성화
        yield return new WaitForSeconds(1f);

        _panel.SetActive(true); //Pass판넬 부모 활성화 
        _panel.transform.GetChild(0).gameObject.SetActive(true);
        _panel.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);


        _panel.transform.GetChild(0).gameObject.SetActive(false);   //미리보기판넬
        _panel.transform.GetChild(1).gameObject.SetActive(true);    //결과보기판넬
        //실천한 운동 이름
        _panel.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text =
            DataReadWrite.instance.KinectExerciseKindName(Exercise_UIManager.instance.execiseOrder);

        //5초 카운트 다운 시작
        //_countdown.text = "5";
        //yield return new WaitForSeconds(1f);

        //_countdown.text = "4";
        //yield return new WaitForSeconds(1f);

        _countdown.text = "3";
        yield return new WaitForSeconds(1f);

        _countdown.text = "2";
        yield return new WaitForSeconds(1f);

        _countdown.text = "1";
        yield return new WaitForSeconds(1f);

        _panel.transform.GetChild(0).gameObject.SetActive(false);
        _panel.transform.GetChild(1).gameObject.SetActive(false);
        _panel.SetActive(false);

        //운동 마지막 끝나고 최종 판넬 나오게하는 조건식
        if(Exercise_UIManager.instance.execiseOrder < 6)
        {
            stickmanObj.SetActive(true);
            kinectExerciseStart = true; // 카운트다운 했음
        }
        else
        {
            //마지막 최종 결과 화면
            stickmanObj.SetActive(false);
            finalPanel.SetActive(true);
            Exercise_UIManager.instance.ResultScoreShow();  //결과정보 뿌려주기
        }
    }

    //비디오 카운트 다운 타이머
    public void VideoPlayTime(float _maxTime, Slider _slider, Text _videoTimeText)
    {
        kinectExerciseStart = false;
        StartCoroutine(_VideoPlayTime(_maxTime, _slider, _videoTimeText));
    }

    IEnumerator _VideoPlayTime(float _maxTime, Slider _slider, Text _videoTimeText)
    {
        Exercise_UIManager.instance.execiseOrder++; //비디오 재생할때 마다 +1해준다.
        currTime = 0;
        yield return new WaitForSeconds(1f);

        while(currTime <= _maxTime)
        {
            //비디오 타이머가 시작했을때 영상 플레이
            if (currTime.Equals(0))
            {
                Exercise_DataManager.instance.totalSoccess = 0;
                VideoHandler.instance.LoadVideo(PlayerPrefs.GetString("CARE_KinectMode"),
                    Exercise_UIManager.instance.execiseOrder);
            }

            //검사 시작하는 타이밍을 할려준다.
            if(currTime >= DataReadWrite.instance.KinectNarrationTime(Exercise_UIManager.instance.execiseOrder) &&
                currTime <= DataReadWrite.instance.KinectNarrationTime(Exercise_UIManager.instance.execiseOrder) + 1)
            {
                Exercise_UIManager.instance.execiseCheckStart = true;   //운동체크 시작!
            }

            yield return new WaitForSecondsRealtime(0f);
            currTime += Time.deltaTime;
            _slider.value = currTime / _maxTime;

            //형식에 맞춰서 시간 텍스트 변경
            TimeSpan timeSpan = TimeSpan.FromSeconds(currTime);
            string timer = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);

            _videoTimeText.text = timer;

            //끝나기 3초전에 운동 체크 그만
            if(currTime >= _maxTime - 3)
                Exercise_UIManager.instance.execiseCheckStart = false;   //운동체크 그만!!!

            //비디오가 다 끝났으면
            if (currTime >= _maxTime)
            {
                //Debug.Log("총 갯수 : " + Exercise_DataManager.instance.totalSoccess);
                if(Exercise_DataManager.instance.totalSoccess >= 4)
                {
                    //성공햇을 때 판넬
                    NextStartTime(resultPanel[0], nextCountText[0]);
                    Exercise_DataManager.instance.ResultSave("Yes");    //결과 Yes로 저장
                }
                else
                {
                    //실패했을 때 판넬
                    NextStartTime(resultPanel[1], nextCountText[1]);
                    Exercise_DataManager.instance.ResultSave("No"); //결과 No로 저장
                }
            }
        }
    }
}
