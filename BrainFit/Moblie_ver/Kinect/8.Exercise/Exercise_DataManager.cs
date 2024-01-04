using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise_DataManager : MonoBehaviour
{
    public static Exercise_DataManager instance { get; private set; }




    public int totalSoccess;    //성공갯수

    //오른쪽
    float rightExercisePassCheckTime = 0;    //성공타이머(잘했어요)
    float rightExerciseNoPassCheckTime = 0;  //실패타이머(노력하세요)
    bool rightExercisePassCheck; //성공멘트 한번만 나오게 하는 조건변수
    bool rightExerciseNoPassCheck;  //실패멘트 한번만 나오게하는 조건변수 
    bool r_ExerciseNoPassDoubleCheck;   //양쪽 실패 체크 조건 변수
    //왼쪽
    float leftExercisePassCheckTime = 0;
    float leftExerciseNoPassCheckTime = 0;
    bool leftExercisePassCheck;
    bool leftExerciseNoPassCheck;
    bool l_ExerciseNoPassDoubleCheck;

    int r_Shoulder_Curr, r_Shoulder_Before, r_Shoulder_Gap; //오른쪽 현재 값, 이전값, 갭(이전값 - 현재값)
    int l_Shoulder_Curr, l_Shoulder_Before, l_Shoulder_Gap; //왼쪽 현재값, 이전값, 갭(이전값 - 현재값)

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        //KinectExerciseUserData.instance.UserKinectExerciseDataInit();   //운동데이터 초기화
    }


    void Update()
    {

    }

    //운동 결과값 저장
    public void ResultSave(string _result)
    {
        int order = Exercise_UIManager.instance.execiseOrder;

        if (order.Equals(1))
            KinectExerciseUserData.instance.SetExercise1_PassSave(_result);
        else if (order.Equals(2))
            KinectExerciseUserData.instance.SetExercise2_PassSave(_result);
        else if (order.Equals(3))
            KinectExerciseUserData.instance.SetExercise3_PassSave(_result);
        else if (order.Equals(4))
            KinectExerciseUserData.instance.SetExercise4_PassSave(_result);
        else if (order.Equals(5))
            KinectExerciseUserData.instance.SetExercise5_PassSave(_result);
        else if (order.Equals(6))
            KinectExerciseUserData.instance.SetExercise6_PassSave(_result);
    }

    //성공멘트 랜덤으로 나오게
    int VoiceRandom()
    {
        int index = Random.Range(0, 5);

        return index;
    }


    public void ExerciseCheck(string _check1, float _angle)
    {
        int order = Exercise_UIManager.instance.execiseOrder;
        if (PlayerPrefs.GetString("CARE_KinectMode").Equals("Sit"))
        {
            //팔 앞으로 뻗기
            if (order.Equals(1))
            {
                ArmFrontStretchExercise(_check1, _angle);
                //ShoulderTurn(_check1, _angle);
                //RightArmTurn(_check1, _angle);
                //LeftArmTurn(_check1, _angle);
                //BothArmsPutUp(_check1, _angle);
                //BothArmsSideStretch(_check1, _angle); 
            }
            //어깨 돌리기
            else if (order.Equals(2))
            {
                ShoulderTurn(_check1, _angle);
            }
            //오른쪽 팔 돌리기
            else if(order.Equals(3))
            {
                RightArmTurn(_check1, _angle);
            }
            //왼쪽 팔 돌리기
            else if(order.Equals(4))
            {
                LeftArmTurn(_check1, _angle);
            }
            //양팔 위로 올리기
            else if(order.Equals(5))
            {
                BothArmsPutUp(_check1, _angle);
            }
            //양팔 옆으로 뻗기
            else if(order.Equals(6))
            {
                BothArmsSideStretch(_check1, _angle);
            }
        }
    }

    //팔 앞으로 뻗기
    public void ArmFrontStretchExercise(string _check1, float _angle)
    {
        if (_check1.Equals("RightShoulder"))
        {
            //Debug.Log("오른쪽 어깨 : " + _angle.ToString("N1"));
            if (int.Parse(_angle.ToString("N0")) >= 60)
            {
                rightExerciseNoPassCheckTime = 0;    //실패 타이머 초기화
                rightExercisePassCheckTime += Time.deltaTime;
                //잘햇다
                if (rightExercisePassCheckTime < 5.1f)
                {
                    rightExerciseNoPassCheck = false; //실패여부 false
                    r_ExerciseNoPassDoubleCheck = false;    //실패여부 false

                    if (rightExercisePassCheckTime >= 5 && rightExercisePassCheck.Equals(false))
                    {
                        rightExercisePassCheck = true;  //성공여부 true
                        rightExercisePassCheckTime = 0;
                        //Debug.LogError("오른쪽 잘했다!!!!!!!");
                    }
                }
            }
            else
            {
                rightExercisePassCheckTime = 0;  //성공 타이머 초기화
                //못했다
                rightExerciseNoPassCheckTime += Time.deltaTime;

                if (rightExerciseNoPassCheckTime < 3.1f)
                {
                    rightExercisePassCheck = false; //성공여부 false

                    if (rightExerciseNoPassCheckTime >= 3 && rightExerciseNoPassCheck.Equals(false))
                    {
                        r_ExerciseNoPassDoubleCheck = true;
                        rightExerciseNoPassCheck = true;    //실패여부 true
                        rightExerciseNoPassCheckTime = 0;
                        //.LogError("오른쪽 못했다------");
                    }
                }
            }
        }

        if (_check1.Equals("LeftShoulder"))
        {
            if (int.Parse(_angle.ToString("N0")) >= 60)
            {
                //잘햇다
                leftExerciseNoPassCheckTime = 0;    //실패 타이머 초기화
                leftExercisePassCheckTime += Time.deltaTime;

                if (leftExercisePassCheckTime < 5.1f)
                {
                    leftExerciseNoPassCheck = false;    //실패여부false
                    l_ExerciseNoPassDoubleCheck = false;    //실패여부false

                    if (leftExercisePassCheckTime >= 5 && leftExercisePassCheck.Equals(false))
                    {
                        leftExercisePassCheck = true;
                        leftExercisePassCheckTime = 0;
                        //Debug.LogError("왼쪽 잘했다!!!!!!!");
                    }
                }
            }
            else
            {
                //못했다
                leftExercisePassCheckTime = 0;    //성공 타이머 초기화
                leftExerciseNoPassCheckTime += Time.deltaTime;

                if (leftExerciseNoPassCheckTime < 3.1f)
                {
                    leftExercisePassCheck = false;  //성공여부false

                    if (leftExerciseNoPassCheckTime >= 3 && leftExerciseNoPassCheck.Equals(false))
                    {
                        l_ExerciseNoPassDoubleCheck = true;
                        leftExerciseNoPassCheck = true;
                        leftExerciseNoPassCheckTime = 0;
                        //Debug.LogError("왼쪽 못했다---------");
                    }
                }
            }
        }

        if (rightExercisePassCheck.Equals(true) && leftExercisePassCheck.Equals(true))
        {
            Debug.LogError("양쪽모두 성공!!!!");
            totalSoccess++;
            VoiceSound.instance.VoiceAnnouncement(VoiceRandom());   
            rightExercisePassCheck = false;
            leftExercisePassCheck = false;
        }

        if (r_ExerciseNoPassDoubleCheck.Equals(true) && l_ExerciseNoPassDoubleCheck.Equals(true))
        {
            Debug.LogError("양쪽모두 실패 --------");
            VoiceSound.instance.VoiceAnnouncement(7); //팔앞으로
            r_ExerciseNoPassDoubleCheck = false;
            l_ExerciseNoPassDoubleCheck = false;
            rightExerciseNoPassCheck = false;
            leftExerciseNoPassCheck = false;
        }
        else if (rightExerciseNoPassCheck.Equals(true) && leftExerciseNoPassCheck.Equals(false) &&
            rightExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError(r_ExerciseNoPassDoubleCheck + " +오른쪽 실패 --------" + l_ExerciseNoPassDoubleCheck);
            VoiceSound.instance.VoiceAnnouncement(5); //오른팔앞으로올려
            rightExerciseNoPassCheck = false;
        }
        else if (leftExerciseNoPassCheck.Equals(true) && rightExerciseNoPassCheck.Equals(false) &&
            leftExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError(r_ExerciseNoPassDoubleCheck + "~왼쪽 실패 --------" + l_ExerciseNoPassDoubleCheck);
            VoiceSound.instance.VoiceAnnouncement(6); //왼팔앞으로올려
            leftExerciseNoPassCheck = false;
        }
    }


    //어깨 돌리기
    public void ShoulderTurn(string _check, float _angle)
    {
        if (_check.Equals("RightShoulder"))
        {
            r_Shoulder_Curr = (int)_angle;
            r_Shoulder_Gap = Mathf.Abs(r_Shoulder_Before - r_Shoulder_Curr);
            //Debug.Log("오른쪽 - " + r_Shoulder_Gap);

            rightExercisePassCheckTime += Time.deltaTime;
            rightExerciseNoPassCheckTime += Time.deltaTime;

            //Debug.Log(rightExercisePassCheck + " 오른쪽  " + rightExercisePassCheckTime);
            if (r_Shoulder_Gap > 5f)
            {
                rightExerciseNoPassCheck = false;
                rightExerciseNoPassCheckTime = 0;
                //성공
                if (rightExercisePassCheckTime >= 0f)
                {
                    if (rightExercisePassCheckTime > 5 && rightExercisePassCheck.Equals(false))
                    {
                        rightExercisePassCheckTime = 0;
                        rightExerciseNoPassCheckTime = 0;
                        rightExercisePassCheck = true;
                    }
                }
                r_Shoulder_Before = r_Shoulder_Curr;
            }
            else
            {
                //실패
                if (rightExerciseNoPassCheckTime <= 3.1f)
                {
                    
                    if (rightExerciseNoPassCheckTime >= 1.5f)
                    {
                        rightExercisePassCheck = false; //성공여부 false
                        rightExercisePassCheckTime = 0; //성공여부시간 초기화

                        if (rightExerciseNoPassCheckTime > 3f && rightExerciseNoPassCheck.Equals(false))
                        {
                            rightExerciseNoPassCheckTime = 0;   //다시 검사하기 위해 실패 타이머 초기화
                            rightExercisePassCheckTime = 0; //실패 확정이라서 성공 타이머 초기화
                            rightExerciseNoPassCheck = true;
                            r_ExerciseNoPassDoubleCheck = true;
                        }
                    }
                }
                r_Shoulder_Before = r_Shoulder_Curr;
            }
        }

        if (_check.Equals("LeftShoulder"))
        {
            l_Shoulder_Curr = (int)_angle;
            l_Shoulder_Gap = Mathf.Abs(l_Shoulder_Before - l_Shoulder_Curr);
            //Debug.Log("왼쪽 - " + l_Shoulder_Gap);
            leftExercisePassCheckTime += Time.deltaTime;
            leftExerciseNoPassCheckTime += Time.deltaTime;

            if (l_Shoulder_Gap > 5f)
            {
                leftExerciseNoPassCheck = false;
                leftExerciseNoPassCheckTime = 0;
                //성공
                if (leftExercisePassCheckTime >= 0)
                {
                    if (leftExercisePassCheckTime > 5 && leftExercisePassCheck.Equals(false))
                    {
                        leftExercisePassCheckTime = 0;
                        leftExerciseNoPassCheckTime = 0;
                        leftExercisePassCheck = true;
                    }
                }
                l_Shoulder_Before = l_Shoulder_Curr;
            }
            else
            {
                //실패
                if (leftExerciseNoPassCheckTime < 3.1f)
                {
                    if (leftExerciseNoPassCheckTime >= 1.5f)
                    {
                        leftExercisePassCheck = false;
                        leftExercisePassCheckTime = 0;  //실패쪽에 들어와서 성공여부시간 초기화
                        if (leftExerciseNoPassCheckTime > 3 && leftExerciseNoPassCheck.Equals(false))
                        {
                            leftExerciseNoPassCheckTime = 0;
                            leftExercisePassCheckTime = 0;
                            leftExerciseNoPassCheck = true;
                            l_ExerciseNoPassDoubleCheck = true;
                        }
                    }
                }
                l_Shoulder_Before = l_Shoulder_Curr;
            }
        }

        if (rightExercisePassCheck.Equals(true))
        {
            //rightExercisePassCheck = false;
            //Debug.Log("오른쪽 성공");
        }

        if (leftExercisePassCheck.Equals(true))
        {
            //leftExercisePassCheck = false;
            //Debug.Log("왼쪽 성공");
        }

        if(rightExercisePassCheck.Equals(true) && leftExercisePassCheck.Equals(true))
        {
            Debug.Log("양쪽 성공!!!");
            totalSoccess++;
            VoiceSound.instance.VoiceAnnouncement(VoiceRandom());
            rightExercisePassCheck = false;
            leftExercisePassCheck = false;
        }
        
        if (r_ExerciseNoPassDoubleCheck.Equals(true) && l_ExerciseNoPassDoubleCheck.Equals(true))
        {
            Debug.LogError("양쪽모두 실패 ---");
            VoiceSound.instance.VoiceAnnouncement(10);   //팔돌려
            r_ExerciseNoPassDoubleCheck = false;
            l_ExerciseNoPassDoubleCheck = false;
            rightExerciseNoPassCheck = false;
            leftExerciseNoPassCheck = false;
        }
        else if (rightExerciseNoPassCheck.Equals(true) && leftExerciseNoPassCheck.Equals(false) &&
            rightExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError("오른쪽 실패 ---!!!");
            VoiceSound.instance.VoiceAnnouncement(8);   //오른쪾어깨돌려
            rightExerciseNoPassCheck = false;
        }
        else if (leftExerciseNoPassCheck.Equals(true) && rightExerciseNoPassCheck.Equals(false) &&
            leftExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError("왼쪽 실패~~~!");
            VoiceSound.instance.VoiceAnnouncement(9);  //왼쪽어깨돌려
            leftExerciseNoPassCheck = false;
        }
    }


    //오른쪽 팔 돌리기
    public void RightArmTurn(string _check, float _angle)
    {
        if (_check.Equals("RightShoulder"))
        {
            r_Shoulder_Curr = (int)_angle;
            r_Shoulder_Gap = Mathf.Abs(r_Shoulder_Before - r_Shoulder_Curr);
            //Debug.Log("오른쪽 - " + r_Shoulder_Gap);

            rightExercisePassCheckTime += Time.deltaTime;
            rightExerciseNoPassCheckTime += Time.deltaTime;

            //Debug.Log(rightExercisePassCheck + " 오른쪽  " + rightExercisePassCheckTime);
            if (r_Shoulder_Gap > 6f)
            {
                rightExerciseNoPassCheck = false;
                rightExerciseNoPassCheckTime = 0;
                //성공
                if (rightExercisePassCheckTime >= 0f)
                {
                    if (rightExercisePassCheckTime > 5 && rightExercisePassCheck.Equals(false))
                    {
                        rightExercisePassCheckTime = 0;
                        rightExerciseNoPassCheckTime = 0;
                        rightExercisePassCheck = true;
                    }
                }
                r_Shoulder_Before = r_Shoulder_Curr;
            }
            else
            {
                //실패
                if (rightExerciseNoPassCheckTime <= 3.1f)
                {

                    if (rightExerciseNoPassCheckTime >= 2f)
                    {
                        rightExercisePassCheck = false; //성공여부 false
                        rightExercisePassCheckTime = 0; //성공여부시간 초기화

                        if (rightExerciseNoPassCheckTime > 3f && rightExerciseNoPassCheck.Equals(false))
                        {
                            rightExerciseNoPassCheckTime = 0;   //다시 검사하기 위해 실패 타이머 초기화
                            rightExercisePassCheckTime = 0; //실패 확정이라서 성공 타이머 초기화
                            rightExerciseNoPassCheck = true;
                        }
                    }
                }
                r_Shoulder_Before = r_Shoulder_Curr;
            }
        }

        if(rightExercisePassCheck.Equals(true))
        {
            totalSoccess++;
            rightExercisePassCheck = false;
            VoiceSound.instance.VoiceAnnouncement(VoiceRandom());
            Debug.Log("오른쪽 성공!");
        }

        if(rightExerciseNoPassCheck.Equals(true))
        {
            rightExerciseNoPassCheck = false;
            VoiceSound.instance.VoiceAnnouncement(11);  //오른팔돌려
            Debug.Log("오른쪽 실패 ~~");
        }
    }


    //왼쪽 팔 돌리기
    public void LeftArmTurn(string _check, float _angle)
    {
        if (_check.Equals("LeftShoulder"))
        {
            l_Shoulder_Curr = (int)_angle;
            l_Shoulder_Gap = Mathf.Abs(l_Shoulder_Before - l_Shoulder_Curr);
            //Debug.Log("왼쪽 - " + l_Shoulder_Gap);
            leftExercisePassCheckTime += Time.deltaTime;
            leftExerciseNoPassCheckTime += Time.deltaTime;

            if (l_Shoulder_Gap > 6f)
            {
                leftExerciseNoPassCheck = false;
                leftExerciseNoPassCheckTime = 0;
                //성공
                if (leftExercisePassCheckTime >= 0)
                {
                    if (leftExercisePassCheckTime > 5 && leftExercisePassCheck.Equals(false))
                    {
                        leftExercisePassCheckTime = 0;
                        leftExerciseNoPassCheckTime = 0;
                        leftExercisePassCheck = true;
                    }
                }
                l_Shoulder_Before = l_Shoulder_Curr;
            }
            else
            {
                //실패
                if (leftExerciseNoPassCheckTime < 3.1f)
                {
                    if (leftExerciseNoPassCheckTime >= 2f)
                    {
                        leftExercisePassCheck = false;
                        leftExercisePassCheckTime = 0;  //실패쪽에 들어와서 성공여부시간 초기화
                        if (leftExerciseNoPassCheckTime > 3 && leftExerciseNoPassCheck.Equals(false))
                        {
                            leftExerciseNoPassCheckTime = 0;
                            leftExercisePassCheckTime = 0;
                            leftExerciseNoPassCheck = true;
                        }
                    }
                }
                l_Shoulder_Before = l_Shoulder_Curr;
            }
        }

        if(leftExercisePassCheck.Equals(true))
        {
            totalSoccess++;
            leftExercisePassCheck = false;
            VoiceSound.instance.VoiceAnnouncement(VoiceRandom());
            Debug.Log("왼쪽 성공 ~!");
        }

        if(leftExerciseNoPassCheck.Equals(true))
        {
            leftExerciseNoPassCheck = false;
            VoiceSound.instance.VoiceAnnouncement(12);  //왼쪽팔돌려
            Debug.Log("왼쪽 실패~!!!!");
        }
    }


    //양팔 위로 올리기
    public void BothArmsPutUp(string _check, float _angle)
    {
        if (_check.Equals("RightShoulder"))
        {
            //Debug.Log("오른쪽 어깨 : " + _angle.ToString("N1"));
            if (int.Parse(_angle.ToString("N0")) >= 100)
            {
                rightExerciseNoPassCheckTime = 0;    //실패 타이머 초기화
                rightExercisePassCheckTime += Time.deltaTime;
                //잘햇다
                if (rightExercisePassCheckTime < 5.1f)
                {
                    rightExerciseNoPassCheck = false; //실패여부 false
                    r_ExerciseNoPassDoubleCheck = false;    //실패여부 false

                    if (rightExercisePassCheckTime >= 5 && rightExercisePassCheck.Equals(false))
                    {
                        rightExercisePassCheck = true;  //성공여부 true
                        rightExercisePassCheckTime = 0;
                        //Debug.LogError("오른쪽 잘했다!!!!!!!");
                    }
                }
            }
            else
            {
                rightExercisePassCheckTime = 0;  //성공 타이머 초기화
                //못했다
                rightExerciseNoPassCheckTime += Time.deltaTime;

                if (rightExerciseNoPassCheckTime < 3.1f)
                {
                    rightExercisePassCheck = false; //성공여부 false

                    if (rightExerciseNoPassCheckTime >= 3 && rightExerciseNoPassCheck.Equals(false))
                    {
                        r_ExerciseNoPassDoubleCheck = true;
                        rightExerciseNoPassCheck = true;    //실패여부 true
                        rightExerciseNoPassCheckTime = 0;
                        //.LogError("오른쪽 못했다------");
                    }
                }
            }
        }

        if (_check.Equals("LeftShoulder"))
        {
            if (int.Parse(_angle.ToString("N0")) >= 100)
            {
                //잘햇다
                leftExerciseNoPassCheckTime = 0;    //실패 타이머 초기화
                leftExercisePassCheckTime += Time.deltaTime;

                if (leftExercisePassCheckTime < 5.1f)
                {
                    leftExerciseNoPassCheck = false;    //실패여부false
                    l_ExerciseNoPassDoubleCheck = false;    //실패여부false

                    if (leftExercisePassCheckTime >= 5 && leftExercisePassCheck.Equals(false))
                    {
                        leftExercisePassCheck = true;
                        leftExercisePassCheckTime = 0;
                        //Debug.LogError("왼쪽 잘했다!!!!!!!");
                    }
                }
            }
            else
            {
                //못했다
                leftExercisePassCheckTime = 0;    //성공 타이머 초기화
                leftExerciseNoPassCheckTime += Time.deltaTime;

                if (leftExerciseNoPassCheckTime < 3.1f)
                {
                    leftExercisePassCheck = false;  //성공여부false

                    if (leftExerciseNoPassCheckTime >= 3 && leftExerciseNoPassCheck.Equals(false))
                    {
                        l_ExerciseNoPassDoubleCheck = true;
                        leftExerciseNoPassCheck = true;
                        leftExerciseNoPassCheckTime = 0;
                        //Debug.LogError("왼쪽 못했다---------");
                    }
                }
            }
        }

        if (rightExercisePassCheck.Equals(true) && leftExercisePassCheck.Equals(true))
        {
            Debug.LogError("양쪽모두 성공!!!!");
            totalSoccess++;
            VoiceSound.instance.VoiceAnnouncement(VoiceRandom());
            rightExercisePassCheck = false;
            leftExercisePassCheck = false;
        }

        if (r_ExerciseNoPassDoubleCheck.Equals(true) && l_ExerciseNoPassDoubleCheck.Equals(true))
        {
            Debug.LogError("양쪽모두 실패 --------");
            VoiceSound.instance.VoiceAnnouncement(18);  //양팔 위로 올려
            r_ExerciseNoPassDoubleCheck = false;
            l_ExerciseNoPassDoubleCheck = false;
            rightExerciseNoPassCheck = false;
            leftExerciseNoPassCheck = false;
        }
        else if (rightExerciseNoPassCheck.Equals(true) && leftExerciseNoPassCheck.Equals(false) &&
            rightExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError(r_ExerciseNoPassDoubleCheck + " +오른쪽 실패 --------" + l_ExerciseNoPassDoubleCheck);
            VoiceSound.instance.VoiceAnnouncement(16);  //오른팔위로올려
            rightExerciseNoPassCheck = false;
        }
        else if (leftExerciseNoPassCheck.Equals(true) && rightExerciseNoPassCheck.Equals(false) &&
            leftExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError(r_ExerciseNoPassDoubleCheck + "~왼쪽 실패 --------" + l_ExerciseNoPassDoubleCheck);
            VoiceSound.instance.VoiceAnnouncement(17);  //왼팔위로올려
            leftExerciseNoPassCheck = false;
        }
    }


    //양팔 옆으로 뻗기
    public void BothArmsSideStretch(string _check, float _angle)
    {
        if (_check.Equals("RightShoulder"))
        {
            //Debug.Log("오른쪽 어깨 : " + _angle.ToString("N1"));
            if (int.Parse(_angle.ToString("N0")) >= 90)
            {
                rightExerciseNoPassCheckTime = 0;    //실패 타이머 초기화
                rightExercisePassCheckTime += Time.deltaTime;
                //잘햇다
                if (rightExercisePassCheckTime < 5.1f)
                {
                    rightExerciseNoPassCheck = false; //실패여부 false
                    r_ExerciseNoPassDoubleCheck = false;    //실패여부 false

                    if (rightExercisePassCheckTime >= 5 && rightExercisePassCheck.Equals(false))
                    {
                        rightExercisePassCheck = true;  //성공여부 true
                        rightExercisePassCheckTime = 0;
                        //Debug.LogError("오른쪽 잘했다!!!!!!!");
                    }
                }
            }
            else
            {
                rightExercisePassCheckTime = 0;  //성공 타이머 초기화
                //못했다
                rightExerciseNoPassCheckTime += Time.deltaTime;

                if (rightExerciseNoPassCheckTime < 3.1f)
                {
                    rightExercisePassCheck = false; //성공여부 false

                    if (rightExerciseNoPassCheckTime >= 3 && rightExerciseNoPassCheck.Equals(false))
                    {
                        r_ExerciseNoPassDoubleCheck = true;
                        rightExerciseNoPassCheck = true;    //실패여부 true
                        rightExerciseNoPassCheckTime = 0;
                        //.LogError("오른쪽 못했다------");
                    }
                }
            }
        }

        if (_check.Equals("LeftShoulder"))
        {
            if (int.Parse(_angle.ToString("N0")) >= 90)
            {
                //잘햇다
                leftExerciseNoPassCheckTime = 0;    //실패 타이머 초기화
                leftExercisePassCheckTime += Time.deltaTime;

                if (leftExercisePassCheckTime < 5.1f)
                {
                    leftExerciseNoPassCheck = false;    //실패여부false
                    l_ExerciseNoPassDoubleCheck = false;    //실패여부false

                    if (leftExercisePassCheckTime >= 5 && leftExercisePassCheck.Equals(false))
                    {
                        leftExercisePassCheck = true;
                        leftExercisePassCheckTime = 0;
                        //Debug.LogError("왼쪽 잘했다!!!!!!!");
                    }
                }
            }
            else
            {
                //못했다
                leftExercisePassCheckTime = 0;    //성공 타이머 초기화
                leftExerciseNoPassCheckTime += Time.deltaTime;

                if (leftExerciseNoPassCheckTime < 3.1f)
                {
                    leftExercisePassCheck = false;  //성공여부false

                    if (leftExerciseNoPassCheckTime >= 3 && leftExerciseNoPassCheck.Equals(false))
                    {
                        l_ExerciseNoPassDoubleCheck = true;
                        leftExerciseNoPassCheck = true;
                        leftExerciseNoPassCheckTime = 0;
                        //Debug.LogError("왼쪽 못했다---------");
                    }
                }
            }
        }

        if (rightExercisePassCheck.Equals(true) && leftExercisePassCheck.Equals(true))
        {
            Debug.LogError("양쪽모두 성공!!!!");
            totalSoccess++;
            VoiceSound.instance.VoiceAnnouncement(VoiceRandom());
            rightExercisePassCheck = false;
            leftExercisePassCheck = false;
        }

        if (r_ExerciseNoPassDoubleCheck.Equals(true) && l_ExerciseNoPassDoubleCheck.Equals(true))
        {
            Debug.LogError("양쪽모두 실패 --------");
            VoiceSound.instance.VoiceAnnouncement(15);  //양팔옆으로
            r_ExerciseNoPassDoubleCheck = false;
            l_ExerciseNoPassDoubleCheck = false;
            rightExerciseNoPassCheck = false;
            leftExerciseNoPassCheck = false;
        }
        else if (rightExerciseNoPassCheck.Equals(true) && leftExerciseNoPassCheck.Equals(false) &&
            rightExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError(r_ExerciseNoPassDoubleCheck + " +오른쪽 실패 --------" + l_ExerciseNoPassDoubleCheck);
            VoiceSound.instance.VoiceAnnouncement(13);  //오른팔옆으로
            rightExerciseNoPassCheck = false;
        }
        else if (leftExerciseNoPassCheck.Equals(true) && rightExerciseNoPassCheck.Equals(false) &&
            leftExerciseNoPassCheckTime > 1f)
        {
            Debug.LogError(r_ExerciseNoPassDoubleCheck + "~왼쪽 실패 --------" + l_ExerciseNoPassDoubleCheck);
            VoiceSound.instance.VoiceAnnouncement(14);  //왼팔옆으로
            leftExerciseNoPassCheck = false;
        }
    }

}
