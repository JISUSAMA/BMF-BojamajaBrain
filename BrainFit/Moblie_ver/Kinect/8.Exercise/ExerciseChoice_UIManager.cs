using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExerciseChoice_UIManager : MonoBehaviour
{
    
    public void SitButtonOn()
    {
        KinectExerciseUserData.instance.SetKinectExerciseModeSave("Sit");
        SceneManager.LoadScene("9.Exercise");
    }

    public void StandButtonOn()
    {
        KinectExerciseUserData.instance.SetKinectExerciseModeSave("Stand");
        SceneManager.LoadScene("9.Exercise");
    }
    public void ChooseBackBtn()
    {
        SceneManager.LoadScene("8.ChooseGame");
    }
}
