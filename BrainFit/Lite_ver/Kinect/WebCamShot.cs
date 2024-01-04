using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Camera))]
public class WebCamShot : MonoBehaviour
{
    public RawImage display;
    WebCamTexture camTexture;
    int currentIndex = 0;


    Camera snapCam;

    int resWidth = 520;
    int resHeight = 520;

    // Start is called before the first frame update
    void Start()
    {
        WebCamShow();
        snapCam = GetComponent<Camera>();
        if(snapCam.targetTexture.Equals(null))
        {
            snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 0);
        }
        else
        {
            resWidth = snapCam.targetTexture.width;
            resHeight = snapCam.targetTexture.height;
        }
        snapCam.gameObject.SetActive(true);
    }

    void WebCamShow()
    {
        if(camTexture != null)
        {
            display.texture = null;
            camTexture.Stop();
            camTexture = null;
        }

        WebCamDevice device = WebCamTexture.devices[currentIndex];
        camTexture = new WebCamTexture(device.name);
        display.texture = camTexture;
        camTexture.Play();
    }


    public void CallTakeSnapShot()
    {
        snapCam.gameObject.SetActive(true);
    }

    void LateUpdate()
    {   
        if(snapCam.gameObject.activeInHierarchy)
        {
            Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            snapCam.Render();
            RenderTexture.active = snapCam.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);

            byte[] bytes = snapshot.EncodeToPNG();
            string fileName = SnapshotName();
            System.IO.File.WriteAllBytes(fileName, bytes);
            Debug.Log("Snpashot taken!");
            snapCam.gameObject.SetActive(false);
        }
    }

    string SnapshotName()
    {
        return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png",
            Application.dataPath,
            resWidth,
            resHeight,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss"));
    }
}
