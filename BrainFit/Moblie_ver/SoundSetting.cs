using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public Slider Volum_Slider;
    public Toggle Volum_Mute_toggle;


    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("Volum_Value"))
        {
            PlayerPrefs.SetFloat("Volum_Value", 0);
        }
        else
        {
            float volum = PlayerPrefs.GetFloat("Volum_Value");
            Volum_Slider.value = volum;
            AppSoundManager.Instance.AudioMixer_Volume("BGM", volum);
            AppSoundManager.Instance.AudioMixer_Volume("SFX", volum);
            AppSoundManager.Instance.AudioMixer_Volume("NARRA", volum);
        }
       

       
    }
    public void Toggle_Mute_Check()
    {
        float volum;
        if (Volum_Mute_toggle.isOn.Equals(false)) 
        {
            volum = -80;
        }
        else
        {
            volum = Volum_Slider.value;
        }
        AppSoundManager.Instance.AudioMixer_Volume("BGM", volum);
        AppSoundManager.Instance.AudioMixer_Volume("SFX", volum);
        AppSoundManager.Instance.AudioMixer_Volume("NARRA", volum);
        PlayerPrefs.SetFloat("Volum_Value", volum);
    }
    public void Volum_Ctrl()
    {
        float volum = Volum_Slider.value;
        AppSoundManager.Instance.AudioMixer_Volume("BGM", volum);
        AppSoundManager.Instance.AudioMixer_Volume("SFX", volum);
        AppSoundManager.Instance.AudioMixer_Volume("NARRA", volum);
        PlayerPrefs.SetFloat("Volum_Value", volum);
    }
}
