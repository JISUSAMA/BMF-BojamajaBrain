using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainGame4_UIManager : MonoBehaviour
{
    public GameObject[] LeftGrup; //도형이 담긴 그룹
    public GameObject[] RigthGrup;//도형이 담긴 그룹


    [Header("DrawLine")]
    public GameObject handle;
    public GameObject linePrefab;

    public Text DebugText_0;
    public Text DebugText_1;

    public List<TouchLocation> touches = new List<TouchLocation>();
    public List<GameObject> touches_ob = new List<GameObject>();
    public GameObject currentLine;

    public Vector2[] delta = new Vector2[5];
    public void DrawLine_Start()
    {
        StopCoroutine("_DrawLine_Start");
        StartCoroutine("_DrawLine_Start");
    }
    IEnumerator _DrawLine_Start()
    {
        Set_QuestionShape();
        BrainGame4_DataManager.instance.DrawLine_GameRun = true;
        while (BrainGame4_DataManager.instance.DrawLine_GameRun)
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
                    ob.GetComponent<BrainGame4_DrawCtrl>().index = id;
                    delta[id] = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (BrainGame4_DataManager.instance.checkPoint1 && BrainGame4_DataManager.instance.checkPoint2
                    && BrainGame4_DataManager.instance.checkPoint3 && BrainGame4_DataManager.instance.checkPoint4)
                    {
                        BrainGame4_DataManager.instance.TimerManager_sc.Question_Success();
                        BrainGame4_DataManager.instance.DrawLine_GameRun = false;
                    }
                    else
                    {
                       BrainGame4_DataManager.instance.TimerManager_sc.Question_Fail();
                        BrainGame4_DataManager.instance.DrawLine_GameRun = false;
                    }
                }

                DebugText_0.text = " checkPoint1 :  " + BrainGame4_DataManager.instance.checkPoint1 + " checkPoint2 : " + BrainGame4_DataManager.instance.checkPoint2
                + " checkPoint3 :  " + BrainGame4_DataManager.instance.checkPoint3 + " checkPoint4 : " + BrainGame4_DataManager.instance.checkPoint4;
            }

            //     if (Input.GetMouseButtonDown(0))
            //     {
            //         GameObject ob = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            //         touches_ob.Add(ob);
            //     }
            //

            yield return null;
        }

    }
   
    public void Set_QuestionShape()
    {
        BrainGame4_DataManager.instance.QuestionLeft_Num = Random.Range(0, LeftGrup.Length);
        BrainGame4_DataManager.instance.QuestionRight_Num = Random.Range(0, RigthGrup.Length);
        LeftGrup[BrainGame4_DataManager.instance.QuestionLeft_Num].SetActive(true);
        RigthGrup[BrainGame4_DataManager.instance.QuestionRight_Num].SetActive(true);
    }
}

