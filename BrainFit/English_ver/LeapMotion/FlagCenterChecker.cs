using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCenterChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("L_Hand"))
        {
            if (AppManager.Instance.gameRunning)
            {
                Touch_BCenter();
            }
            //Touch_BCenter();
        }
        else if (other.CompareTag("R_Hand"))
        {
            if (AppManager.Instance.gameRunning)
            {
                Touch_WCenter();
            }
            //Touch_WCenter();
        }
    }

    public void Touch_BCenter()
    {
        QuizManager.Instance.BLeft_Text.text = "BC";
        QuizManager.Instance.BLeft = "BC";
        UIManager.Instance.BlueFlag_img.sprite = UIManager.Instance.BlueFlag_sp[0];
    }

    public void Touch_WCenter()
    {
        QuizManager.Instance.WRight_Text.text = "WC";
        QuizManager.Instance.WRight = "WC";
        UIManager.Instance.WhiteFlag_img.sprite = UIManager.Instance.WhiteFlag_sp[0];
    }
}
