using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDownChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("L_Hand"))
        {
            if (AppManager.Instance.gameRunning)
            {
                Touch_BDown();
            }
        }
        else if (other.CompareTag("R_Hand"))
        {
            if (AppManager.Instance.gameRunning)
            {
                Touch_WDown();
            }
        }
    }

    //청기올려 버튼
    public void Touch_BDown()
    {
        QuizManager.Instance.BLeft_Text.text = "BD";
        QuizManager.Instance.BLeft = "BD";
        UIManager.Instance.BlueFlag_img.sprite = UIManager.Instance.BlueFlag_sp[1];
    }

    //백기올려 버튼
    public void Touch_WDown()
    {
        QuizManager.Instance.WRight_Text.text = "WD";
        QuizManager.Instance.WRight = "WD";
        UIManager.Instance.WhiteFlag_img.sprite = UIManager.Instance.WhiteFlag_sp[1];
    }
}
