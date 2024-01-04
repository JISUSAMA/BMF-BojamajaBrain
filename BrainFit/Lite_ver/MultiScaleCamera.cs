using UnityEngine;
using UnityEngine.Rendering;
public class MultiScaleCamera : MonoBehaviour
{
    // 고정해상도를 적용하고 남는 부분(Letterbox)에 사용될 게임오브젝트(Prefab)
    public Camera camera;
    private void Start()
    {//
        Debug.Log("Start");
        GL.Clear(true, true, Color.white);
        //SetResolution(); // 초기에 게임 해상도 고정
        //Camera2View();
        Debug.Log("111111111111111111111111111");
        SetResolution1(); // 초기에 게임 해상도 고정
        Debug.Log("22222222222222222222222222222222222");
        // 시작시 한번 실행(게임 실행중에 해상도가 변경되면 다시 호출)
        //UpdateResolution();
    }
    // 해상도 설정하는 함수 
    public void SetResolution1()
    {
        int setWidth = 1440; // 사용자 설정 너비
        int setHeight = 2560; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        //Debug.Log(deviceWidth);
        //Debug.Log(deviceHeight);

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            camera.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            camera.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }

        GL.Clear(true, true, Color.white);
    }

  
    void OnPreCull() => GL.Clear(true, true, Color.white);

  

}