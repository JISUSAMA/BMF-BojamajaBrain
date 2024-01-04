using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame5_UIManager : MonoBehaviour
{
    public Button [] NumberBtn;
    public Text[] NumberBtn_text;

    public Sprite []NumberBtn_sp;
    public int CurrnetOrderNum =-1;

    public void OnClick_WordBtn(Text word)
    {
        CurrnetOrderNum += 1;
        //Debug.Log("word.text:" + word.text + "CurrnetOrderNum" + CurrnetOrderNum);
        AnswerQusetion(word.text, word.gameObject.GetComponentInParent<Button>());
    }
    public void AnswerQusetion(string wordStr ,Button Parent)
    {
      
        //정답
        if (BrainGame5_DataManager.instance.Order_word[CurrnetOrderNum].Equals(wordStr))
        {
            Parent.gameObject.GetComponent<Image>().sprite = NumberBtn_sp[1];
            Parent.interactable = false;
          
            if (CurrnetOrderNum > 23)
            {
                SetUIGrup.instance.Question_Success();
            }
        }
        //틀림
        else
        {
            CurrnetOrderNum -= 1;
            //   BrainGame5_DataManager.instance.TimerManager_sc.Question_Fail();
        }
    }
}
