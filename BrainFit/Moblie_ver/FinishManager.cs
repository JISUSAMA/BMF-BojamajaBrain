using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class FinishManager : MonoBehaviour
{
    //public Image Basic_Panel_img;
    public Animator Clear_panel_ani;
    public Sprite[] Basic_Panel_sp;
    public GameObject[] Match_particle;

    public Sprite[] Checks_Brain_img; //실패 성공 이미지 변경 스프라이트
    public Sprite[] Checks_Dementia_img; //실패 성공 이미지 변경 스프라이트
    public Sprite[] Checks_Real_img; //실패 성공 이미지 변경 스프라이트
    public Button HomeButton;

    public GameObject[] KindGrup_ob; //touch /Leap /Kinect 에 따른 점수판
    [Header("Touch_Brain")]
    public Image[] BrainGrup_img; //게임 종류 base_back 이미지 
    public Text[] Brain_GameTitle_text; //게임의 이름
    public Text[] Brain_GameScore_text; //게임의 점수 
    public Image[] Brain_GameMedal;

    [Header("Touch_Dementia")]
    public Image[] DementiaGrup_img; //게임 종류 base_back 이미지 
    public Text[] Dementia_GameTitle_text; //게임의 이름
    public Text[] Dementia_GameScore_text; //게임의 점수 
    public Image[] Dementia_GameMedal;

    public Sprite[] Medal_sp;

    string nowTime_Str;
    private void Awake()
    {
        if (GameAppManager.instance.GameKind.Equals("Brain"))
            HomeButton.onClick.AddListener(() => OnClick_Touch_BackHome());
        else if (GameAppManager.instance.GameKind.Equals("Dementia"))
            HomeButton.onClick.AddListener(() => OnClick_Touch_BackHome());
 
    }
    private void Start()
    {
        AdsManager.instance.Show_AD_interstitial();
        switch (GameAppManager.instance.GameKind)
        {
            case "Brain":
                KindGrup_ob[0].SetActive(true);
                Match_particle[0].SetActive(true);
                Clear_panel_ani.SetBool("Yellow", true);
                for (int i = 0; i < 7; i++)
                {
                    Brain_GameTitle_text[i].text = GameAppManager.instance.BrainName[GameAppManager.instance.rand_GameNumber[i]]; //게임 한글명
                    if (!GameAppManager.instance.GamePlayMedal[i].Equals("timeOver"))
                    {
                        if (GameAppManager.instance.GamePlayMedal[i].Equals("gold")){ Brain_GameMedal[i].sprite = Medal_sp[0]; }
                        else if (GameAppManager.instance.GamePlayMedal[i].Equals("silver")){ Brain_GameMedal[i].sprite = Medal_sp[1]; }
                        else if (GameAppManager.instance.GamePlayMedal[i].Equals("bronze")){ Brain_GameMedal[i].sprite = Medal_sp[2]; }
                        else if (GameAppManager.instance.GamePlayMedal[i].Equals("wooden")){ Brain_GameMedal[i].sprite = Medal_sp[3]; }
                    }
                    else
                    {
                        BrainGrup_img[i].sprite = Checks_Brain_img[1]; //실패 판넬로 변경
                        Brain_GameMedal[i].sprite = Medal_sp[4];
                    }
                }
                break;
            case "Dementia":
                KindGrup_ob[1].SetActive(true);
                Match_particle[1].SetActive(true);
                Clear_panel_ani.SetBool("Green", true);
                //Basic_Panel_img.sprite = Basic_Panel_sp[1]; //그린
                //  Match_particle[1].SetActive(true);//그린
                for (int i = 0; i < 8; i++)
                {
                    Dementia_GameTitle_text[i].text = GameAppManager.instance.DementiaName[GameAppManager.instance.rand_GameNumber[i]]; //게임 한글명
                    if (!GameAppManager.instance.GamePlayMedal[i].Equals("timeOver"))
                    {
                        if (GameAppManager.instance.GamePlayMedal[i].Equals("gold")) { Dementia_GameMedal[i].sprite = Medal_sp[0]; }
                        else if (GameAppManager.instance.GamePlayMedal[i].Equals("silver")) { Dementia_GameMedal[i].sprite = Medal_sp[1]; }
                        else if (GameAppManager.instance.GamePlayMedal[i].Equals("bronze")) { Dementia_GameMedal[i].sprite = Medal_sp[2]; }
                        else if (GameAppManager.instance.GamePlayMedal[i].Equals("wooden")) { Dementia_GameMedal[i].sprite = Medal_sp[3]; }
                    }
                    else
                    {
                        DementiaGrup_img[i].sprite = Checks_Dementia_img[1]; //실패 판넬로 변경
                        Dementia_GameMedal[i].sprite = Medal_sp[4];
                    }
                }
                break;
        }
    }
 
    public void OnClick_Touch_BackHome()
    {
        GameAppManager.instance.play_touchGame = true;
        SceneManager.LoadScene("8.ChooseGame");
    }
    public void OnClick_Leap_BackHome()
    {
        GameAppManager.instance.play_touchGame = false;
        SceneManager.LoadScene("8.ChooseGame");
    }

    public void Data_WriteToDay()
    {
        DateTime.Now.ToString("yyyy-MM-dd");
    }
}
