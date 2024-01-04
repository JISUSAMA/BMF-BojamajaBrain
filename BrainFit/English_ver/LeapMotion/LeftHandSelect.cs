using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandSelect : MonoBehaviour
{
    SkinnedMeshRenderer lefthand_renderer;
    SkinnedMeshRenderer righthand_renderer;

    public GameObject lefthand;
    public GameObject righthand;

    private Shader m_standard;
    private Shader m_transparentShader;
    private BoxCollider boxCollider;
    private void Start()
    {
        lefthand_renderer = lefthand.GetComponent<SkinnedMeshRenderer>();
        righthand_renderer = righthand.GetComponent<SkinnedMeshRenderer>();
        boxCollider = this.GetComponent<BoxCollider>();
        m_transparentShader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        m_standard = Shader.Find("Standard");
    }
   // private void OnTriggerEnter(Collider other)
   // {
   //     // materials 바꿔주기 : 오른손 투명
   //     // 오른손 동작 X , 왼손만 동작
   //     if (other.CompareTag("R_Hand"))
   //     {
   //         boxCollider.isTrigger = false;
   //         GestureManager.Instance.isSelectedLeftHand = true;
   //         GestureManager.Instance.isSelectedRightHand = false;
   //         UIManager.Instance.Left_Select.sprite = UIManager.Instance.Left_Right_Select[1];//왼쪽 활성화
   //         UIManager.Instance.Right_Select.sprite = UIManager.Instance.Left_Right_Select[2];//오른쪽 비활성화
   //
   //         //GestureManager.target_select_left();
   //         //GestureManager.target_deselect_right();
   //         StartCoroutine(FadeOut());
   //         StartCoroutine(FadeIn());
   //     }
   //     else if (other.CompareTag("L_Hand"))
   //     {
   //         boxCollider.isTrigger = false;
   //         GestureManager.Instance.isSelectedLeftHand = true;
   //         GestureManager.Instance.isSelectedRightHand = false;
   //         UIManager.Instance.Left_Select.sprite = UIManager.Instance.Left_Right_Select[1];//왼쪽 활성화
   //         UIManager.Instance.Right_Select.sprite = UIManager.Instance.Left_Right_Select[2];//오른쪽 비활성화
   //         StartCoroutine(FadeOut());
   //         StartCoroutine(FadeIn());
   //     }
   // }

    public void OnClick_leftHandBtn()
    {
        boxCollider.isTrigger = false;
        GestureManager.Instance.isSelectedLeftHand = true;
        GestureManager.Instance.isSelectedRightHand = false;
        UIManager.Instance.Left_Select.sprite = UIManager.Instance.Left_Right_Select[1];//왼쪽 활성화
        UIManager.Instance.Right_Select.sprite = UIManager.Instance.Left_Right_Select[2];//오른쪽 비활성화
        StartCoroutine(FadeOut());
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeOut()
    {
        righthand_renderer.material.shader = m_transparentShader;
        //obj.GetComponent<Renderer>().material.SetFloat("변수이름", 값);
        //leftMat.SetColor("_Color", new Color(255f, 255f, 255f, 0f));
        int i = 10;
        Color c;

        while (i > 0f)
        {
            i -= 1;
            float f = i / 10.0f;
            c = righthand_renderer.material.color;
            c.a = f;
            righthand_renderer.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }

        c = righthand_renderer.material.color;
        c.a = 0f;
        righthand_renderer.material.color = c;

        boxCollider.isTrigger = true;

        yield return null;
    }

    IEnumerator FadeIn()
    {
        lefthand_renderer.material.shader = m_transparentShader;
        //obj.GetComponent<Renderer>().material.SetFloat("변수이름", 값);
        //leftMat.SetColor("_Color", new Color(255f, 255f, 255f, 0f));
        int i = 0;
        Color c;

        while (i < 10f)
        {
            i += 1;
            float f = i / 10.0f;
            c = lefthand_renderer.material.color;
            c.a = f;
            lefthand_renderer.material.color = c;

            yield return new WaitForSeconds(0.02f);
        }

        c = lefthand_renderer.material.color;
        c.a = 1f;
        lefthand_renderer.material.color = c;

        lefthand_renderer.material.shader = m_standard;
        lefthand_renderer.material.SetFloat("_Mode", 0.0f);   // Opaque

        boxCollider.isTrigger = true;

        yield return null;
    }
}
