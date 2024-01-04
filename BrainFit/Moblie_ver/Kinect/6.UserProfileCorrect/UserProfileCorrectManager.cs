using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserProfileCorrectManager : MonoBehaviour
{
    List<Dictionary<string, object>> data;

    public Image myImage;   //얼굴이미지
    public RawImage myRawImg;
    public InputField nameField;   //이름텍스트
    public InputField brithdayField;   //생일텍스트
    public ToggleGroup sexToggleGroup; //성별토글그룹
    public Toggle[] sexToggle;    //성별텍스트
    public InputField contactField;  //연락처텍스트

    public GameObject virtualCanvas;    //가상키보드캔버스
    public VirtualTextInputBox TextInputBox = null; //가상키보드 인풋필드
    public InputField virtualField; //가상키보드 인풋필드
    string btnName;

    string user_name_adr, user_birth_adr, user_contect_adr;

    public Toggle sexCurrentSeletion
    {
        get { return sexToggleGroup.ActiveToggles().FirstOrDefault(); }
    }
    public void SexAllToggleClick()
    {
        if (sexCurrentSeletion.name.Equals("ManToggle"))
        {
            PlayerPrefs.SetString("EP_UserSex", "남자");
        }
        else if (sexCurrentSeletion.name.Equals("WomanToggle"))
        {
            PlayerPrefs.SetString("EP_UserSex", "여자");
        }
    }

    void Start()
    {
        UserDataInit();
    }
    private void OnEnable()
    {
        user_name_adr = PlayerPrefs.GetString("EP_UserNAME");
        user_birth_adr = PlayerPrefs.GetString("EP_UserBrithDay");
        user_contect_adr = PlayerPrefs.GetString("EP_UserContact");
    }

    public void BackButtonClick()
    {
        SceneManager.LoadScene("5.UserExercise");
    }

    //해당 유저 정보를 들고온다.
    void UserDataInit()
    {
        data = CSVReader.Read_("Student Data");

        for (int i = 0; i < data.Count; i++)
        {
            if (data[i]["이름"].ToString().Equals(PlayerPrefs.GetString("EP_UserNAME"))
                   && data[i]["생년월일"].ToString().Equals(PlayerPrefs.GetString("EP_UserBrithDay")))
            {
                PlayerPrefs.SetString("EP_UserNAME", data[i]["이름"].ToString());   //유저이름
                PlayerPrefs.SetString("EP_UserSex", data[i]["성별"].ToString());    //유저성별
                PlayerPrefs.SetString("EP_UserBrithDay", data[i]["생년월일"].ToString());   //유저생년월일
                PlayerPrefs.SetString("EP_UserContact", data[i]["연락처"].ToString()); //유저 연락처
    
            }
        }

        if (PlayerPrefs.GetString("EP_MyFaceChange").Equals("No"))
        {
            //myImage.sprite = Resources.Load<Sprite>("Snapshots/" + PlayerPrefs.GetString("EP_UserNAME"));

            byte[] filedata = File.ReadAllBytes(Application.persistentDataPath + PlayerPrefs.GetString("EP_UserNAME") 
                + "_" + PlayerPrefs.GetString("EP_UserBrithDay") + ".png");
            //byte[] filedata = File.ReadAllBytes(@"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\ScreenShots\" +
            //    PlayerPrefs.GetString("EP_UserNAME") + ".png");

            Texture2D textrue = null;
            textrue = new Texture2D(0, 0);
            textrue.LoadImage(filedata); textrue.LoadImage(filedata);
            myRawImg.texture = textrue;
        } 
        else
        {
            string path = Application.persistentDataPath+ PlayerPrefs.GetString("EP_UserNAME")
                 + "_" + PlayerPrefs.GetString("EP_UserBrithDay")+".png";
            //string path = @"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\ScreenShots\" +
            //    PlayerPrefs.GetString("EP_UserNAME") + ".png";
            File.Delete(path);

            System.IO.File.Move(Application.persistentDataPath + "User.png",
                Application.persistentDataPath  + PlayerPrefs.GetString("EP_UserNAME")
                + "_" + PlayerPrefs.GetString("EP_UserBrithDay")+ ".png");
            //System.IO.File.Move(@"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\ScreenShots\User.png",
            //@"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\ScreenShots\" + PlayerPrefs.GetString("EP_UserNAME") + ".png");

            byte[] filedata = File.ReadAllBytes(Application.persistentDataPath +
                PlayerPrefs.GetString("EP_UserNAME")+ "_" + PlayerPrefs.GetString("EP_UserBrithDay") + ".png");
            //byte[] filedata = File.ReadAllBytes(@"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\ScreenShots\" +
            //    PlayerPrefs.GetString("EP_UserNAME") + ".png");

            Texture2D textrue = null;
            textrue = new Texture2D(0, 0);
            textrue.LoadImage(filedata); textrue.LoadImage(filedata);
            myRawImg.texture = textrue;

            PlayerPrefs.SetString("EP_MyFaceChange", "No"); //사진찍고 왔기때문에 다시 No로 변경

            //string path = "Assets/Resources/Snapshots/" + PlayerPrefs.GetString("EP_UserNAME") + ".png";
            //AssetDatabase.DeleteAsset(path);    //기존의 사진 삭제
            //AssetDatabase.Refresh();    //F5
            //string path2 = "Assets/Resources/Snapshots/SnapShot_User.png";// AssetDatabase.GetAssetPath(Selection.activeObject);
            //TextureImporter tImporter = AssetImporter.GetAtPath(path2) as TextureImporter;
            //tImporter.textureType = TextureImporterType.Sprite; //타입변경
            //tImporter.isReadable = true;    //읽기버전으로 변경
            //AssetDatabase.ImportAsset(path2);
            //AssetDatabase.Refresh();
            //AssetDatabase.RenameAsset("Assets/Resources/Snapshots/SnapShot_User.png", PlayerPrefs.GetString("EP_UserNAME"));
            //AssetDatabase.Refresh();

            //myImage.sprite = Resources.Load<Sprite>("Snapshots/" + PlayerPrefs.GetString("EP_UserNAME"));

            //PlayerPrefs.SetString("EP_MyFaceChange", "No"); //사진찍고 왔기때문에 다시 No로 변경
        }

        nameField.text = PlayerPrefs.GetString("EP_UserNAME");
        brithdayField.text = PlayerPrefs.GetString("EP_UserBrithDay");
        contactField.text = PlayerPrefs.GetString("EP_UserContact");

        //user_name_adr = Application.persistentDataPath + "/ScreenShots/"+PlayerPrefs.GetString("EP_UserNAME")+".png"; //현재 사용자 이름
        //user_birth_adr = PlayerPrefs.GetString("EP_UserBrithDay"); //현재 사용자 생년월일
        //user_contect_adr = PlayerPrefs.GetString("EP_UserContact"); //현재 사용자 연락처

        if (PlayerPrefs.GetString("EP_UserSex").Equals("남자"))
            sexToggle[0].isOn = true;
        else if (PlayerPrefs.GetString("EP_UserSex").Equals("여자"))
            sexToggle[1].isOn = true;


            
    }

    //사진변경버튼 클릭 시 이벤트
    public void MyFaceImageChangeClickOn()
    {
        PlayerPrefs.SetString("EP_MyFaceChange", "Yes"); //사진찍기
        SceneManager.LoadScene("3.FaceShoot");
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
        btnName = "ContactButton";
    }

    //가상키보드를 사용해 입력한 텍스트를 정보란에 입력하는 함수
    public void UserInfoTextShow()
    {
        if (btnName.Equals("NameButton"))
        {
            nameField.text = virtualField.text;
            PlayerPrefs.SetString("EP_UserNAME", nameField.text);   //유저이름
        }
        else if (btnName.Equals("BrithDayButton"))
        {
            brithdayField.text = virtualField.text;
            PlayerPrefs.SetString("EP_UserBrithDay", brithdayField.text);   //유저생년월일
        }
        else if (btnName.Equals("ContactButton"))
        {
            contactField.text = virtualField.text;
            PlayerPrefs.SetString("EP_UserContact", contactField.text); //유저 연락처
        }
     
        TextInputBox.Clear();
        virtualCanvas.SetActive(false);
    }


    //수정하기 버튼 클릭 이벤트
    public void UserProfileCorrectSave()
    {
        data = CSVReader.Read_("Student Data");
     
        //System.IO.File.Move(Application.persistentDataPath + "/ScreenShots/User.png",
        //    Application.persistentDataPath + "/ScreenShots/" + nameField.text +"_"+ brithdayField.text + ".png");
       // Debug.Log("안들어오니 " + data.Count);

        string filePath = getPath();

        //This is the writer, it writes to the filepath
        StreamWriter writer = new StreamWriter(filePath);
        //Debug.Log("뭐닝");
        //This is writing the line of the type, name, damage... etc... (I set these)
        writer.WriteLine("이름,생년월일,성별,연락처");
        //This loops through everything in the inventory and sets the file to these.
        Debug.Log("data.Count: " + data.Count);

        for (int i = 0; i < data.Count; ++i)
        {
            if(data[i]["이름"].ToString().Equals(user_name_adr)
                && data[i]["생년월일"].ToString().Equals(user_birth_adr))
            {
                writer.WriteLine(PlayerPrefs.GetString("EP_UserNAME") +
                "," + PlayerPrefs.GetString("EP_UserBrithDay") +
                "," + PlayerPrefs.GetString("EP_UserSex") +
                "," + PlayerPrefs.GetString("EP_UserContact"));
            }
            else
            {
                writer.WriteLine(data[i]["이름"] +
                "," + data[i]["생년월일"] +
                "," + data[i]["성별"] +
                "," + data[i]["연락처"]);
            }
        }
        writer.Flush();
        //This closes the file
        writer.Close();


        //AssetDatabase.Refresh();
        data = CSVReader.Read_("Student Data");
        SceneManager.LoadScene("5.UserExercise");
    }

    public void UserProfile_Reset()
    {
        //로그인 이미지 이름 변경 
        System.IO.File.Move(Application.persistentDataPath + user_name_adr+"_"+user_birth_adr+ ".png",
            Application.persistentDataPath + PlayerPrefs.GetString("EP_UserNAME") + "_"+ PlayerPrefs.GetString("EP_UserBrithDay") + ".png");
        //게임플레이 데이터 이름 정보 변경
        string user_BrainFile = Application.persistentDataPath + user_name_adr + "_" + user_birth_adr+ "_Brain.csv";
        string user_DementiaFile = Application.persistentDataPath + user_name_adr + "_" + user_birth_adr+ "_Dementia.csv";
        string user_RealFile = Application.persistentDataPath + user_name_adr + "_" + user_birth_adr+ "_Real.csv";
        FileInfo fileInfo_brain = new FileInfo(user_BrainFile);//파일 있는지 확인 있을때(true), 없으면(false)
        FileInfo fileInfo_Dementia = new FileInfo(user_DementiaFile);//파일 있는지 확인 있을때(true), 없으면(false)
        FileInfo fileInfo_Real = new FileInfo(user_RealFile);//파일 있는지 확인 있을때(true), 없으면(false)
       //뇌게임
        if (fileInfo_brain.Exists)
        {
            System.IO.File.Move(Application.persistentDataPath +user_name_adr + "_" + user_birth_adr + "_Brain.csv",
Application.persistentDataPath +PlayerPrefs.GetString("EP_UserNAME") + "_" + PlayerPrefs.GetString("EP_UserBrithDay") +"_Brain.csv");
        }
        //치매예방
        if (fileInfo_Dementia.Exists)
        {
            System.IO.File.Move(Application.persistentDataPath +user_name_adr + "_" + user_birth_adr + "_Dementia.csv",
Application.persistentDataPath +PlayerPrefs.GetString("EP_UserNAME") + "_" + PlayerPrefs.GetString("EP_UserBrithDay") + "_Dementia.csv");
        }
        //손게임
        if (fileInfo_Real.Exists)
        {
            System.IO.File.Move(Application.persistentDataPath+ user_name_adr + "_" + user_birth_adr + "_Real.csv",
Application.persistentDataPath + PlayerPrefs.GetString("EP_UserNAME") + "_" + PlayerPrefs.GetString("EP_UserBrithDay") + "_Real.csv");
        }
        //플레이 일수 데이터 이름 정보 변경
        
        string user_DayFile = Application.persistentDataPath + user_name_adr + "_" + user_birth_adr + "_Day.csv";
        FileInfo fileInfo_Day = new FileInfo(user_DayFile);//파일 있는지 확인 있을때(true), 없으면(false)
        if (fileInfo_Day.Exists)
        {
            System.IO.File.Move(Application.persistentDataPath  + user_name_adr + "_" + user_birth_adr + "_Day.csv",
Application.persistentDataPath + PlayerPrefs.GetString("EP_UserNAME") + "_" + PlayerPrefs.GetString("EP_UserBrithDay") + "_Day.csv");
        }
    }
    private string getPath()
    {
        return Application.persistentDataPath + "Student Data.csv";
        //return @"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\Date\Student Data.csv";
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

}
