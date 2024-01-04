using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class VoiceSound : MonoBehaviour
{
    public static VoiceSound instance { get; private set; }

    public AudioSource voiceAudioSource;

    public AudioClip[] voiceClip;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    public void VoiceAnnouncement(int _index)
    {
        voiceAudioSource.clip = voiceClip[_index];
        voiceAudioSource.Play();
    }

}
