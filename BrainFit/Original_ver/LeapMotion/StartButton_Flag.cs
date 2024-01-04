using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartButton_Flag : MonoBehaviour
{
    private BoxCollider boxCollider;    
    private void Start()
    {
        boxCollider = this.GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("R_Hand"))
        {
            boxCollider.isTrigger = false;
            AppManager.Instance.OnRoundStart();
        }
        else if (other.CompareTag("L_Hand"))
        {
            boxCollider.isTrigger = false;
            AppManager.Instance.OnRoundStart();
        }
    }
}
