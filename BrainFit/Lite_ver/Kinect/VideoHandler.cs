using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    public static VideoHandler instance { get; private set; }

    public RawImage mScrenn;
    public RenderTexture mVideoRawImage;
    public VideoPlayer mVideoPlayer;
    public VideoClip[] mVideoClip;  //비디오 칩
    //public Slider slider;

    bool isDone;    //비디오플레이어 속성


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    private void Start()
    {
    }



    public bool IsPlaying
    {
        get { return mVideoPlayer.isPlaying; }
    }

    public bool IsLooping
    {
        get { return mVideoPlayer.isLooping; }
    }

    public bool IsPrepared
    {
        get { return mVideoPlayer.isPrepared; }
    }

    public bool IsDone
    {
        get { return isDone; }
    }

    public double Times
    {
        get { return mVideoPlayer.time; }
    }

    public ulong Duration
    {
        get { return (ulong)(mVideoPlayer.frameCount / mVideoPlayer.frameRate); }
    }

    public double NTime
    {
        get { return Times / Duration; }
    }


    private void OnEnable()
    {
        mVideoPlayer.errorReceived += errorReceived;
        mVideoPlayer.frameReady += frameReady;
        mVideoPlayer.loopPointReached += loopPointReached;
        mVideoPlayer.prepareCompleted += prepareCompleted;
        mVideoPlayer.seekCompleted += seekCompleted;
        mVideoPlayer.started += started;
    }

    private void OnDisable()
    {
        mVideoPlayer.errorReceived -= errorReceived;
        mVideoPlayer.frameReady -= frameReady;
        mVideoPlayer.loopPointReached -= loopPointReached;
        mVideoPlayer.prepareCompleted -= prepareCompleted;
        mVideoPlayer.seekCompleted -= seekCompleted;
        mVideoPlayer.started -= started;
    }

    void errorReceived(VideoPlayer v, string msg)
    {
        Debug.Log("video player error : " + msg);
    }

    void frameReady(VideoPlayer v, long frame)
    {

    }

    void loopPointReached(VideoPlayer v)
    {
        //Debug.Log("video player loop point reached");
        isDone = true;
    }

    void prepareCompleted(VideoPlayer v)
    {
        //Debug.Log("video player loop point preparing");
        isDone = false;
    }

    void seekCompleted(VideoPlayer v)
    {
        //Debug.Log("video player loop point seking");
        isDone = false;
    }

    void started(VideoPlayer v)
    {
        //Debug.Log("video player loop point started");
    }


    void Update()
    {
        //if (!IsPrepared) return;
        //slider.value = (float)NTime;

    }

    public void LoadVideo(string _folderName, int _videoIndex)
    {
        string temp = "";
        //string temp = Application.dataPath + "/Video/" + name;/*.mp4, .avi, .mov*/
        //if (_folderName.Equals("율동운동"))
        //    temp = Application.dataPath + "/Video/" + _folderName + "/" + _week + "/" + _videoIndex.ToString() + ".mp4";/*.mp4, .avi, .mov*/
        //else
        //    temp = Application.dataPath + "/Video/" + _folderName + "/" +  _videoIndex.ToString() + ".mp4";/*.mp4, .avi, .mov*/

        temp = Application.persistentDataPath + "/Video/" + _folderName + "/" + _videoIndex.ToString() + ".mp4";

        if (mVideoPlayer.url.Equals(temp))
            return;

        mVideoPlayer.url = temp;
        //mVideoPlayer.Prepare();
        mVideoPlayer.Play();

        
        Debug.Log("can set direct audio volume: " + mVideoPlayer.canSetDirectAudioVolume);
    }

    public void LoadNullVideo()
    {
        mVideoPlayer.clip = null;
    }

    public void PlayVideo()
    {
        mVideoRawImage.Release();
        if (!IsPrepared)
            return;
        mVideoPlayer.Play();
        Time.timeScale = 1; //재생
    }

    public void PauseVideo()
    {
        if (!IsPlaying)
            return;
        mVideoPlayer.Pause();
        Time.timeScale = 0; //일시정지
    }

    public void StopVideo()
    {
        if (!IsPlaying)
            return;
        mVideoPlayer.Stop();
    }

    public void RestartVideo()
    {
        if (!IsPrepared)
            return;
        PauseVideo();
        Seek(0);
    }

    public void LoopViedo(bool toggle)
    {
        if (!IsPrepared)
            return;
        mVideoPlayer.isLooping = toggle;
    }

    public void Seek(float nTime)
    {
        if (!mVideoPlayer.canSetTime) return;
        if (!IsPrepared) return;
        nTime = Mathf.Clamp(nTime, 0, 1);
        mVideoPlayer.time = nTime * Duration;

        //Debug.Log(mVideoPlayer.time  +"  "+ Duration);
    }

    public void IncrementPlaybackSpeed()
    {
        if (!mVideoPlayer.canSetPlaybackSpeed) return;
        mVideoPlayer.playbackSpeed += 1;
        mVideoPlayer.playbackSpeed = Mathf.Clamp(mVideoPlayer.playbackSpeed, 0, 10);
    }

    public void DecrementPlaybackSpeed()
    {
        if (!mVideoPlayer.canSetPlaybackSpeed) return;
        mVideoPlayer.playbackSpeed += 1;
        mVideoPlayer.playbackSpeed = Mathf.Clamp(mVideoPlayer.playbackSpeed, 0, 10);
    }


    public void ExcerciseTime(Text _timer)
    {
        float seconds = (float)mVideoPlayer.time;

        TimeSpan timespan = TimeSpan.FromSeconds(seconds);
        string timer = string.Format("{0:00}:{1:00}", timespan.Minutes, timespan.Seconds);
        //Debug.Log(" timer  " + timer);
        _timer.text = timer;
    }



    

    //public void PlayVideo(int _index)
    //{
    //    mVideoRawImage.Release();
    //    mVideoPlayer.clip = mVideoClip[_index];
    //    mVideoPlayer.Play();
    //}

    //public void Stop_Video()
    //{
    //    mVideoPlayer.Stop();
    //}

    //public void Pause_Video()
    //{
    //    mVideoPlayer.Pause();
    //}

}
