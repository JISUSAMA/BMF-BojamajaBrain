using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame4_UIManager : MonoBehaviour
{
    public GameObject[] LeftGrup; //도형이 담긴 그룹
    public GameObject[] RigthGrup;//도형이 담긴 그룹
    public List<Image> QuestionLeft_img; // 왼쪽 문제 도형의 이미지 접근
    public List<Image> QuestionRight_img;  // 오른쪽 문제 도형의 이미지 접근
    Color newColor;
    [SerializeField] string newColor_code;
    [Header("DrawLine")]
    public GameObject handle;
    public GameObject linePrefab;

    public Text DebugText_0;
    public Text DebugText_1;
    public Text DebugText_2;

  //  public List<TouchLocation> touches = new List<TouchLocation>();
    public List<GameObject> touches_ob = new List<GameObject>();
    public GameObject currentLine;
    public bool touch_bool = true;


    public Vector2[] delta = new Vector2[5];

    public void DrawLine_Start()
    {
        StopCoroutine("_DrawLine_Start");
        StartCoroutine("_DrawLine_Start");
    }
    IEnumerator _DrawLine_Start()
    {
        BrainGame4_DataManager.instance.DrawLine_GameRun = true;
        while (BrainGame4_DataManager.instance.DrawLine_GameRun)
        {
            if (touches_ob.Count.Equals(0))
            {
                touch_bool = true;
            }
            if (touch_bool)
            {
                int count = Input.touchCount;
                for (int i = 0; i < count; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    int id = touch.fingerId;
                    //터치좌표
                    Vector2 pos = touch.position;
                    //begin이라면 무조건 delta에 넣어줌
                    if (touch.phase == TouchPhase.Began)
                    {
                        GameObject ob = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
                        touches_ob.Add(ob); //라인 오브젝트 넣어줌
                        ob.GetComponent<BrainGame4_DrawCtrl>().index = id;
                        //    delta[id] = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        if (BrainGame4_DataManager.instance.L_checkPoint1 && BrainGame4_DataManager.instance.L_checkPoint2
                        && BrainGame4_DataManager.instance.L_checkPoint3 && BrainGame4_DataManager.instance.L_checkPoint4
                        && BrainGame4_DataManager.instance.L_checkPoint5 && BrainGame4_DataManager.instance.R_checkPoint1
                        && BrainGame4_DataManager.instance.R_checkPoint2 && BrainGame4_DataManager.instance.R_checkPoint3
                        && BrainGame4_DataManager.instance.R_checkPoint4 && BrainGame4_DataManager.instance.R_checkPoint5)
                        {
                            SetUIGrup.instance.Question_Success();
                            BrainGame4_DataManager.instance.DrawLine_GameRun = false;
                        }
                        else
                        {
                            SceneSoundCtrl.Instance.GameFailSound();
                            //콜라이더 초기화 
                            Clear_Line_Data();
                        }
                    }
                }
            }
            else
            {
                if (!touches_ob.Count.Equals(0))
                {
                    Clear_Line_Data();
                }
                yield return new WaitForSeconds(1f);
                yield return new WaitUntil(() => touches_ob.Count.Equals(0));
                touch_bool = true;
            }
            yield return null;
        }

    }
    public void Clear_Line_Data()
    {
        touch_bool = false;
        BrainGame4_DataManager.instance.L_checkPoint1 = false;
        BrainGame4_DataManager.instance.L_checkPoint2 = false;
        BrainGame4_DataManager.instance.L_checkPoint3 = false;
        BrainGame4_DataManager.instance.L_checkPoint4 = false;
        BrainGame4_DataManager.instance.L_checkPoint5 = false;
        BrainGame4_DataManager.instance.R_checkPoint1 = false;
        BrainGame4_DataManager.instance.R_checkPoint2 = false;
        BrainGame4_DataManager.instance.R_checkPoint3 = false;
        BrainGame4_DataManager.instance.R_checkPoint4 = false;
        BrainGame4_DataManager.instance.R_checkPoint5 = false;

        for(int i =0; i<touches_ob.Count; i++)
        {
            Destroy(touches_ob[i]);
        }
        touches_ob.Clear();
        newColor_code = "#FFFFFF";
        for (int j = 0; j < 5; j++)
        {
            if (ColorUtility.TryParseHtmlString(newColor_code, out newColor))
            {
                QuestionLeft_img[j].color = newColor;
                QuestionRight_img[j].color = newColor;
            }
        }
    }

    public void Set_QuestionShape()
    {
        BrainGame4_DataManager.instance.QuestionLeft_Num = Random.Range(0, LeftGrup.Length);
        BrainGame4_DataManager.instance.QuestionRight_Num = Random.Range(0, RigthGrup.Length);
        LeftGrup[BrainGame4_DataManager.instance.QuestionLeft_Num].SetActive(true);
        RigthGrup[BrainGame4_DataManager.instance.QuestionRight_Num].SetActive(true);

        GameObject leftOb = LeftGrup[BrainGame4_DataManager.instance.QuestionLeft_Num];
        GameObject rightOb = RigthGrup[BrainGame4_DataManager.instance.QuestionRight_Num];
        
        for(int i =1; i<6; i++)
        {
            QuestionLeft_img.Add(leftOb.transform.GetChild(i).gameObject.GetComponent<Image>());
            QuestionRight_img.Add(rightOb.transform.GetChild(i).gameObject.GetComponent<Image>());
        }
        
    }
}

