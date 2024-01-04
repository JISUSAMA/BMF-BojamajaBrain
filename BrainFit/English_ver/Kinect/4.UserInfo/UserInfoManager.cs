using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class UserInfoManager : MonoBehaviour
{
    List<Dictionary<string, object>> data;


    public GameObject virtualCanvas;    //가상키보드캔버스
    public VirtualTextInputBox TextInputBox = null; //가상키보드 인풋필드
    public InputField virtualField; //가상키보드 인풋필드
    public InputField nameInputField;   //사용자이름필드
    public InputField brithdayField;    //사용자생년월드필드
    public InputField ContactField;  //사용자 연락처필드
    public ToggleGroup sexToggleGroup; //성별토글그룹

    bool cantMakeID = false;
    string btnName;
    bool nextBtn;   //[다음으로]버튼 클릭 여부

    public Text userName;
    public Text userBirth;

    public GameObject join_complete_ob;
    public GameObject join_complete_particle;
    public GameObject SystemPopup_ob;
    public Toggle sexCurrentSeletion
    {
        get { return sexToggleGroup.ActiveToggles().FirstOrDefault(); }
    }


    void Start()
    {
        data = CSVReader.Read_("Student Data");
        Debug.Log("name : "+nameInputField.text.Length);
        PlayerPrefs.SetString("EP_UserSex", "여자"); //기본 여성으로 설정
    }

    public void SexAllToggleClick()
    {
        if(sexCurrentSeletion.name.Equals("ManToggle"))
        {
            PlayerPrefs.SetString("EP_UserSex", "남자");
        }
        else if(sexCurrentSeletion.name.Equals("WomanToggle"))
        {
            PlayerPrefs.SetString("EP_UserSex", "여자");
        }
    }


    public void NextButtonOnClick()
    {
        cantMakeID = false;
        for (int j = 0; j < data.Count; j++)
        {
            Debug.Log("index " + (j).ToString() + " : " + data[j]["이름"] + " " + data[j]["생년월일"] + " " + data[j]["성별"]);
            Debug.Log("EP_UserNAME :" + PlayerPrefs.GetString("EP_UserNAME") + "EP_UserBrithDay :" + PlayerPrefs.GetString("EP_UserBrithDay"));
            Debug.Log("이름 :" + data[j]["이름"] + "생년월일 :" + data[j]["생년월일"]);
            string name = data[j]["이름"].ToString();
            string birth = data[j]["생년월일"].ToString();
            //아이디 이미 존재
            if (PlayerPrefs.GetString("EP_UserNAME").Equals(name))
            {
                if (PlayerPrefs.GetString("EP_UserBrithDay").Equals(birth))
                {
                    cantMakeID = true;
                }
           //     else { cantMakeID = false; }
                Debug.Log("----------------1" + cantMakeID);
            }
         //   else { cantMakeID = false; Debug.Log("----------------2" + cantMakeID); }
        }
        if (cantMakeID)
        {
            Debug.Log("해당 아이디는 이미 존재합니다.");
            SystemPopup_ob.SetActive(true);
        }
        else
        {
            if (nameInputField.text.Length != 0 || brithdayField.text.Length != 0 || ContactField.text.Length != 0)
            {
                nextBtn = true;
                join_complete_ob.SetActive(true);
                join_complete_particle.SetActive(true);
                userName.text = nameInputField.text;
                userBirth.text = brithdayField.text;

                string File_name = userName.text + "_" + userBirth.text; //유저 이름_ 유저 생일
                System.IO.File.Move(Application.persistentDataPath + "/ScreenShots/User.png",
                    Application.persistentDataPath + "/ScreenShots/" + File_name + ".png");
                Debug.Log("File_name name : " + File_name);
                //AssetDatabase.RenameAsset("Assets/Resources/Snapshots/SnapShot_User.png", PlayerPrefs.GetString("EP_UserNAME"));
                // AssetDatabase.RenameAsset("Assets/Resources/Snapshots", PlayerPrefs.GetString("EP_UserNAME"));
                //AssetDatabase.Refresh();
            }
        }
    }

    public void UserDataSaveOnClick()
    {
        //Debug.Log(PlayerPrefs.GetString("EP_UserNAME") + ", " +
        //    PlayerPrefs.GetString("EP_UserSex") + ", " +
        //    PlayerPrefs.GetString("EP_UserBrithDay") + ", " +
        //    PlayerPrefs.GetString("EP_UserWight") + ", " +
        //    PlayerPrefs.GetString("EP_UserDisease_1") + ", " +
        //    PlayerPrefs.GetString("EP_UserDisease_7") + ", " +
        //    PlayerPrefs.GetString("EP_OthersDisease") + ", ");

        SaveInventory();
        StartCoroutine(GoLoginScene());
    }

    IEnumerator GoLoginScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("1.Login");
    }

    public void BackButtonClickOn()
    {
        SceneManager.LoadScene("2.LoginResult");
    }

    public void SaveInventory()
    {
        data = CSVReader.Read_("Student Data");

        Debug.Log("안들어오니 " + data.Count);
        string filePath = getPath();

        //This is the writer, it writes to the filepath
        StreamWriter writer = new StreamWriter(filePath);
        Debug.Log("뭐닝");
        //This is writing the line of the type, name, damage... etc... (I set these)
        writer.WriteLine("이름,생년월일,성별,연락처");
        //This loops through everything in the inventory and sets the file to these.
        Debug.Log("data.Count: " + data.Count);

        for (int i = 0; i < data.Count + 1; ++i)
        {
            Debug.Log("-----" + (data.Count - 1));
            if (i <= data.Count - 1)
            {
                Debug.Log("------------------------------------1");
                writer.WriteLine(data[i]["이름"] +
                "," + data[i]["생년월일"] +
                "," + data[i]["성별"] +
                "," + data[i]["연락처"]);

            }
            else if (i.Equals(data.Count))
            {
                Debug.Log("------------------------------------2");
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                PlayerPrefs.SetString("EP_StartDay", today);
                PlayerPrefs.SetString("EP_EndDay", today);
                PlayerPrefs.SetString("EP_Progress", "No");

                writer.WriteLine(PlayerPrefs.GetString("EP_UserNAME") +
                "," + PlayerPrefs.GetString("EP_UserBrithDay") +
                "," + PlayerPrefs.GetString("EP_UserSex") +
                "," + PlayerPrefs.GetString("EP_UserContact"));

            }
        }
        Debug.Log("------------------------------------3");
        writer.Flush();
        //This closes the file
        writer.Close();

        //AssetDatabase.Refresh();
        data = CSVReader.Read_("Student Data");
    }

    private string getPath()
    {
        //return @"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\Date\Student Data.csv";
        return Application.persistentDataPath + "/Date/Student Data.csv";
        //#if UNITY_EDITOR
        //        //return Application.dataPath + "/Resources/" + "Student Data.csv";
        //        return Path.Combine(Application.persistentDataPath, "Date/Student Data.csv");
        //#elif UNITY_ANDROID
        //        return Application.persistentDataPath+"Student Data.csv";
        //#elif UNITY_IPHONE
        //        return Application.persistentDataPath+"/"+"Student Data.csv";
        //#else
        //        return Path.Combine(Application.persistentDataPath, "Date/Student Data.csv");
        //#endif
    }

    ///각각의 인풋필드 선택 시 클릭 이벤트 함수
    public void NameButtonOnClick()
    {
        TextInputBox.Clear();
        btnName = "NameButton";
    }

    public void BrithDayButtonOnClick()
    {
        TextInputBox.Clear();
        btnName = "BrithDayButton";
    }


    public void ContactButtonOnClick()
    {
        TextInputBox.Clear();
        btnName = "ContactInfoButton";
    }


    //가상키보드를 사용해 입력한 텍스트를 정보란에 입력하는 함수
    public void UserInfoTextShow()
    {
        if(nextBtn.Equals(false))
        {
            if (btnName.Equals("NameButton"))
            {
                nameInputField.text = virtualField.text;
                PlayerPrefs.SetString("EP_UserNAME", nameInputField.text);   //유저이름
            }
            else if (btnName.Equals("BrithDayButton"))
            {
                brithdayField.text = virtualField.text;
                PlayerPrefs.SetString("EP_UserBrithDay", brithdayField.text);   //유저생년월일
            }
            else if (btnName.Equals("ContactInfoButton"))
            {
                ContactField.text = virtualField.text;
                PlayerPrefs.SetString("EP_UserContact", ContactField.text); //유저 연락처
            }
        }
     

        TextInputBox.Clear();
        virtualCanvas.SetActive(false);
    }
    public void LoginButtonClick()
    {
        PlayerPrefs.SetString("CARE_PlayMode", "LoginMode");
        SceneManager.LoadScene("1.Login");
    }
}
