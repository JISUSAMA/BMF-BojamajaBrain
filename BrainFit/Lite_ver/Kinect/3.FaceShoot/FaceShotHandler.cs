using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FaceShotHandler : MonoBehaviour
{
    //public static FaceShotHandler instance { get; private set; }

    public Camera myCamera;
    WebCamTexture camTexture;
    public RawImage display;
    int currentIndex = 0;

    bool takeScreenShotOnNextFrame;
    RenderTexture renderTexture;


    private void Awake()
    {
        //if (instance != null)
        //    Destroy(this);
        //else instance = this;

        myCamera = gameObject.GetComponent<Camera>();
    }

    void Start()
    {
        WebCamShow();
    }

    //화면에 얼굴 나오게 하는 함수
    void WebCamShow()
    {
        if (camTexture != null)
        {
            Debug.Log("뭐지");
            display.texture = null;
            camTexture.Stop();
            camTexture = null;
        }
      

        for (int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            Debug.Log(i + "  " + WebCamTexture.devices[i].name);
            if (WebCamTexture.devices[i].name.Equals("Integrated Webcam"))
                currentIndex = i;
        }


        WebCamDevice device = WebCamTexture.devices[currentIndex];
        camTexture = new WebCamTexture(device.name);
        display.texture = camTexture;

        camTexture.Play();
        //      StartCoroutine(Playstop());
    }

    IEnumerator Playstop()
    {
        yield return new WaitForSeconds(3.5f);
        //camTexture.Stop();
        Time.timeScale = 0;
        yield return new WaitForSeconds(1f);
        //camTexture.Play();
        Time.timeScale = 1;
    }



    private void OnPostRender()
    {
        if (takeScreenShotOnNextFrame)
        {
            takeScreenShotOnNextFrame = false;
            renderTexture = myCamera.targetTexture;

            //Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height,
            //    TextureFormat.ARGB32, false);
            //Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            Texture2D renderResult = new Texture2D(300, 300,
                TextureFormat.ARGB32, false);
            Rect rect = new Rect(448,345, 700, 700);
            Debug.Log(renderTexture.width + "  " + renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);


            byte[] byteArray = renderResult.EncodeToPNG();
            string fileName = SnapshotName();

            //유니티 내 폴더
            //System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/Snapshots/SnapShot_" + SnapshotName(), byteArray); //Application.dataPath + "/Test.png"

            //외부 폴더
            File.WriteAllBytes(Application.persistentDataPath + "/ScreenShots/" + SnapshotName(), byteArray);
            //File.WriteAllBytes(@"C:\Users\user\AppData\LocalLow\DefaultCompany\FaceRecognition_v1\ScreenShots\" + SnapshotName(), byteArray);


            //System.IO.File.WriteAllBytes(Application.persistentDataPath + "'\'ScreenShots" + SnapshotName(), byteArray);
            //File.WriteAllBytes(Application.persistentDataPath + "'\'ScreenShots" + SnapshotName(), byteArray);
            Debug.Log(Path.Combine(Application.persistentDataPath, "ScreenShots/" + SnapshotName()));
            Debug.Log("Saved Camera");

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }

    string SnapshotName()
    {
        //return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png",
        //    Application.dataPath,
        //    renderTexture.width,
        //    renderTexture.height,
        //    System.DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss"));

        string today = "User";//System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        return today + ".png";
    }

    void TakeScreenShot(int _width, int _height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(_width, _height, 16);
        takeScreenShotOnNextFrame = true;
    }

    //사진 찍는 함수
    public void TakeScreenShot_Static(int _width, int _height)
    {
        TakeScreenShot(_width, _height);
        //AssetDatabase.Refresh();    //새로고침(f5개념)
    }

    private void OnDestroy()
    {
        camTexture?.Stop();
    }
}