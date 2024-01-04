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
        float volum_prefeb = PlayerPrefs.GetFloat("Volum_Value");
        if (!PlayerPrefs.HasKey("Volum_Value"))
        {
            PlayerPrefs.SetFloat("Volum_Value", 1);
        }
        else
        {
            if (volum_prefeb.Equals(-80))
            {
                Volum_Mute_toggle.isOn = false;
            }
            else
            {
                Volum_Mute_toggle.isOn = true;
            }
            Volum_Slider.value = volum_prefeb;
            AppSoundManager.Instance.AudioMixer_Volume("BGM", volum_prefeb);
            AppSoundManager.Instance.AudioMixer_Volume("SFX", volum_prefeb);
            AppSoundManager.Instance.AudioMixer_Volume("NARRA", volum_prefeb);
        }

    }
    public void Toggle_Mute_Check()
    {
        float volum;
        if (Volum_Mute_toggle.isOn.Equals(false))
        {
            volum = -40;
            Volum_Slider.value = volum;
        }
        else
        {
            volum = 1;
            Volum_Slider.value = volum;
        }

        AppSoundManager.Instance.AudioMixer_Volume("BGM", volum);
        AppSoundManager.Instance.AudioMixer_Volume("SFX", volum);
        AppSoundManager.Instance.AudioMixer_Volume("NARRA", volum);
        PlayerPrefs.SetFloat("Volum_Value", volum);
    }
    public void Volum_Ctrl()
    {
        float volum = Volum_Slider.value;
        if (Volum_Slider.value.Equals(-40))
        {
            Volum_Mute_toggle.isOn = false;
        }

        AppSoundManager.Instance.AudioMixer_Volume("BGM", volum);
        AppSoundManager.Instance.AudioMixer_Volume("SFX", volum);
        AppSoundManager.Instance.AudioMixer_Volume("NARRA", volum);
        PlayerPrefs.SetFloat("Volum_Value", volum);
    }
}
