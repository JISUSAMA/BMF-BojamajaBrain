using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginResultManager : MonoBehaviour
{
    List<Dictionary<string, object>> data;

    public GameObject noUserPanel;
    public GameObject yesUserPanel;

    public Text nameText;   //이름텍스트
    public Text wellcomeText;   //이름텍스트
    public Text brithdayText;   //생년월일텍스트
    public Image userFaceImage; //회원이미지
    public RawImage userFaceRawImg;

    public Image user_gender_img;
    public Sprite[] user_gender_sp;



    void Start()
    {
        data = CSVReader.Read_("Student Data");

        //회원정보가 있을 경우
        if(PlayerPrefs.GetString("EP_UserNAME") != "")
        {
            yesUserPanel.SetActive(true);
            for (var i = 0; i < data.Count; i++)
            {
                //Debug.Log("index " + (i).ToString() + " : " + data[i]["이름"] + " " + data[i]["생년월일"] + " " + data[i]["성별"]);
                //string str = data[i]["이름"].ToString();
                //Debug.Log("str " + str);
                //동일한 이름이 있다면
                if (data[i]["이름"].ToString().Equals(PlayerPrefs.GetString("EP_UserNAME")))
                {
                    PlayerPrefs.SetString("EP_UserBrithDay", data[i]["생년월일"].ToString());
                    PlayerPrefs.SetString("EP_UserSex", data[i]["성별"].ToString());

                    //userFaceImage.sprite = Resources.Load<Sprite>("Snapshots/" + PlayerPrefs.GetString("EP_UserNAME"));

                    byte[] filedata = File.ReadAllBytes(Application.persistentDataPath + "/ScreenShots/" + PlayerPrefs.GetString("EP_UserNAME")+ "_" +PlayerPrefs.GetString("EP_UserBrithDay") + ".png");
                    //byte[] filedata = File.ReadAllBytes(@"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\ScreenShots\" + PlayerPrefs.GetString("EP_UserNAME") + ".png");

                    Texture2D textrue = null;
                    textrue = new Texture2D(0, 0);
                    textrue.LoadImage(filedata); textrue.LoadImage(filedata);
                    userFaceRawImg.texture = textrue;

                    nameText.text = PlayerPrefs.GetString("EP_UserNAME");
                    wellcomeText.text = PlayerPrefs.GetString("EP_UserNAME");
                    if (PlayerPrefs.GetString("EP_UserSex").Equals("여자")) { user_gender_img.sprite = user_gender_sp[0]; }
                    else if (PlayerPrefs.GetString("EP_UserSex").Equals("남자")) { user_gender_img.sprite = user_gender_sp[1]; }
                    brithdayText.text = PlayerPrefs.GetString("EP_UserBrithDay");
                    
                }
            }
        }
        else
        {
            noUserPanel.SetActive(true);
        }

        Debug.Log("str??? " + PlayerPrefs.GetString("EP_UserNAME"));
    }

    
    void Update()
    {
        
    }

    


    private string getPath()
    {
        return Application.persistentDataPath + "/Date/Student Data.csv";
        //return @"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\Date\Student Data.csv";
        //#if UNITY_EDITOR
        //        return Application.dataPath + "/Resources/" + "Student Data.csv";
        //#elif UNITY_ANDROID
        //        return Application.persistentDataPath+"Student Data.csv";
        //#elif UNITY_IPHONE
        //        return Application.persistentDataPath+"/"+"Student Data.csv";
        //#else
        //        return Application.dataPath +"/"+"Student Data.csv";
        //#endif
    }
}
