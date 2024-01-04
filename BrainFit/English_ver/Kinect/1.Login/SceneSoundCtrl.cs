using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSoundCtrl : MonoBehaviour
{
    public static SceneSoundCtrl Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else Instance = this;

    }
    public void ButtonClick_sound()
    {
        AppSoundManager.Instance.PlaySFX("ClickSound");
    }
    public void Keyboard_ClickSound()
    {
        AppSoundManager.Instance.PlaySFX("Keyboard");
    }
    public void ShootSound()
    {
        AppSoundManager.Instance.PlaySFX("picture");
    }
    public void GameSuccessSound()
    {
        AppSoundManager.Instance.PlaySFX("026_¶ìµù2");
    }
    public void GameFailSound()
    {
        AppSoundManager.Instance.PlaySFX("003_¶ì¿ä¿ë(¹Ù¿î½º)");
    }
}
