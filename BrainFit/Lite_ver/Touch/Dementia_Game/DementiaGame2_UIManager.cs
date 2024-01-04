using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI; 

public class DementiaGame2_UIManager : MonoBehaviour
{

    [Header("UI")]
    public Image ProverbImage; //문제 속담 카드 
    public Sprite[] ProverbSpriteKinds; //속담 카드 이미지 종류 
    public Text[] ProverbText; //랜덤 속담 텍스트

    public void OnClick_Proverb_Btn(int num)
    {
        if (ProverbText[num].text.Equals(DementiaGame2_DataManager.instance.Proverb_List))
        {
            SetUIGrup.instance.Question_Success();
        }
        else
        {
            SceneSoundCtrl.Instance.GameFailSound();
        }

    }
}
