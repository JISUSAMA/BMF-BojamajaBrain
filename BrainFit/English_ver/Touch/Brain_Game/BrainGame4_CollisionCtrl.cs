using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainGame4_CollisionCtrl : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (this.gameObject.name.Equals("Left_MidPoint")) { BrainGame4_DataManager.instance.checkPoint1 = true; }
       else if (this.gameObject.name.Equals("Left_EndPoint")) { BrainGame4_DataManager.instance.checkPoint2 = true; }
       else if (this.gameObject.name.Equals("Right_MidPoint")) { BrainGame4_DataManager.instance.checkPoint3 = true; }
       else if (this.gameObject.name.Equals("Right_EndPoint")) { BrainGame4_DataManager.instance.checkPoint4 = true; }

    }
}
