using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPageManager : MonoBehaviour
{

    bool motion_open_b = true;
    public Text debug_file_;

    [System.Serializable]
    public struct Setting_grup
    {
        public GameObject SettingPopup;
        public Toggle SoundToggle;
        public Toggle TutorialToggle;
        public Slider VolumSlider;
        public float volum;
    }
    [Header("Setting Grup")]
    public Setting_grup setting; 

    private void OnEnable()
    {
        AppSoundManager.Instance.PlayBGM("MainBGM");
        SettingToggle();
        //setting.volum = PlayerPrefs.GetFloat("SoundValue");
        //AppSoundManager.Instance.audioMixer.SetFloat("SFX", setting.volum);
        //AppSoundManager.Instance.audioMixer.SetFloat("BGM", setting.volum);
        //setting.VolumSlider.value = setting.volum;
        Debug.Log("Sound : " + setting.VolumSlider.value);
    }
    //게임 시작 버튼
    public void GameStartButtonClick()
    {
        SceneManager.LoadScene("8.ChooseGame");
    }

    public void SystemCloseBtnClickOn()
    {
        Application.Quit();
    }
    public void OpenSettingPopup()
    {
        setting.SettingPopup.SetActive(true);
        setting.volum = PlayerPrefs.GetFloat("SoundValue");

        AppSoundManager.Instance.audioMixer.SetFloat("SFX", setting.volum);
        AppSoundManager.Instance.audioMixer.SetFloat("BGM", setting.volum);
        setting.VolumSlider.value = setting.volum;
    //    Debug.Log("Tutorial :" + PlayerPrefs.GetString("TutorialPlay"));
        SettingToggle();

      
    }
    //팝업창 열릴 때, 셋팅 설정하기 
    public void SettingToggle()
    {
        //사운드 
        if (PlayerPrefs.HasKey("SoundPlay"))
        {
            string sound =  PlayerPrefs.GetString("SoundPlay");
            if (sound.Equals("true"))
            {
                //사운드 켜짐
                setting.SoundToggle.isOn = true;
                setting.VolumSlider.value = setting.volum;
                Debug.Log("Sound : " + setting.VolumSlider.value);
                PlayerPrefs.SetString("SoundPlay", "true");
            }
            else
            {
                //사운드 꺼짐
                setting.SoundToggle.isOn = false;
                AppSoundManager.Instance.audioMixer.SetFloat("SFX", -65f);
                AppSoundManager.Instance.audioMixer.SetFloat("BGM", -65f);
                PlayerPrefs.SetString("SoundPlay", "false");
            }
        }
        else
        {
            PlayerPrefs.SetString("SoundPlay", "true");
            setting.SoundToggle.isOn = true;
        }

        //튜토리얼
        if (PlayerPrefs.HasKey("TutorialPlay"))
        {
            string tutorial = PlayerPrefs.GetString("TutorialPlay");
            if (tutorial.Equals("true"))
            {
                setting.TutorialToggle.isOn = true;
                PlayerPrefs.SetString("TutorialPlay", "true");
            }
            else
            {
                setting.TutorialToggle.isOn = false;
                PlayerPrefs.SetString("TutorialPlay", "false");
            }
        }
        else
        {
            PlayerPrefs.SetString("TutorialPlay", "true");
            setting.TutorialToggle.isOn = true;
        }
    }

    public void Volum_Setting()
    {
        if (setting.SoundToggle.isOn  == true)
        {
            AppSoundManager.Instance.audioMixer.SetFloat("SFX", setting.VolumSlider.value);
            AppSoundManager.Instance.audioMixer.SetFloat("BGM", setting.VolumSlider.value);
            PlayerPrefs.SetFloat("SoundValue", setting.VolumSlider.value);
            PlayerPrefs.SetString("SoundPlay", "true");

            Debug.Log("Sound : " + setting.VolumSlider.value);
        }
        else
        {
            //사운드 꺼짐
            AppSoundManager.Instance.audioMixer.SetFloat("SFX", -64);
            AppSoundManager.Instance.audioMixer.SetFloat("BGM", -64);
            PlayerPrefs.SetFloat("SoundValue", setting.VolumSlider.value);
            PlayerPrefs.SetString("SoundPlay", "false");
        }
    }
    public void SettingToggle_Sound()
    {
        //사운드가 꺼짐
        if (setting.SoundToggle.isOn)
        {
            AppSoundManager.Instance.audioMixer.SetFloat("SFX", setting.VolumSlider.value);
            AppSoundManager.Instance.audioMixer.SetFloat("BGM", setting.VolumSlider.value);
            PlayerPrefs.SetString("SoundPlay", "true");

        }
        else
        {
            AppSoundManager.Instance.audioMixer.SetFloat("SFX", -64);
            AppSoundManager.Instance.audioMixer.SetFloat("BGM", -64);
            PlayerPrefs.SetString("SoundPlay", "false");

        }

    }
    public void SettingToggle_Tutorial()
    {
        string tutorial = PlayerPrefs.GetString("TutorialPlay");
        Debug.Log("Tutorial :" + PlayerPrefs.GetString("TutorialPlay"));
        if (setting.TutorialToggle.isOn)
        {
            PlayerPrefs.SetString("TutorialPlay", "true");
        }
        else
        {
            PlayerPrefs.SetString("TutorialPlay", "false");
        }
    }
}
