using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DementiaGame4_UIManager : MonoBehaviour
{
    [Header("UI")]
    //2022.11.21 수정
    public Image QusetionImg;
    public Image[] PreViews;
    public Sprite[] Food_Pizza_img; //Pizza
    public Sprite[] Food_Cake_img; //Cake
    public Sprite[] Food_Donuts_img; //Donuts
    public Sprite[] Food_Color_img; //Color
    public Sprite[] Food_Bar_img; //Bar
    [Space(10)]
    public Sprite[] Fraction_Pizza_img; //Pizza
    public Sprite[] Fraction_Cake_img; //Cake
    public Sprite[] Fraction_Donuts_img; //Donuts
    public Sprite[] Fraction_Color_img; //Color
    public Sprite[] Fraction_Bar_img; //Bar


    public void OnClick_AnswerBtn(int btnNum)
    {
        //정답 맞춤
        if (DementiaGame4_DataManager.instance.QuestionNum[btnNum]
            .Equals(DementiaGame4_DataManager.instance.AnswerNumber))
        {
            SetUIGrup.instance.Question_Success();
        }
        else
        {
            SceneSoundCtrl.Instance.GameFailSound();
        }
    }
}
