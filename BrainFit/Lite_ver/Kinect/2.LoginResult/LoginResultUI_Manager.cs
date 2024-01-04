using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginResultUI_Manager : MonoBehaviour
{
    

    //회원가입
    public void JoinOnButton()
    {
        SceneManager.LoadScene("3.FaceShoot");
    }

    //게스트입장
    public void GuestButtonOn()
    {
        SceneManager.LoadScene("5.GuestExercise");
    }

    //다시하기
    public void RePlayButton()
    {
        SceneManager.LoadScene("1.Login");
    }

    public void LoginButtonOn()
    {
        SceneManager.LoadScene("5.UserExercise");
    }

    public void SystemCloseBtnClickOn()
    {
        Application.Quit();
    }

}
