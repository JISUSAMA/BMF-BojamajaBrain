using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame4_CollisionCtrl : MonoBehaviour
{
    Color newColor;
    [SerializeField] string newColor_code;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject thisOB = this.gameObject;
        //#FFC340
        if (thisOB.CompareTag("Left"))
        {
            if (thisOB.name.Equals("Left_1")) { BrainGame4_DataManager.instance.L_checkPoint1 = true; }
            else if (thisOB.name.Equals("Left_2")) { BrainGame4_DataManager.instance.L_checkPoint2 = true; }
            else if (thisOB.name.Equals("Left_3")) { BrainGame4_DataManager.instance.L_checkPoint3 = true; }
            else if (thisOB.name.Equals("Left_4")) { BrainGame4_DataManager.instance.L_checkPoint4 = true; }
            else if (thisOB.name.Equals("Left_5")) { BrainGame4_DataManager.instance.L_checkPoint5 = true; }

        }
        if (thisOB.CompareTag("Right"))
        {
            if (thisOB.name.Equals("Right_1")) { BrainGame4_DataManager.instance.R_checkPoint1 = true; }
            else if (thisOB.name.Equals("Right_2")) { BrainGame4_DataManager.instance.R_checkPoint2 = true; }
            else if (thisOB.name.Equals("Right_3")) { BrainGame4_DataManager.instance.R_checkPoint3 = true; }
            else if (thisOB.name.Equals("Right_4")) { BrainGame4_DataManager.instance.R_checkPoint4 = true; }
            else if (thisOB.name.Equals("Right_5")) { BrainGame4_DataManager.instance.R_checkPoint5 = true; }
        }
        newColor_code = "#FFC340";
        if (ColorUtility.TryParseHtmlString(newColor_code, out newColor))
        {
            thisOB.GetComponent<Image>().color = newColor;
        }
    }
}
