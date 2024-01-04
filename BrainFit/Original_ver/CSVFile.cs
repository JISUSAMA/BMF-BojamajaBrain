using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Linq;
using UnityEditor;

public class CSVFile : MonoBehaviour
{
    public static CSVFile instance { get; private set; }

    public List<Face2> SearchableFaces = new List<Face2>();
    List<Dictionary<string, object>> data;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    public void GetFaceData(List<Face2> _SearchableFaces)
    {
        Debug.Log("여기 들어는 옵니 ???");
        data = CSVReader.Read_("Student Data");

        Debug.Log("개수 : "+ data.Count);

        for (var i = 0; i < data.Count; i++)
        {
            Debug.Log("index " + (i).ToString() + " : " + data[i]["이름"] + " " + data[i]["생년월일"] + " " + data[i]["성별"]);
            //string str = data[i]["이름"].ToString();
            //Debug.Log("str " + str);

            _SearchableFaces.Add(new Face2(data[i]["이름"].ToString(), Color.green, data[i]["이름"].ToString()+ "_"+ data[i]["생년월일"].ToString(),
                data[i]["생년월일"].ToString(), data[i]["성별"].ToString()));
        }
    }

    public void SaveInventory()
    {
        Debug.Log("안들어오니");
        string filePath = getPath();

        Debug.Log("주소 : " + filePath);

        //This is the writer, it writes to the filepath
        StreamWriter writer = new StreamWriter(filePath);
        Debug.Log("뭐닝");
        //This is writing the line of the type, name, damage... etc... (I set these)
        writer.WriteLine("이름,생년월일,성별");
        //This loops through everything in the inventory and sets the file to these.
        Debug.Log("data.Count: " + data.Count);
      
      
        for (int i = 0; i < data.Count+1; ++i)
        {
            Debug.Log("-----" + (data.Count - 1));
            if (i <= data.Count-1)
            {
                Debug.Log("여기들어오지 ???");
                writer.WriteLine(data[i]["이름"] +
                "," + data[i]["생년월일"] +
                "," + data[i]["성별"]);
            }
            else if(i.Equals(data.Count))
            {
                writer.WriteLine("홍길동" +
                "," + "20001212" +
                "," + "남자");
            }
        }
        writer.Flush();
        //This closes the file
        writer.Close();

        
        //AssetDatabase.Refresh();
        data = CSVReader.Read_("Student Data");
    }

    public void UserDataSvae()
    {
        string filePath = getPath();

        //This is the writer, it writes to the filepath
        StreamWriter writer = new StreamWriter(filePath);

        //사용자 유저가 한명도 없으면
        if(data.Count.Equals(0))
        {
            //writer.WriteLine
        }
        else
        {

        }
    }

    private string getPath()
    {
        return Application.persistentDataPath+ "/Date/Student Data.csv";
        //return @"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\Date\Student Data.csv";

        //return @"D:\User/Student Data.csv";
        //#if UNITY_EDITOR
        //        //return Application.dataPath + "/Resources/" + "Student Data.csv";
        //        Debug.Log(Path.Combine(Application.persistentDataPath, "Date/Student Data.csv"));
        //        //return Path.Combine(Application.persistentDataPath, "Date/Student Data.csv");
        //        //return "D:'\'User/Student Data.csv";
        //#elif UNITY_ANDROID
        //        return Application.persistentDataPath+"Student Data.csv";
        //#elif UNITY_IPHONE
        //        return Application.persistentDataPath+"/"+"Student Data.csv";
        //#else
        //        return Path.Combine(Application.persistentDataPath, "Date/Student Data.csv");
        //#endif
    }
}
