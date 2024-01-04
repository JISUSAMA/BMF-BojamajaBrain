using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    // userID : ca-app-pub-9800709682711040~9695622077
    // android : ca-app-pub-9800709682711040/5249944750    
    public InterstitialAd interstitial;

    public static AdsManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        RequestInterstitial();
    }
    //전면 광고 로드
    private void RequestInterstitial()
    {
        //플랫폼에 따라 각기 다른 광고 단위 사용함
#if UNITY_ANDROID
        //string adUnitId = "ca-app-pub-9800709682711040/5249944750";
        string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //sample
#elif UNITY_IPHONE
      string adUnitId = ""; //iPhone id
       // string adUnitId = "ca-app-pub-3940256099942544/4411468910"; //sample
#else
string adUnitId = "unexpected_platform";
#endif
        //객체 초기화
        this.interstitial = new InterstitialAd(adUnitId);

        AddEvent();

        //빈 광고 요청을 만들어준다.
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
    //광고 요청이 성공적으로 로드되면 호출됩니다.
    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
       print("HandleAdLoaded event received");
    }
    //광고 요청을 로드하지 못한 경우 호출됩니다.
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        print("Interstitial failed to load: " + e.ToString());
    }
    //광고가 닫히면 호출됩니다.
    private void HandleOnAdClosed(object sender, EventArgs e)
    {
        RequestInterstitial();
        print("HandleAdOpening event received");
    }

    //광고가 표시되면 호출됩니다.
    private void HandleOnAdOpening(object sender, EventArgs e)
    {
        print("HandleAdClosed event received");
    }

    //전면 광고 게재
    public void Show_AD_interstitial()
    {
        if (this.interstitial.CanShowAd())
        {
            
            this.interstitial.Show();
        }
        else
        {
            Debug.Log("NOT Loaded Interstitial");
            RequestInterstitial();
        }
    }

    void AddEvent()
    {
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdOpening += HandleOnAdOpening;
        this.interstitial.OnAdClosed += HandleOnAdClosed;
    }

    void RemoveEvent()
    {
        this.interstitial.OnAdLoaded -= HandleOnAdLoaded;
        this.interstitial.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        this.interstitial.OnAdOpening -= HandleOnAdOpening;
        this.interstitial.OnAdClosed -= HandleOnAdClosed;
    }
}
