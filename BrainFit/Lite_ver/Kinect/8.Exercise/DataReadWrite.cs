using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReadWrite : MonoBehaviour
{
    public static DataReadWrite instance { get; private set; }

    List<Dictionary<string, object>> data;



    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        
    }

    //키넥트 운동종류 이름 반환
    public string KinectExerciseKindName(int _indexName)
    {
        data = CSVReader.Read_("KinectExerciseKind");
        string name = "";

        for (int i = 0; i < data.Count; i++)
        {
            if (data[i]["Mode"].ToString().Equals(PlayerPrefs.GetString("CARE_KinectMode")))
            {
                name = data[i]["Name" + _indexName.ToString()].ToString();
            }
        }

        return name;
    }

    //키넥트 운동에 따른 나레이션시간 반환
    public int KinectNarrationTime(int _indexName)
    {
        data = CSVReader.Read_("KinectExerciseKind");

        int stopTime = 0;

        for(int i = 0; i < data.Count; i++)
        {
            if(data[i]["Mode"].ToString().Equals("StopTime"))
            {
                stopTime = int.Parse(data[i]["Name" + _indexName.ToString()].ToString());
            }
        }
        return stopTime;
    }
}
