using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BreakTimeUIManager : MonoBehaviour
{

    // public GameObject BackGround;
    //public Image StatePanel_img;
    //public Sprite[] StatePanel_sp; //성공 실패 배경 이미지 0:brain 1:Dementia 2:Real
    public GameObject[] Back_panel;

    public GameObject Fail_StempAni_ob;
    public Text GameTitle; //게임 타이틀

    public Image MarkImg;
    public Sprite[] MarkSprite_clear; //성공  이미지 0:brain 1:Dementia 2:Real 3:실패
    public Sprite[] MarkSprite_fail; // 실패 이미지 0:brain 1:Dementia 2:Real 3:실패

    public Text ClearTimeText;

 //   public GameObject Clear_particle;

    public GameObject GameExitPopup;
    public Button YesBTN;
    public Button NoBTN;

    //변하는 것 ; 배경색상, 훈련성공,실패                              

    [Header("Next Time")]
    public Image timeRotateImg; //타임 돌아가는 이미지
    public Sprite[] timeRotateSp; //타임 스프라이트
    public Text TimerText;
    bool isRotate;

    private void Awake()
    {
        YesBTN.onClick.AddListener(() => OnClick_ExitYes());
        NoBTN.onClick.AddListener(() => OnClick_ExitNo());
    }
    private void Start()
    {
        string state = PlayerPrefs.GetString("GameClearState");
        switch (GameAppManager.instance.GameKind)
        {
            case "Brain":
              
                timeRotateImg.sprite = timeRotateSp[0];
                if (state.Equals("true"))
                {
                    //StatePanel_img.sprite = StatePanel_sp[0]; //노란 배경
                    Back_panel[0].SetActive(true);
                    MarkImg.sprite = MarkSprite_clear[0];//brain성공
                    GameTitle.text
                        = GameAppManager.instance.BrainName[GameAppManager.instance.rand_GameNumber[GameAppManager.instance.CurrentQusetionNumber - 1]].ToString();
                    ClearTimeText.text = GameAppManager.instance.GamePlayTime[GameAppManager.instance.CurrentQusetionNumber - 1].ToString("N0") + "초";
                   // Clear_particle.SetActive(true); 
                }
                else if (state.Equals("false"))
                {
                    //StatePanel_img.sprite = StatePanel_sp[3]; //노란 배경
                    MarkImg.sprite = MarkSprite_fail[0];//brain실패
                   
                    GameTitle.text
                          = GameAppManager.instance.BrainName[GameAppManager.instance.rand_GameNumber[GameAppManager.instance.CurrentQusetionNumber - 1]].ToString();
                    ClearTimeText.text = "다음기회에..";
                    Fail_StempAni_ob.SetActive(true);
                }
                break;
            case "Dementia":
              
                timeRotateImg.sprite = timeRotateSp[1];
                if (state.Equals("true"))
                {
                    //StatePanel_img.sprite = StatePanel_sp[1]; //초록 배경
                    Back_panel[1].SetActive(true);
                    MarkImg.sprite = MarkSprite_clear[1];//brain성공
                    GameTitle.text
                        = GameAppManager.instance.DementiaName[GameAppManager.instance.rand_GameNumber[GameAppManager.instance.CurrentQusetionNumber - 1]].ToString();
                    ClearTimeText.text = GameAppManager.instance.GamePlayTime[GameAppManager.instance.CurrentQusetionNumber - 1].ToString("N0") + "초";
                   // Clear_particle.SetActive(true);
             
                }
                else if (state.Equals("false"))
                {
                  //  StatePanel_img.sprite = StatePanel_sp[3]; //노란 배경
                    MarkImg.sprite = MarkSprite_fail[1];//brain실패
                    GameTitle.text
                          = GameAppManager.instance.DementiaName[GameAppManager.instance.rand_GameNumber[GameAppManager.instance.CurrentQusetionNumber - 1]].ToString();
                    ClearTimeText.text = "다음기회에..";
                    Fail_StempAni_ob.SetActive(true);
                }
                break;
         
        }
        Waiting_SecTime();
    }
    void Update()
    {
        if (isRotate.Equals(true))
            timeRotateImg.transform.Rotate(new Vector3(0, 0, -0.5f));
    }

    public void OnClick_ExitBTN()
    {
        Time.timeScale = 0;
        GameExitPopup.SetActive(true);
    }
   public void OnClick_ExitYes()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("8.ChooseGame");
    }
    public void OnClick_ExitNo()
    {
        Time.timeScale = 1;
        GameExitPopup.SetActive(false);
    }

    public void Waiting_SecTime()
    {
        StartCoroutine("_Waiting_SecTime");
    }
    IEnumerator _Waiting_SecTime()
    {
        isRotate = true;
        yield return new WaitForSeconds(1);
        TimerText.text = "4";
        yield return new WaitForSeconds(1);
        TimerText.text = "3";
        yield return new WaitForSeconds(1);
        TimerText.text = "2";
        yield return new WaitForSeconds(1);
        TimerText.text = "1";
        GameAppManager.instance.GameLoadScene();
    }
}
