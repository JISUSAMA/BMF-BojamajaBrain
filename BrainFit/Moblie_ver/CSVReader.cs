using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class CSVReader
{
    //https://mentum.tistory.com/214
  //  static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string SPLIT_RE = @",";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
    public string[] m_ColumnHeadings = { "Name", "Birth", "PlayDate", "GameKind", "Score" };

    public static List<Dictionary<string, object>> Read_(string file)
    {
        var list = new List<Dictionary<string, object>>();

        //유니티 Resources폴더에서 불러오기
        //TextAsset data = Resources.Load(file) as TextAsset;
        //var lines = Regex.Split(data.text, LINE_SPLIT_RE);
        //Debug.Log(Application.persistentDataPath + "/Date/Student Data.csv");
        //Debug.Log(Path.Combine(Application.persistentDataPath, "Date/Student Data.csv"));
        //외부
        string source;
        StreamReader sr = new StreamReader(Application.persistentDataPath +"/Date/" + file + ".csv");
        //Debug.Log("sr + :: " + sr);
        //StreamReader sr = new StreamReader(@"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\Date\" + file + ".csv");
        source = sr.ReadToEnd();
        sr.Close();
        var lines = Regex.Split(source, LINE_SPLIT_RE);


        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }


    public static List<Dictionary<string, string>> Read(string file)
    {
        var list = new List<Dictionary<string, string>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, string>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                string finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n.ToString();
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f.ToString();
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
    public static List<Dictionary<string, object>> Read_file(string file)
    {
        var list = new List<Dictionary<string, object>>();
        //외부
        string source;
        StreamReader sr = new StreamReader(Application.persistentDataPath + "/" + file + ".csv");
        Debug.Log("sr + :: " + sr);

        source = sr.ReadToEnd();
        sr.Close();
        var lines = Regex.Split(source, LINE_SPLIT_RE);


        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}
