using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerRot : MonoBehaviour
{
    
    void Update()
    {
        //타이머 이미지 빙글빙글
        this.transform.Rotate(new Vector3(0, 0, -130f * Time.deltaTime));
    }
}
