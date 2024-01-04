using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamView : MonoBehaviour
{
    public RawImage display;
    WebCamTexture camTexture;
    int currentIndex = 0;


    void Start()
    {
        UIWebCam();
    }

    public void Plane3DWebCam()
    {
        WebCamTexture web = new WebCamTexture(520, 520);
        GetComponent<MeshRenderer>().material.mainTexture = web;
        web.Play();
    }

    public void UIWebCam()
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

    
}
