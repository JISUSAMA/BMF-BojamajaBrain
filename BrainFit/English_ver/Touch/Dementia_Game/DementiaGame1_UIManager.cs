using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DementiaGame1_UIManager : MonoBehaviour
{
    [Header("질문")]
    public Text[] Word_Text;
    public GameObject[] Word_Blank;

    public GameObject BlankAnswer_ob;

    public Text[] CheckAnswer_text;
    public Button[] AnswerBtn;
    public GameObject[] AnswerCheckMark;
    Image checkColor;
    public Sprite[] AnswerCheck_sp;
    string Answer_text;

    public GameObject[] QuizNum_Grup; //문제의 종류 5가지
    public GameObject[] Quiz1_Blank_ob; //애호박 문제 4가지  
    public GameObject[] Quiz2_Blank_ob; //감자 문제 4가지
    public GameObject[] Quiz3_Blank_ob; //버섯 문제 4가지
    public GameObject[] Quiz4_Blank_ob; //호박 문제 4가지
    public GameObject[] Quiz5_Blank_ob; //토마토 문제 4가지

    public GameObject Blind_ob;

    public void SubmitAnswer()
    {
        StopCoroutine("_SubmitAnswer");
        StartCoroutine("_SubmitAnswer");
    }
    IEnumerator _SubmitAnswer()
    {
        for (int i = 0; i < Word_Blank.Length; i++)
        {
            Word_Blank[i].SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);

        yield return null;
    }

    public void AnswerToggle_Btn(GameObject ob)
    {
        if (ob.name.Equals("Number 1")) { AnswerBtn_Action(CheckAnswer_text[0].text.ToString(), true, false, false, false); }
        else if (ob.name.Equals("Number 2")) { AnswerBtn_Action(CheckAnswer_text[1].text.ToString(), false, true, false, false); }
        else if (ob.name.Equals("Number 3")) { AnswerBtn_Action(CheckAnswer_text[2].text.ToString(), false, false, true, false); }
        else if (ob.name.Equals("Number 4")) { AnswerBtn_Action(CheckAnswer_text[3].text.ToString(), false, false, false, true); }
        string choose_name = ob.name;
        AnswerQusetion(choose_name);

    }
    public void AnswerBtn_Action(string answer, bool num1, bool num2, bool num3, bool num4)
    {
        AnswerCheckMark[0].SetActive(num1);
        AnswerCheckMark[1].SetActive(num2);
        AnswerCheckMark[2].SetActive(num3);
        AnswerCheckMark[3].SetActive(num4);
        Answer_text = answer;
        Debug.Log(Answer_text);
    }

    public void AnswerQusetion(string ch_name)
    {
        if (ch_name.Equals("Number 1")) { checkColor = AnswerCheckMark[0].GetComponent<Image>(); }
        else if (ch_name.Equals("Number 2")) { checkColor = AnswerCheckMark[1].GetComponent<Image>(); }
        else if (ch_name.Equals("Number 3")) { checkColor = AnswerCheckMark[2].GetComponent<Image>(); }
        else if (ch_name.Equals("Number 4")) { checkColor = AnswerCheckMark[3].GetComponent<Image>(); }
        BlankAnswer_ob.SetActive(true);
        //맞춤
        if (Answer_text.Equals(DementiaGame1_DataManager.instance.thisQuizAnswer))
        {
            checkColor.sprite = AnswerCheck_sp[0];
            string changeColor_str = "#17B983";
            Color changeColor;
            if (ColorUtility.TryParseHtmlString(changeColor_str, out changeColor))
            {
                Debug.Log(BlankAnswer_ob.gameObject.GetComponent<TextMeshPro>());
                BlankAnswer_ob.GetComponent<TextMeshPro>().color = changeColor;
            }
            SubmitAnswer();
            DementiaGame1_DataManager.instance.TimerManager_sc.Question_Success();
        }
        //틀림
        else
        {
            checkColor.sprite = AnswerCheck_sp[1];
            string changeColor_str = "#FD6C6C";
            Color changeColor;
            if (ColorUtility.TryParseHtmlString(changeColor_str, out changeColor))
            {
                Debug.Log(BlankAnswer_ob.gameObject.GetComponent<TextMeshPro>());
                BlankAnswer_ob.GetComponent<TextMeshPro>().color = changeColor;
            }
            SubmitAnswer();
            DementiaGame1_DataManager.instance.TimerManager_sc.Question_Fail();
        }

    }
}
