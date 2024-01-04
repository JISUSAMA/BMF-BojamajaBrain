using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartButton : MonoBehaviour
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
            string t = GestureManager.Instance.GetSelectedHand();

            if (t == "선택되지않았음")
            {
                // 손을 선택해 주세요. 프린트
                boxCollider.isTrigger = true;
                StartCoroutine(BlinkText("게임을 진행 할 손을 선택해 주세요."));
            }
            else
            {
                AppManager.Instance.OnRoundStart();
            }
        }
        else if (other.CompareTag("L_Hand"))
        {
            boxCollider.isTrigger = false;
            string t = GestureManager.Instance.GetSelectedHand();

            if (t == "선택되지않았음")
            {
                // 손을 선택해 주세요. 프린트                                
                StartCoroutine(BlinkText("게임을 진행 할 손을 선택해 주세요."));
                boxCollider.isTrigger = true;
            }
            else 
            {
                AppManager.Instance.OnRoundStart();
            }
        }
    }

    IEnumerator BlinkText(string a_text)
    {
        guidlineText.text = a_text;
        int i = 10;
        Color c;

        yield return new WaitForSeconds(2f);

        while (i > 0f)
        {
            i -= 1;
            float f = i / 10.0f;
            c = guidlineText.color;
            c.a = f;
            guidlineText.color = c;
            yield return new WaitForSeconds(0.02f);
        }

        guidlineText.text = "";
        c = guidlineText.color;
        c.a = 1f;
        guidlineText.color = c;

        yield return null;
    }
}
