//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.Windows.WebCam;

//public class CameraShot : MonoBehaviour
//{
//    PhotoCapture photoCaptureObject = null;
//    Texture2D targetTexture = null;

//    // Use this for initialization
//    void Start()
//    {
//        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
//        targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

//        // phoocapture개체를 만들어 시작
//        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject) {
//            photoCaptureObject = captureObject;
//            CameraParameters cameraParameters = new CameraParameters();
//            cameraParameters.hologramOpacity = 0.0f;
//            cameraParameters.cameraResolutionWidth = cameraResolution.width;
//            cameraParameters.cameraResolutionHeight = cameraResolution.height;
//            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

//            // Activate the camera
//            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result) {
//                // Take a picture
//                photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
//            });
//        });
//    }

//    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
//    {
//        // Copy the raw image data into the target texture
//        photoCaptureFrame.UploadImageDataToTexture(targetTexture);

//        // Create a GameObject to which the texture can be applied
//        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
//        Renderer quadRenderer = quad.GetComponent<Renderer>() as Renderer;
//        quadRenderer.material = new Material(Shader.Find("Custom/Unlit/UnlitTexture"));

//        quad.transform.parent = this.transform;
//        quad.transform.localPosition = new Vector3(0.0f, 0.0f, 3.0f);

//        quadRenderer.material.SetTexture("_MainTex", targetTexture);

//        // Deactivate the camera
//        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
//    }

//    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
//    {
//        // Shutdown the photo capture resource
//        photoCaptureObject.Dispose();
//        photoCaptureObject = null;
//    }

//    //public Camera camera;       //보여지는 카메라.

//    //private int resWidth;
//    //private int resHeight;
//    //string path;
//    //// Use this for initialization
//    //void Start()
//    //{
//    //    resWidth = Screen.width;
//    //    resHeight = Screen.height;
//    //    path = Application.dataPath + "/ScreenShot/";
//    //    Debug.Log(path);
//    //}

//    //public void ClickScreenShot()
//    //{
//    //    DirectoryInfo dir = new DirectoryInfo(path);
//    //    if (!dir.Exists)
//    //    {
//    //        Directory.CreateDirectory(path);
//    //    }
//    //    string name;
//    //    name = path + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
//    //    RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
//    //    camera.targetTexture = rt;
//    //    Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
//    //    Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);
//    //    camera.Render();
//    //    RenderTexture.active = rt;
//    //    screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
//    //    screenShot.Apply();

//    //    byte[] bytes = screenShot.EncodeToPNG();
//    //    File.WriteAllBytes(name, bytes);
//    //}
//}
