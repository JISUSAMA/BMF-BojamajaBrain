using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DementiaGame2_DrawCtrl : MonoBehaviour
{
    public List<GameObject> col_List;
    public string start_proverbNum, end_proverbNum, startNum, endNum;
    int star_num, end_num; //end 번호 0,1,2,3
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
      
        if (col_List.Count < 2)
        {
            col_List.Add(collision.gameObject);
        }
        if (spstring[0].Equals("start"))
        {
     
            startNum = spstring[1];
            star_num = Int32.Parse(startNum); 
       
            int proverb_int = DementiaGame2_DataManager.instance.ProverbNum_Shuffle[star_num]; //리스트 x번째 있는 속담
            start_proverbNum = proverb_int.ToString(); //9
            //Debug.LogError("star_num: " + star_num + " proverb_int: " + proverb_int);
            //  start_proverbNum = DementiaGame2_DataManager.instance.proverb_data[proverb_num - 1]["number"]; //9

        }
        else if (spstring[0].Equals("end"))
        {
            endNum = spstring[1];
            end_num = Int32.Parse(endNum);
            int proverbNum_int = DementiaGame2_DataManager.instance.ProverbNum_List[end_num];
            end_proverbNum = proverbNum_int.ToString(); //9
           // Debug.LogError("end_num: " + end_num + " end_proverbNum: " + end_proverbNum);
            //end_proverbNum = DementiaGame2_DataManager.instance.ProverbNum_Shuffle[proverbNum_i - 1]["number"]; //9

        }
        if (col_List.Count >= 2)
        {
           // Debug.LogError("start_proverbNum: " + start_proverbNum + " end_proverbNum: " + end_proverbNum);
            trigger1 = true;
            if (start_proverbNum.Equals(end_proverbNum))
            {
                if (DementiaGame2_DataManager.instance.AnswerQuestionCount < 3)
                {
                    this.gameObject.GetComponent<LineRenderer>().positionCount = 2;
                    DementiaGame2_DataManager.instance.AnswerQuestionCount += 1;
                    startVecPos = DementiaGame2_DataManager.instance.UIManager.startPos[star_num].GetComponent<Transform>().position;
                    endVecPos = DementiaGame2_DataManager.instance.UIManager.endPos[end_num].GetComponent<Transform>().position;
                   // Debug.Log("AnswerQuestionCount :" + DementiaGame2_DataManager.instance.AnswerQuestionCount);
                    this.gameObject.GetComponent<LineRenderer>().SetPosition(0, startVecPos);
                    this.gameObject.GetComponent<LineRenderer>().SetPosition(1, endVecPos);
                    for (int i = 0; i < col_List.Count; i++)
                    {
                        col_List[i].gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    }
                }
                else
                {
                    DementiaGame2_DataManager.instance.TimerManager_sc.Question_Success();
                }

            }
            else
            {
                DementiaGame2_DataManager.instance.TimerManager_sc.Question_Fail();
                this.gameObject.GetComponent<LineRenderer>().positionCount = 0;
            }
        }

    }
}
