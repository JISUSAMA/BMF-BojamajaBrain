using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseGame_UIManager : MonoBehaviour
{

    [Header("Setting Grup")]
    public Toggle TutorialToggle;
    private void Start()
    {
        //튜토리얼
        SettingToggle();
    }
    //뇌 훈련 게임 버튼 클릭
    public void OnClick_BrainGame()
    {
        GameAppManager.instance.Initialization();
        GameAppManager.instance.GameKind = "Brain";
        while (GameAppManager.instance.QuestionStringList.Count < GameAppManager.instance.Brain_QusetionCount)
        {
            int rand_input = UnityEngine.Random.Range(0, GameAppManager.instance.Brain_QusetionCount);
            if (GameAppManager.instance.QuestionStringList.Count != 0)
            {
                if (!GameAppManager.instance.QuestionStringList.Contains(GameAppManager.instance.Brain_gameName[rand_input]))
                {
                    GameAppManager.instance.rand_GameNumber.Add(rand_input);
                    GameAppManager.instance.QuestionStringList.Add(GameAppManager.instance.Brain_gameName[rand_input]);

                }
            }
            else
            {
                GameAppManager.instance.rand_GameNumber.Add(rand_input);
                GameAppManager.instance.QuestionStringList.Add(GameAppManager.instance.Brain_gameName[rand_input]);
            }
        }
        GameAppManager.instance.GameLoadScene();
    }
    //치매 예방 게임 버튼 클릭
    public void OnClick_Dementia()
    {
        GameAppManager.instance.Initialization();
        GameAppManager.instance.GameKind = "Dementia";
        while (GameAppManager.instance.QuestionStringList.Count < GameAppManager.instance.Dementia_QusetionCount)
        {
            int rand_input = UnityEngine.Random.Range(0, GameAppManager.instance.Dementia_QusetionCount);
            if (GameAppManager.instance.QuestionStringList.Count != 0)
            {
                if (!GameAppManager.instance.QuestionStringList.Contains(GameAppManager.instance.Dementia_gameName[rand_input]))
                {
                    GameAppManager.instance.rand_GameNumber.Add(rand_input);
                    GameAppManager.instance.QuestionStringList.Add(GameAppManager.instance.Dementia_gameName[rand_input]);
                }
            }
            else
            {
                GameAppManager.instance.rand_GameNumber.Add(rand_input);
                GameAppManager.instance.QuestionStringList.Add(GameAppManager.instance.Dementia_gameName[rand_input]);
            }
        }
        GameAppManager.instance.GameLoadScene();
    }
    //손동작 게임 버튼 클릭
    public void OnClick_LeapMotion()
    {
        GameAppManager.instance.Initialization();
        GameAppManager.instance.GameKind = "Real";
        while (GameAppManager.instance.LeapMotion_PlayList.Count < GameAppManager.instance.LeapMotion_SceneName.Length)
        {
            int random = UnityEngine.Random.Range(0, GameAppManager.instance.LeapMotion_SceneName.Length);
            Debug.Log(GameAppManager.instance.LeapMotion_SceneName[random]);
            if (GameAppManager.instance.LeapMotion_PlayList.Count != 0)
            {
                if (!GameAppManager.instance.LeapMotion_PlayList.Contains(GameAppManager.instance.LeapMotion_SceneName[random]))
                {
                   GameAppManager.instance.LeapMotion_PlayList.Add(GameAppManager.instance.LeapMotion_SceneName[random]);
                    GameAppManager.instance.LeapMotion_PlayNumList.Add(random);
                }
            }
            else
            {
                GameAppManager.instance.LeapMotion_PlayList.Add(GameAppManager.instance.LeapMotion_SceneName[random]);
                GameAppManager.instance.LeapMotion_PlayNumList.Add(random);
            }
        }

        GameAppManager.instance.GameLoadScene();
    }
    public void OnClick_RestVideo(string url)
    {
        AppSoundManager.Instance.StopBGM();
        Application.OpenURL(url);
    }
    public void LoadSceneName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void BackButton_ForChooseGame()
    {
        //게스트가 아닌경우, 데이터 저장!
        if (PlayerPrefs.GetString("CARE_PlayMode") != "GuestMode")
        {
            SceneManager.LoadScene("5.UserExercise");
        }
        else
        {
            SceneManager.LoadScene("0.Main");
        }
    }
    //팝업창 열릴 때, 셋팅 설정하기 
    public void SettingToggle()
    {
        //튜토리얼
        if (PlayerPrefs.HasKey("TutorialPlay"))
        {
            string tutorial = PlayerPrefs.GetString("TutorialPlay");
            if (tutorial.Equals("true"))
            {
                TutorialToggle.isOn = true;
                PlayerPrefs.SetString("TutorialPlay", "true");
            }
            else
            {
                TutorialToggle.isOn = false;
                PlayerPrefs.SetString("TutorialPlay", "false");
            }
        }
        else
        {
            PlayerPrefs.SetString("TutorialPlay", "true");
            TutorialToggle.isOn = true;
        }
    }
    public void SettingToggle_Tutorial()
    {
        string tutorial = PlayerPrefs.GetString("TutorialPlay");
        Debug.Log("Tutorial :" + PlayerPrefs.GetString("TutorialPlay"));
        if (TutorialToggle.isOn)
        {
            PlayerPrefs.SetString("TutorialPlay", "true");
        }
        else
        {
            PlayerPrefs.SetString("TutorialPlay", "false");
        }
    }
}
