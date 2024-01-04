using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame6_UIManager : MonoBehaviour
{
    public Button[] QuestionGrup_Btn;
    public Text[] QuestionGrup_t;
    public Image[] QuestionGrup_BackColor;
    public Sprite[] QuestionGrup_sp;
    public Sprite[] QuestionGrup_sp_select;
    Color strColor;
    string[][] Split_Str;
    public int Find_Correct_i =0;

    public void OnClick_MatchColor(int number)
    {
        string str = BrainGame6_DataManager.instance.Random_Color_Lsit[number];
        if (BrainGame6_DataManager.instance.Correct_Color_Lsit.Contains(str)) 
        { 
            Find_Correct_i += 1;
            if (QuestionGrup_t[number].text.Equals("검정")) { QuestionGrup_BackColor[number].sprite = QuestionGrup_sp_select[0]; }
            else if (QuestionGrup_t[number].text.Equals("파랑")) { QuestionGrup_BackColor[number].sprite = QuestionGrup_sp_select[1]; }
            else if (QuestionGrup_t[number].text.Equals("초록")) { QuestionGrup_BackColor[number].sprite = QuestionGrup_sp_select[2]; }
            else if (QuestionGrup_t[number].text.Equals("빨강")) { QuestionGrup_BackColor[number].sprite = QuestionGrup_sp_select[3]; }
            else if (QuestionGrup_t[number].text.Equals("노랑")) { QuestionGrup_BackColor[number].sprite = QuestionGrup_sp_select[4]; }
            QuestionGrup_Btn[number].interactable = false;
         
        }
        else
        {
            SceneSoundCtrl.Instance.GameFailSound();
           // BrainGame6_DataManager.instance.TimeManager_sc.Question_Fail();
        }
    }

    //0_검정,Black 1_파랑,Blue 2_초록, Green 3_빨강,Red 4_노랑, Yellow 
    public void ColorData(int listNumber)
    {
        string text = BrainGame6_DataManager.instance.Random_Color_Lsit[listNumber];
        string[] split_text;
        split_text = text.Split(',');
        QuestionGrup_t[listNumber].text = split_text[0];
        string color_c = split_text[1];
        if (color_c.Equals("Black")){ QuestionGrup_BackColor[listNumber].sprite = QuestionGrup_sp[0]; }
        else if (color_c.Equals("Blue")) { QuestionGrup_BackColor[listNumber].sprite = QuestionGrup_sp[1]; }
        else if (color_c.Equals("Green")) { QuestionGrup_BackColor[listNumber].sprite = QuestionGrup_sp[2]; }
        else if (color_c.Equals("Red")) { QuestionGrup_BackColor[listNumber].sprite = QuestionGrup_sp[3]; }
        else if (color_c.Equals("Yellow")) { QuestionGrup_BackColor[listNumber].sprite = QuestionGrup_sp[4]; }
    }
}
