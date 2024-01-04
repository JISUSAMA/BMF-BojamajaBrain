using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI_Manager : MonoBehaviour
{
    public RawImage userFaceRawImg;
    public RawImage MyRawImage;
    //    public Toggle othersToggle; //기타토글
    //    public GameObject othersObj;    //기타필드오브젝트



    void Start()
    {
        //userFaceImg.sprite = Resources.Load<Sprite>("Snapshots/SnapShot_User");

        byte[] filedata = File.ReadAllBytes(Application.persistentDataPath + "/ScreenShots/User.png");
        //byte[] filedata = File.ReadAllBytes(@"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\ScreenShots\User.png");

        Texture2D textrue = null;
        textrue = new Texture2D(0, 0);
        textrue.LoadImage(filedata); textrue.LoadImage(filedata);
        userFaceRawImg.texture = textrue;
        MyRawImage.texture = textrue;
    }

}
