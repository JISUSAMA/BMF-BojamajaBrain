using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//https://xd.adobe.com/view/12532840-d1ae-44fe-803c-5b35804bddfc-4700/screen/8b8eb92d-3722-4d8c-adf0-f953ca012fa7
//https://xd.adobe.com/view/37c684aa-4bb7-4b31-84ce-8942eceee042-6295/screen/7b8c4001-f7e0-4a8d-afbc-a78a3fab9579/
public class KinectExerciseUserData : MonoBehaviour
{
    public static KinectExerciseUserData instance { get; private set; }


    public string kinectExerciseMode;   //운동 모드(Sit/Stand)
    public string[] exercisePassState;  //운동 통과 여부(Yes/No) 


    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);
    }


    void Start()
    {
        exercisePassState = new string[6];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //유저 키넥트 운동 초기화
    public void UserKinectExerciseDataInit()
    {
        SetExercise1_PassSave("");
        SetExercise2_PassSave("");
        SetExercise3_PassSave("");
        SetExercise4_PassSave("");
        SetExercise5_PassSave("");
        SetExercise6_PassSave("");
    }

    //키넥트 운동 모드 저장
    public void SetKinectExerciseModeSave(string _mode)
    {
        kinectExerciseMode = _mode;
        PlayerPrefs.SetString("CARE_KinectMode", _mode);
    }

    //키넥트 운동 결과 저장 함수들
    public void SetExercise1_PassSave(string _state)
    {
        exercisePassState[0] = _state;
        PlayerPrefs.SetString("CARE_KinectPassState1", _state);
    }

    public void SetExercise2_PassSave(string _state)
    {
        exercisePassState[1] = _state;
        PlayerPrefs.SetString("CARE_KinectPassState2", _state);
    }

    public void SetExercise3_PassSave(string _state)
    {
        exercisePassState[2] = _state;
        PlayerPrefs.SetString("CARE_KinectPassState3", _state);
    }

    public void SetExercise4_PassSave(string _state)
    {
        exercisePassState[3] = _state;
        PlayerPrefs.SetString("CARE_KinectPassState4", _state);
    }

    public void SetExercise5_PassSave(string _state)
    {
        exercisePassState[4] = _state;
        PlayerPrefs.SetString("CARE_KinectPassState5", _state);
    }

    public void SetExercise6_PassSave(string _state)
    {
        exercisePassState[5] = _state;
        PlayerPrefs.SetString("CARE_KinectPassState6", _state);
    }



}
