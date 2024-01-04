using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Result_DataManager : MonoBehaviour
{
    public int Total_count;
    public int Month_count;
    public static Result_DataManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    public void TapKinectExerciseDataShow(string _mode, TextMeshProUGUI[] _numberText, TextMeshProUGUI[] _totalText,
        Slider[] _slider)
    {
        List<Dictionary<string, object>> data;
        string name =
            PlayerPrefs.GetString("EP_UserNAME") + "_" + PlayerPrefs.GetString("EP_UserBrithDay") + "_"+_mode ;
        string filePath = Application.persistentDataPath + "/Date/KinectExercise/" + name + ".csv";
        FileInfo fileinfo = new FileInfo(filePath);
        Debug.Log("------------------" + name);
        //파일 있는지 체크
        if (!fileinfo.Exists)
        {
            //파일 없음
            return;
        }
        else
        {
            //Debug.Log("------------------222222222222222");
            //Debug.Log("_mode :"+ _mode);
            data = CSVReader.Read_("KinectExercise/" + name);

            if (_mode.Equals("Brain"))
            {
                if (data.Count <= 8)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (int.Parse(data[i]["횟수"].ToString()).Equals(i + 1))
                        {
                            _numberText[i].text = data[i]["횟수"].ToString() + "회";
                            _totalText[i].text = data[i]["총점"].ToString();
                            _slider[i].value = (float)int.Parse(data[i]["총점"].ToString());
                        }
                       // Debug.Log("------------------" + data[i]["총점"].ToString());
                       // Debug.Log("------------------" + data[i]["횟수"].ToString());
                    }
                }
                else
                {
                    for (int i = data.Count - 8; i < data.Count; i++)
                    {
                        if (int.Parse(data[i]["횟수"].ToString()).Equals(i + 1))
                        {
                            _numberText[i - (data.Count - 8)].text = data[i]["횟수"].ToString() + "회";
                            _totalText[i - (data.Count - 8)].text = data[i]["총점"].ToString();
                            _slider[i - (data.Count - 8)].value = (float)int.Parse(data[i]["총점"].ToString());
                        }
                    }
                }
            }
            else if (_mode.Equals("Dementia"))
            {
              //  Debug.Log("아니 데이터 있어???????????????? :" + name.ToString()+"count : "+data.Count.ToString());
                if (data.Count <= 8)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (int.Parse(data[i]["횟수"].ToString()).Equals(i + 1))
                        {
                            _numberText[i].text = data[i]["횟수"].ToString() + "회";
                            _totalText[i].text = data[i]["총점"].ToString();
                            _slider[i].value = (float)int.Parse(data[i]["총점"].ToString());
                        }
                    }
                }
                else
                {
                    for (int i = data.Count - 8; i < data.Count; i++)
                    {
                        if (int.Parse(data[i]["횟수"].ToString()).Equals(i + 1))
                        {
                            _numberText[i - (data.Count - 8)].text = data[i]["횟수"].ToString() + "회";
                            _totalText[i - (data.Count - 8)].text = data[i]["총점"].ToString();
                            _slider[i - (data.Count - 8)].value = (float)int.Parse(data[i]["총점"].ToString());
                        }
                    }
                }
            }
            else if (_mode.Equals("Real"))
            {
                if (data.Count <= 8)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (int.Parse(data[i]["횟수"].ToString()).Equals(i + 1))
                        {
                            _numberText[i].text = data[i]["횟수"].ToString() + "회";
                            _totalText[i].text = data[i]["총점"].ToString();
                            _slider[i].value = (float)int.Parse(data[i]["총점"].ToString());
                        }
                    }
                }
                else
                {
                    for (int i = data.Count - 8; i < data.Count; i++)
                    {
                        if (int.Parse(data[i]["횟수"].ToString()).Equals(i + 1))
                        {
                            _numberText[i - (data.Count - 8)].text = data[i]["횟수"].ToString();
                            _totalText[i - (data.Count - 8)].text = data[i]["총점"].ToString();
                            _slider[i - (data.Count - 8)].value = (float)int.Parse(data[i]["총점"].ToString());
                        }
                    }
                }
            }
            else if (_mode.Equals("Sit") || _mode.Equals("Stand"))
            {
                if (data.Count <= 8)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (int.Parse(data[i]["횟수"].ToString()).Equals(i + 1))
                        {
                            _numberText[i].text = data[i]["횟수"].ToString() + "회";
                            _totalText[i].text = data[i]["총점"].ToString();
                            _slider[i].value = (float)int.Parse(data[i]["총점"].ToString());
                        }
                    }
                }
                else
                {
                    for (int i = data.Count - 8; i < data.Count; i++)
                    {
                        if (int.Parse(data[i]["횟수"].ToString()).Equals(i + 1))
                        {
                            _numberText[i - (data.Count - 8)].text = data[i]["횟수"].ToString() + "회";
                            _totalText[i - (data.Count - 8)].text = data[i]["총점"].ToString();
                            _slider[i - (data.Count - 8)].value = (float)int.Parse(data[i]["총점"].ToString());
                        }
                    }
                }
            }
        }
    }

    public void Calculate_Total_traing()
    {
        string name =
        PlayerPrefs.GetString("EP_UserNAME") + "_" + PlayerPrefs.GetString("EP_UserBrithDay") + "_Day";

        string filePath = Application.persistentDataPath + "/Date/KinectExercise/" + name + ".csv";
        FileInfo fileinfo = new FileInfo(filePath);
        //파일 있는지 체크
        if (!fileinfo.Exists)
        {
            Total_count = 0; //누적일수
            return; //파일없음
        }
        else
        {
            List<Dictionary<string, object>> data = CSVReader.Read_("KinectExercise/" + name);
            Total_count = data.Count; //누적일수
        }

    }
    public void Calculate_Month_traing()
    {
        string name =
        PlayerPrefs.GetString("EP_UserNAME") + "_" + PlayerPrefs.GetString("EP_UserBrithDay") + "_Day";

        string filePath = Application.persistentDataPath + "/Date/KinectExercise/" + name + ".csv";
        FileInfo fileinfo = new FileInfo(filePath);

        string Before_day_date;
        string Now_day_date;
        string[] before_split;
        string[] now_split;

        //파일있는지 체크
        if (!fileinfo.Exists)
        {
            Month_count = 0;
            return; //파일없음
        }
        else
        {
            List<Dictionary<string, object>> data = CSVReader.Read_("KinectExercise/" + name);

            if (!data.Count.Equals(0))
            {
                Now_day_date = DateTime.Now.ToString("yyyy-MM-dd");
                now_split = Now_day_date.Split('-');
                string month_now = now_split[1];

                string month_before = "";
                Debug.Log("month_now : " + month_now);
                Debug.Log("data.Count : " + data.Count);
                for (int i = 0; i < data.Count; i++)
                {
                    if (!data.Count.Equals(1))
                    {
                        Before_day_date = data[i]["운동일자"].ToString();
                        before_split = Before_day_date.Split('-');
                        month_before = before_split[1];
                        Debug.Log("month_before : " + month_before);
                        if (month_before.Equals(month_now))
                        {
                            Month_count += 1;
                        }
                    }
                
                }
            }
            else
            {
                Month_count = 0;
            }
        }
    }
}