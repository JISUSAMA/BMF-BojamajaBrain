using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DementiaGame4_DrawCtrl : MonoBehaviour
{
    public List<GameObject> col_List;
    public string startNum, endNum;
    public int start_Num, end_Num;
    int start_num, end_num; //end 번호 0,1,2,3
    public Vector2 startVecPos, endVecPos;
    public bool trigger1;

    private void Update()
    {
      if (Input.GetMouseButtonUp(0))
      {
          if (!trigger1)
          {
              this.gameObject.GetComponent<LineRenderer>().positionCount = 0;
              Destroy(this.gameObject, 1f);
          }
      }
      if (!GameAppManager.instance.playBool) Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        string obName = collision.gameObject.name;
        char sp = ' ';
        string[] spstring = obName.Split(sp);
        Debug.Log(obName);
        if (col_List.Count < 2)
        {
            col_List.Add(collision.gameObject);
        }
        //이미지 ....가 랜덤으로 들어감 .. 
        if (spstring[0].Equals("start"))
        {
            startNum = spstring[1]; //문제가 적혀있는 번호
            start_num = Int32.Parse(startNum);
            start_Num = DementiaGame4_DataManager.instance.Fraction_int[start_num];
            Debug.Log("start_proverbNum : " + start_Num);
        }
        else if (spstring[0].Equals("end"))
        {
            endNum = spstring[1];
            end_num = Int32.Parse(endNum);
            Debug.Log("end_num : " + end_num);
            end_Num = DementiaGame4_DataManager.instance.Food_int[end_num];
            Debug.Log("end_proverbNum : " + end_Num);
        }
        if (col_List.Count >= 2)
        {
            trigger1 = true;
            if (start_Num.Equals(end_Num))
            {
                if (DementiaGame4_DataManager.instance.AnswerQuestionCount < 4)
                {
                    this.gameObject.GetComponent<LineRenderer>().positionCount = 2;
                    DementiaGame4_DataManager.instance.AnswerQuestionCount += 1;
                    startVecPos = DementiaGame4_DataManager.instance.UIManager.startPos[start_num].GetComponent<Transform>().position;
                    endVecPos = DementiaGame4_DataManager.instance.UIManager.endPos[end_num].GetComponent<Transform>().position;
                    Debug.Log("AnswerQuestionCount :" + DementiaGame4_DataManager.instance.AnswerQuestionCount);
                    this.gameObject.GetComponent<LineRenderer>().SetPosition(0, startVecPos);
                    this.gameObject.GetComponent<LineRenderer>().SetPosition(1, endVecPos);
                    for (int i = 0; i < col_List.Count; i++)
                    {
                        col_List[i].gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    }
                }
                else
                {
                    DementiaGame4_DataManager.instance.TimerManager_sc.Question_Success();
                }

            }
            else
            {
                DementiaGame4_DataManager.instance.TimerManager_sc.Question_Fail();
                this.gameObject.GetComponent<LineRenderer>().positionCount = 0;
            }
        }

    }
}
