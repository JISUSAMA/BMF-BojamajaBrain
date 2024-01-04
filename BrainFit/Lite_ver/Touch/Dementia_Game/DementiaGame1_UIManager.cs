using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DementiaGame1_UIManager : MonoBehaviour
{
    [Header("질문")]
    public Text[] Word_Text; 
    public GameObject[] Word_Blank;

    public Text[] CheckAnswer_text;
    public Button[] AnswerBtn;
    public GameObject[] AnswerCheckMark;
    Image checkColor;
    public Sprite[] AnswerCheck_sp;
    int AnswerNum_i;



    public void SubmitAnswer()
    {
        StopCoroutine("_SubmitAnswer");
        StartCoroutine("_SubmitAnswer");
    }
    IEnumerator _SubmitAnswer()
    {
            for (int i = 0; i <Word_Blank.Length; i++)
            {
               Word_Blank[i].SetActive(false);
            }
            yield return new WaitForSeconds(1.5f);
     
        yield return null;
    }

    public void AnswerToggle_Btn(GameObject ob)
    {
        if (ob.name.Equals("Number 1")) { AnswerBtn_Action(1,true, false, false, false); }
        else if (ob.name.Equals("Number 2")) { AnswerBtn_Action(2,false, true, false, false); }
        else if (ob.name.Equals("Number 3")) { AnswerBtn_Action(3,false, false, true, false); }
        else if (ob.name.Equals("Number 4")) { AnswerBtn_Action(4,false, false, false, true); }
        string choose_name = ob.name;
        AnswerQusetion(choose_name);

    }
    public void AnswerBtn_Action(int num, bool num1, bool num2, bool num3, bool num4)
    {
        AnswerCheckMark[0].SetActive(num1);
        AnswerCheckMark[1].SetActive(num2);
        AnswerCheckMark[2].SetActive(num3);
        AnswerCheckMark[3].SetActive(num4);
        AnswerNum_i = num;
    }
  
    public void AnswerQusetion(string ch_name)
    {
      
        if(ch_name.Equals("Number 1")) { checkColor = AnswerCheckMark[0].GetComponent<Image>(); }
        else if(ch_name.Equals("Number 2")) { checkColor = AnswerCheckMark[1].GetComponent<Image>(); }
        else if(ch_name.Equals("Number 3")) { checkColor = AnswerCheckMark[2].GetComponent<Image>(); }
        else if(ch_name.Equals("Number 4")) { checkColor = AnswerCheckMark[3].GetComponent<Image>(); }
        //맞춤
        if (AnswerNum_i.Equals(DementiaGame1_DataManager.instance.SubmitAnswerNum_i))
        {
            checkColor.sprite = AnswerCheck_sp[0];
            string changeColor_str = "#17B983"; 
            Color changeColor;
            if (ColorUtility.TryParseHtmlString(changeColor_str, out changeColor))
            {
                Word_Text[DementiaGame1_DataManager.instance.blank1].color = changeColor;
                Word_Text[DementiaGame1_DataManager.instance.blank2].color = changeColor;
            }
            SubmitAnswer();
            SetUIGrup.instance.Question_Success();
        }
        //틀림
        else
        {
            SceneSoundCtrl.Instance.GameFailSound();
            //checkColor.sprite = AnswerCheck_sp[1];
            //string changeColor_str = "#FD6C6C"; 
            //Color changeColor;
            //if (ColorUtility.TryParseHtmlString(changeColor_str, out changeColor))
            //{
            //    Word_Text[DementiaGame1_DataManager.instance.blank1].color = changeColor;
            //    Word_Text[DementiaGame1_DataManager.instance.blank2].color = changeColor;
            //}
            //SubmitAnswer();
            //SetUIGrup.instance.Question_Fail();
        }
      
    }
}
