using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartButton_Arithmetic : MonoBehaviour
{
    private BoxCollider boxCollider;
    public TMP_Text guidlineText;
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
