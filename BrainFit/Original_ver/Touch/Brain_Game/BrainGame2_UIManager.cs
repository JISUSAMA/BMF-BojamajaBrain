using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame2_UIManager : MonoBehaviour
{
    public Image MenuIMG;
    public Sprite[] MenuSprite; 
    public Text Problem_text;
    public Text Total_PriceText; //계산 결과창 
    public Text AnswerNum;

    public string AnswerValue;

    public void SetTextDataText()
    {
        // for (int i = 0; i < MenuNameText.Length; i++)
        // {
        //     MenuNameText[i].text = BrainGame2_DataManager.instance.menu[i];
        //     MenuPriceText[i].text = BrainGame2_DataManager.instance.menu_price[i].ToString();
        // }
        MenuIMG.sprite = MenuSprite[BrainGame2_DataManager.instance.Problem_number]; 
        BrainGame2_DataManager.instance.Question_number = Random.Range(0, BrainGame2_DataManager.instance.problem_list.Count);
        Problem_text.text = BrainGame2_DataManager.instance.problem_list[BrainGame2_DataManager.instance.Question_number].ToString();
    }

    public void OnClick_NumberBtn(GameObject name)
    {
        if (AnswerValue.Length == 0) AnswerValue = "0";
        if(AnswerValue.Substring(0, 1).Equals("0")) 
        { 
            AnswerValue = AnswerValue.Substring(0,0);
            SetValue(name);
        }
        else
        {
            SetValue(name);
        }

        AnswerNum.text = AnswerValue;
        Debug.Log(AnswerNum.text.Length);
    }
    public void AnswerQusetion()
    {
        if (AnswerValue.Equals(BrainGame2_DataManager.instance.QuestionValue_list[BrainGame2_DataManager.instance.Question_number]))
        {
          BrainGame2_DataManager.instance.TimerManager_sc.Question_Success();
        }
        else
        {
            BrainGame2_DataManager.instance.TimerManager_sc.Question_Fail();
        }
    }
    void SetValue(GameObject name)
    { 
        if (name.name.Equals("1")) { AnswerValue += "1"; }
        else if (name.name.Equals("2")) { AnswerValue += "2"; }
        else if (name.name.Equals("3")) { AnswerValue += "3"; }
        else if (name.name.Equals("4")) { AnswerValue += "4"; }
        else if (name.name.Equals("5")) { AnswerValue += "5"; }
        else if (name.name.Equals("6")) { AnswerValue += "6"; }
        else if (name.name.Equals("7")) { AnswerValue += "7"; }
        else if (name.name.Equals("8")) { AnswerValue += "8"; }
        else if (name.name.Equals("9")) { AnswerValue += "9"; }
        else if (name.name.Equals("0"))
        {
            if (!AnswerValue.Length.Equals(0))
                AnswerValue += "0";
        }
        else if (name.name.Equals("Back"))
        {
            if (AnswerValue.Length > 0)
                AnswerValue = AnswerValue.Substring(0, AnswerNum.text.Length - 1);
            else AnswerValue = "0";
        }
        else if (name.name.Equals("OK")) { AnswerQusetion(); }

    }
}
