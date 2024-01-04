using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetUIGrup : MonoBehaviour
{
    public Button ExitBtn;
    public Text AnswerTime_text;
    public Slider AnswerTimeSlider;

    public Text QuestionNum_text;
    public Slider QuestionNum_Slider;

    [Header("Question Clear / Fail Check")]
    public GameObject QuestionState;
    public Image State_img;
    public Sprite[] state_sp; //0 :o 1:X 
    public bool Check = false;


    [Header("InGame")]
    public GameObject GameExit_PopupOb;
    public Button GameExitButton; //인게임

    [Header("CountPanel")]
    public GameObject CountDownPopup;
    public Image CountImg_bg;
    public Sprite[] CountImg_sp;
    public Text description_text;


    private void Awake()
    {
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
        if (GameAppManager.instance.GameKind == "Brain" || GameAppManager.instance.GameKind == "Dementia")
        {
            CountDownPopup.SetActive(true);
            for (int i = 0; i < CountImg_sp.Length - 1; i++)
            {
                CountImg_bg.sprite = CountImg_sp[4 - i];
                yield return new WaitForSeconds(1);
            }
            CountDownPopup.SetActive(false);
        }
        GameAppManager.instance.playBool = true;
    }
}
