using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class AppSoundManager : MonoBehaviour
{
    // public SceneSoundCtrl sceneSoundCtrl_sc;
    public static AppSoundManager Instance { get; private set; }
    public AudioMixer audioMixer;
    public AudioSource bgm;
    public AudioSource sfx;

    public AudioClip[] Bgm_audioClips;
    public AudioClip[] Sfx_audioClips;
    public AudioClip[] GymnasticClips;

    public Dictionary<string, AudioClip> bgm_lookupTable = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> sfx_lookupTable = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> gym_lookupTable = new Dictionary<string, AudioClip>();
    public float fadeSpeed = 80f;
    //public string currentPlayClipName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //BGM
        foreach (AudioClip clip in Bgm_audioClips)
        {
            bgm_lookupTable.Add(clip.name, clip);
        }
        //SFX
        foreach (AudioClip clip in Sfx_audioClips)
        {
            sfx_lookupTable.Add(clip.name, clip);
         
        }
        foreach (AudioClip clip in GymnasticClips)
        {
            gym_lookupTable.Add(clip.name, clip);
        }

        if (PlayerPrefs.HasKey("SoundPlay"))
        {
            string sound = PlayerPrefs.GetString("SoundPlay");
            float volum = PlayerPrefs.GetFloat("SoundValue");
            if (sound.Equals("true"))
            {
               
                audioMixer.SetFloat("SFX", volum);
                audioMixer.SetFloat("BGM", volum);
            }
            else
            {
                audioMixer.SetFloat("SFX", -60f);
                audioMixer.SetFloat("BGM", -60f);
            }
        }

    }

    public void PlayBGM(string soundName)
    {
        //Debug.LogError("soundName    " + soundName);
        //Debug.LogError("bgm.isPlaying    " + bgm.isPlaying);
        //현재 재생 중인 사운드와 일치

        if (bgm.isPlaying && bgm.clip.name.Equals(soundName))
            return;
        else
        {
            //Debug.LogError("게임 종료후 다시 !/???");
            bgm.clip = Bgm_GetClipByName(soundName);
            bgm.Play();
            StopCoroutine(_Bmg_FadeIn());
            StopCoroutine(_Bmg_FadeOut());
            StartCoroutine(_Bmg_FadeIn());
        }

    }
    public void StopBGM()
    {
        StopCoroutine(_Bmg_FadeOut());
        StopCoroutine(_Bmg_FadeIn());
        StartCoroutine(_Bmg_FadeOut());
    }
    IEnumerator _Bmg_FadeIn()
    {
        float vol = 0f;
        audioMixer.SetFloat("BGM", vol);
        //Debug.LogError("BGMPlaySound" + vol);
        while (vol < 0f)
        {
            vol += Time.deltaTime * fadeSpeed;
            audioMixer.SetFloat("BGM", vol);
            yield return new WaitForEndOfFrame();
        }
        audioMixer.SetFloat("BGM", 0f);
    }

    IEnumerator _Bmg_FadeOut()
    {
        float vol = 0f;

        audioMixer.SetFloat("BGM", vol);

        while (vol > -80f)
        {
            vol -= Time.deltaTime * fadeSpeed;
            audioMixer.SetFloat("BGM", vol);
            yield return new WaitForEndOfFrame();
        }
        audioMixer.SetFloat("BGM", -80f);
        bgm.Stop();
    }
    public AudioClip Bgm_GetClipByName(string clipName)
    {
        return bgm_lookupTable[clipName];
    }
    public void PlayBGM(string clipName, float volume = 1f)
    {
        bgm.PlayOneShot(Bgm_GetClipByName(clipName), volume);
    }
    public AudioClip Sfx_GetClipByName(string clipName)
    {
        return sfx_lookupTable[clipName];
    }
    public void PlaySFX(string clipName, float volume = 1f)
    {
        //Debug.Log("PlaySFX Sound name : " + clipName);
        sfx.PlayOneShot(Sfx_GetClipByName(clipName), volume);
    }
    public void PlayGYM(string clipName, float volume = 1f)
    {
        Debug.Log("Clip Name :" + clipName);
        sfx.PlayOneShot(Gym_lookupTable(clipName), volume);
    }
    public AudioClip Gym_lookupTable(string clipName)
    {
        return gym_lookupTable[clipName];
    }
    public void AudioMixer_Volume(string mixer, float volume)
    {
        audioMixer.SetFloat(mixer, volume);
    }
    //public AudioSource bgmSource;
    //public AudioSource sfxSource;


    //public AudioClip bgmMain_Clip;

    //public AudioClip click_Clip;
    //public AudioClip shoot_Clip;
    //public AudioClip touch_Clip;
    //public AudioClip keyboard_Clip;

    //private void Awake()
    //{
    //    if (instance != null)
    //        Destroy(this.gameObject);
    //    else instance = this;

    //    DontDestroyOnLoad(this.gameObject);
    //}


    //void Start()
    //{
    //    MainBGM_Sound();
    //}

    //public void MainBGM_Sound()
    //{
    //    bgmSource.clip = bgmMain_Clip;
    //    bgmSource.Play();
    //}

    //public void MainBGM_SoundPuase()
    //{
    //    bgmSource.clip = bgmMain_Clip;
    //    bgmSource.Pause();
    //}

    //public void ClickSound()
    //{
    //    sfxSource.clip = click_Clip;
    //    sfxSource.Play();
    //}

    //public void ShootSound()
    //{
    //    sfxSource.clip = shoot_Clip;
    //    sfxSource.Play();
    //}

    //public void WindowTouchSound()
    //{
    //    sfxSource.clip = touch_Clip;
    //    sfxSource.Play();
    //}

    //public void KeyboardTouchSound()
    //{
    //    sfxSource.clip = keyboard_Clip;
    //    sfxSource.Play();
    //}
}
