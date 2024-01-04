using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SetUIGrup : MonoBehaviour
{
    public Button ExitBtn;
    public Text AnswerTime_text;
    public Slider AnswerTimeSlider;

    public Text QuestionNum_text;
    public Slider QuestionNum_Slider;

    [Header("Question Clear / Fail Check")]
    public float DefaltAnswerT;
    public float AnswerTimer_f; //문제의 시간

    public Image State_img;
    public Sprite[] State_sp; //0 :o 1:X 
    public bool Check = false;

    public int Medal_level;
    public string Medal_level_string;

    [Header("타이머")]
    public Sprite[] AnswerImg_sprite;
    public Slider AnswerTimer_slider; //타이머
    public Text AnswerTimer_text;

    [Header("InGame")]
    public GameObject GameExit_PopupOb;
    public Button GameExitButton; //인게임

    [Header("CountPanel")]
    public GameObject CountDownPopup;
    public Image CountImg_bg;
    public Sprite[] CountImg_sp;
    public Text description_text;

    [Header("Tutorial")]
    public GameObject[] Tutorial_Panel;
    public bool Tutorial_state;
    public static SetUIGrup instance { get; private set; }
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;

        if (GameExitButton.gameObject.activeSelf.Equals(true))
            GameExitButton.onClick.AddListener(() => OnClick_GameExitButton());
    }
    private void Start()
    {
        StartCountDown(); //5 초 카운트 다운
    }
    public void OnClick_GameExitButton()
    {
        GameExit_PopupOb.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnClick_GameExitBtn_Yes()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("8.ChooseGame");
    }
    public void OnClick_GameExitBtn_No()
    {
        Time.timeScale = 1;
        GameExit_PopupOb.SetActive(false);
    }
    public void StartCountDown()
    {
        StartCoroutine(_StartCountDown());
    }
    IEnumerator _StartCountDown()
    {
        StartTutorial();
        yield return new WaitUntil(() => Tutorial_state == false);
        //if (GameAppManager.instance.GameKind == "Brain" || GameAppManager.instance.GameKind == "Dementia")
        //{
        //    CountDownPopup.SetActive(true);
        //    for (int i = 0; i < CountImg_sp.Length - 1; i++)
        //    {
        //        CountImg_bg.sprite = CountImg_sp[4 - i];
        //        yield return new WaitForSeconds(1);
        //    }
        //    CountDownPopup.SetActive(false);
        //}
        GameAppManager.instance.playBool = true;
    }
    void StartTutorial()
    {
        if (PlayerPrefs.GetString("TutorialPlay").Equals("true"))
        {
            Tutorial_Panel[0].gameObject.transform.parent.gameObject.SetActive(true);
            Tutorial_state = true;
            Tutorial_Panel[0].SetActive(true);
        }
        else
        {
            Tutorial_state = false;
        }
    }
    public void OnClick_tutorial_next(GameObject next)
    {
        SceneSoundCtrl.Instance.ButtonClick_sound();
        if (EventSystem.current.currentSelectedGameObject.name.Equals("NextBtn"))
        {
            EventSystem.current.currentSelectedGameObject.SetActive(false); //현재 오브젝트는 비활성화 
            next.gameObject.SetActive(true);  //다음 튜토리얼 활성화 
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("GameStart"))
        {
            OnClick_tutorial_finish();
        }
    }
    public void TimeToScore(float timer)
    {
        //등급 종류 금, 은, 동, 나무, 실패
        GameAppManager.instance.playBool = true; //게임 시작
        //현재 문제의 번호 UI Setting
        if (GameAppManager.instance.GameKind.Equals("Brain"))
        {
            QuestionNum_text.text = (GameAppManager.instance.CurrentQusetionNumber + 1).ToString() + "/7";
            QuestionNum_Slider.value = GameAppManager.instance.CurrentQusetionNumber + 1;
        }
        else if (GameAppManager.instance.GameKind.Equals("Dementia"))
        {
            QuestionNum_text.text = (GameAppManager.instance.CurrentQusetionNumber + 1).ToString() + "/8";
            QuestionNum_Slider.value = GameAppManager.instance.CurrentQusetionNumber + 1;
        }

        DefaltAnswerT = timer; //주어신 시간
        StopCoroutine(_TimeToScore());
        StartCoroutine(_TimeToScore());
    }
    IEnumerator _TimeToScore()
    {
        AnswerTimer_f = DefaltAnswerT; //주어진 변하는 시간
        AnswerTimer_slider.maxValue = AnswerTimer_f;
        AnswerTimer_slider.value = AnswerTimer_f;

        while (GameAppManager.instance.playBool)
        {
            yield return new WaitForSeconds(0.5f);
            while (AnswerTimer_f > 0)
            {
                AnswerTimer_f -= Time.deltaTime;
                AnswerTimer_slider.value = AnswerTimer_f;
                AnswerTimer_text.text = AnswerTimer_f.ToString("N0");
                if (!GameAppManager.instance.playBool) break;
                yield return null;
            }
            if (AnswerTimer_f <= 0)
            {
                AnswerTimer_f = 0;
                AnswerTimer_text.text = " ";
                Medal_level = 4;
                if (SceneManager.GetActiveScene().Equals("DementiaGame8"))
                {
                   DementiaGame8_DataManager.instance.UIManager.Game_changeColor("fail");
                }
                Question_Fail();
            }
        }
        yield return null;
    }

    public void OnClick_tutorial_finish()
    {
        SceneSoundCtrl.Instance.ButtonClick_sound();
        for (int i = 0; i < Tutorial_Panel.Length; i++)
        {
            Tutorial_Panel[i].SetActive(false);
        }
        Tutorial_Panel[0].gameObject.transform.parent.gameObject.SetActive(false);
        Tutorial_state = false;
    }

    public void Question_Success()
    {
        StopCoroutine("_Question_Success");
        StartCoroutine("_Question_Success");
    }
    IEnumerator _Question_Success()
    {
        Debug.Log("DefaltAnswerT " + DefaltAnswerT + " AnswerTimer_f " + AnswerTimer_f);
        if (AnswerTimer_f >= (DefaltAnswerT / 4) * 3)
        {
            //금메달
            Medal_level = 0;
            Medal_level_string = "gold";
        }
        else if (AnswerTimer_f >= (DefaltAnswerT / 4) * 2 && AnswerTimer_f < (DefaltAnswerT / 4) * 3)
        {
            //은메달
            Medal_level = 1;
            Medal_level_string = "silver";
        }
        else if (AnswerTimer_f >= (DefaltAnswerT / 4) * 1 && AnswerTimer_f < (DefaltAnswerT / 4) * 2)
        {
            //동메달
            Medal_level = 2;
            Medal_level_string = "bronze";
        }
        else if (AnswerTimer_f >= 0 && AnswerTimer_f < (DefaltAnswerT / 4))
        {
            //나무메달
            Medal_level = 3;
            Medal_level_string = "wooden";
        }
        Time.timeScale = 0;
        SceneSoundCtrl.Instance.GameSuccessSound();
        PlayerPrefs.SetString("GameClearState", "true");
        GameAppManager.instance.GamePlayTime.Add(DefaltAnswerT - AnswerTimer_f);
        GameAppManager.instance.GamePlayMedal.Add(Medal_level_string);

        Debug.Log("Medal_level"+ Medal_level);
        State_img.gameObject.SetActive(true);
        State_img.sprite = State_sp[Medal_level];
        GameAppManager.instance.playBool = false;
        Time.timeScale = 1;
        yield return new WaitForSeconds(1.5f);
        State_img.gameObject.SetActive(true);
        GameAppManager.instance.CurrentQusetionNumber += 1;
        GameAppManager.instance.GameLoadScene();
        yield return null;
    }
    public void Question_Fail()
    {
        StopCoroutine("_Question_Fail");
        StartCoroutine("_Question_Fail");
    }
    IEnumerator _Question_Fail()
    {
        SceneSoundCtrl.Instance.GameFailSound();
        PlayerPrefs.SetString("GameClearState", "false");
        GameAppManager.instance.GamePlayTime.Add(AnswerTimer_f);
        GameAppManager.instance.GamePlayMedal.Add("timeOver");
        State_img.gameObject.SetActive(true);
        State_img.sprite = State_sp[4];
        GameAppManager.instance.playBool = false;
        yield return new WaitForSeconds(1.5f);
        State_img.gameObject.SetActive(false);
        GameAppManager.instance.CurrentQusetionNumber += 1;
        GameAppManager.instance.GameLoadScene();
        yield return null;
    }

}
