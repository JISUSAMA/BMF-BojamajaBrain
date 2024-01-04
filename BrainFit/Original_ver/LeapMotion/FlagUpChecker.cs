using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagUpChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("L_Hand"))
        {
            if (AppManager.Instance.gameRunning)
            {
                Touch_BUp();
            }
        }
        else if (other.CompareTag("R_Hand"))
        {
            if (AppManager.Instance.gameRunning)
            {
                Touch_WUp();
            }
        }
    }

    //청기올려 버튼
    public void Touch_BUp()
    {
        QuizManager.Instance.BLeft_Text.text = "BU";
        QuizManager.Instance.BLeft = "BU";
        UIManager.Instance.BlueFlag_img.sprite = UIManager.Instance.BlueFlag_sp[2];
    }

    //백기올려 버튼
    public void Touch_WUp()
    {
        QuizManager.Instance.WRight_Text.text = "WU";
        QuizManager.Instance.WRight = "WU";
        UIManager.Instance.WhiteFlag_img.sprite = UIManager.Instance.WhiteFlag_sp[2];
    }
}
