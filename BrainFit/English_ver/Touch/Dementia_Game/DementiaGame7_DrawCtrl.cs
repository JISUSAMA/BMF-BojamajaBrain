using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame7_DrawCtrl : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Line_DementiaGame7(Clone)"))
        {
            if (this.gameObject.name.Equals("Trigger_1"))
                DementiaGame7_UIManager.instance.triggerCount[0] = true;
            else if (this.gameObject.name.Equals("Trigger_2"))
                DementiaGame7_UIManager.instance.triggerCount[1] = true;
            else if (this.gameObject.name.Equals("Trigger_3"))
                DementiaGame7_UIManager.instance.triggerCount[2] = true;
            else if (this.gameObject.name.Equals("Trigger_4"))
                DementiaGame7_UIManager.instance.triggerCount[3] = true;
            else if (this.gameObject.name.Equals("Trigger_5"))
                DementiaGame7_UIManager.instance.triggerCount[4] = true;
        }
    
        Debug.Log(collision.gameObject.name);
    }
}
