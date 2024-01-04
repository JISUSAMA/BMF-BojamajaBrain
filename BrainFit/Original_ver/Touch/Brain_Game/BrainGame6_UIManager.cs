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
            if (QuestionGrup_t[number].text.Equals("°ËÁ¤")) { QuestionGrup_BackColor[number].sprite = QuestionGrup_sp_select[0]; }
            else if (QuestionGrup_t[number].text.Equals("ÆÄ¶û")) { QuestionGrup_BackColor[number].sprite = QuestionGrup_sp_select[1]; }
            else if (QuestionGrup_t[number].text.Equals("ÃÊ·Ï")) { QuestionGrup_BackColor[number].sprite = QuestionGrup_sp_select[2]; }
            else if (QuestionGrup_t[number].text.Equals("»¡°­")) { QuestionGrup_BackColor[number].sprite = QuestionGrup_sp_select[3]; }
            else if (QuestionGrup_t[number].text.Equals("³ë¶û")) { QuestionGrup_BackColor[number].sprite = QuestionGrup_sp_select[4]; }
            QuestionGrup_Btn[number].interactable = false;
         
        }
        else
        {
            BrainGame6_DataManager.instance.TimeManager_sc.Question_Fail();
        }
    }

    //0_°ËÁ¤,Black 1_ÆÄ¶û,Blue 2_ÃÊ·Ï, Green 3_»¡°­,Red 4_³ë¶û, Yellow 
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
