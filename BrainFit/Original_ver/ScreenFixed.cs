using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenFixed : MonoBehaviour
{

    public Camera camera;


    private void Start()
    {
        //SetResolution(); // 초기에 게임 해상도 고정
        Camera2View();
    }

    /* 해상도 설정하는 함수 */
    //public void SetResolution()
    //{
    //    int setWidth = 1440; // 사용자 설정 너비
    //    int setHeight = 2560; // 사용자 설정 높이

    //    int deviceWidth = Screen.width; // 기기 너비 저장
    //    int deviceHeight = Screen.height; // 기기 높이 저장

    //    Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

    //    if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
    //    {
    //        float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
    //        camera.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
    //    }
    //    else // 게임의 해상도 비가 더 큰 경우
    //    {
    //        float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
    //        camera.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
    //    }
    //}
    public void SetResolution()
    {
        int setWidth = 2560; // 사용자 설정 너비
        int setHeight = 1440; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution((int)(((float)deviceWidth / deviceHeight) * setHeight) , setHeight, true); // SetResolution 함수 제대로 사용하기

        if ((float)setHeight / setWidth < (float)deviceHeight / deviceWidth) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setHeight / setWidth) / ((float)deviceHeight / deviceWidth); // 새로운 너비
            camera.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceHeight / deviceWidth) / ((float)setHeight / setWidth); // 새로운 높이
            camera.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        
        }
        Debug.Log("(float)setWidth / setHeight "+ (float)setWidth / setHeight + "(float)deviceWidth / deviceHeight"+ (float)deviceWidth / deviceHeight);
    }
    public void Camera2View()
    {
        Rect rt = camera.rect;

        float scale_height = ((float)Screen.width / Screen.height) / ((float)16 /9);   //가로
        float scale_width = 1f / scale_height;

        if (scale_height < 1)
        {
            rt.height = scale_height;
            rt.y = (1f - scale_height) / 2f;
        }
        else
        {
            rt.width = scale_width;
            rt.x = (1f - scale_width) / 2f;
        }

        camera.rect = rt;
    }

}

