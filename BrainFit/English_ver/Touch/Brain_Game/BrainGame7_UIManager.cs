using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame7_UIManager : MonoBehaviour
{
    [Header("UI")]
  //  public GameObject[] QuestionGrup;
    public Image[] Question_img_left; 
    public Image[] Question_img_right;

    public Sprite[] LeftIMG_sprite; //문제 이미지
    public Sprite[] RightIMG_Correct; //올바른 반전 이미지
    public Sprite[] RightIMG_Wrong; //틀린 반전 이미지

    public Toggle[] Q1_toggle, Q2_toggle, Q3_toggle, Q4_toggle, Q5_toggle;
  //  public Image[] Q1_img, Q2_img, Q3_img, Q4_img, Q5_img; //0:O 1:X
    bool Q1, Q2, Q3, Q4, Q5;
    int Q1_i, Q2_i, Q3_i, Q4_i, Q5_i;
  
    public void Toggle_Check(string name)
    {
        switch(name)
        {
            case "Q1_toggle":
                if (Q1_toggle[0].isOn) { Q1 = true; }
                else { Q1 = false; }
                break;
            case "Q2_toggle":
                if (Q2_toggle[0].isOn) { Q2 = true; }
                else { Q2 = false; }
                break;

            case "Q3_toggle":
                if (Q3_toggle[0].isOn) { Q3 = true;  }
                else { Q3 = false;}

                break;
            case "Q4_toggle":
                if (Q4_toggle[0].isOn) { Q4 = true;}
                else { Q4 = false;}
       
                break;
            case "Q5_toggle":
                if (Q5_toggle[0].isOn) { Q5 = true; }
                else { Q5 = false;}
                break;
        }

    
     
       
      
     
    }
    public void OnClick_OK_button()
    {
        if (Q1) Q1_i = 0; else Q1_i = 1;
        if (Q2) Q2_i = 0; else Q2_i = 1;
        if (Q3) Q3_i = 0; else Q3_i = 1;
        if (Q4) Q4_i = 0; else Q4_i = 1;
        if (Q5) Q5_i = 0; else Q5_i = 1;
        
        //정답일경우 
        if (BrainGame7_DataManager.instance.Question_answer[0].Equals(Q1_i)
            && BrainGame7_DataManager.instance.Question_answer[1].Equals(Q2_i) 
            && BrainGame7_DataManager.instance.Question_answer[2].Equals(Q3_i) 
            && BrainGame7_DataManager.instance.Question_answer[3].Equals(Q4_i) 
            && BrainGame7_DataManager.instance.Question_answer[4].Equals(Q5_i)) 
        {
            BrainGame7_DataManager.instance.TimerManager_sc.Question_Success();
        }
        else
        {
            BrainGame7_DataManager.instance.TimerManager_sc.Question_Fail();
        }
    }
}
