using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DementiaGame3_DrawCtrl : MonoBehaviour
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
        Debug.Log(obName);
        if (col_List.Count < 2)
        {
            col_List.Add(collision.gameObject);
        }
        //이미지 ....가 랜덤으로 들어감 .. 
        if (spstring[0].Equals("start"))
        {
            startNum = spstring[1]; //문제가 적혀있는 번호
            star_num = Int32.Parse(startNum);
            string obj_str = DementiaGame3_DataManager.instance.Object_Data_Num[star_num]; //리스트 x번째 있는 속담
            int obj_num = Int32.Parse(obj_str);
            Debug.Log("obj Num : " + obj_num);
            start_proverbNum = DementiaGame3_DataManager.instance.object_Data[obj_num - 1]["number"]; //9
            Debug.Log("start_proverbNum : " + start_proverbNum);
        }
        else if (spstring[0].Equals("end"))
        {
            endNum = spstring[1];
            end_num = Int32.Parse(endNum);
            Debug.Log("end_num : " + end_num);
            string objNum_str = DementiaGame3_DataManager.instance.Object_Data_SuffleNum[end_num];
            end_proverbNum = objNum_str;
           Debug.Log("end_proverbNum : " + end_proverbNum);
        }
        if (col_List.Count >= 2)
        {
            trigger1 = true;
            if (start_proverbNum.Equals(end_proverbNum))
            {
                if (DementiaGame3_DataManager.instance.AnswerQuestionCount < 3)
                {
                    this.gameObject.GetComponent<LineRenderer>().positionCount = 2;
                    DementiaGame3_DataManager.instance.AnswerQuestionCount += 1;
                    startVecPos = DementiaGame3_DataManager.instance.UIManager.startPos[star_num].GetComponent<Transform>().position;
                    endVecPos = DementiaGame3_DataManager.instance.UIManager.endPos[end_num].GetComponent<Transform>().position;
                    Debug.Log("AnswerQuestionCount :" + DementiaGame3_DataManager.instance.AnswerQuestionCount);
                    this.gameObject.GetComponent<LineRenderer>().SetPosition(0, startVecPos);
                    this.gameObject.GetComponent<LineRenderer>().SetPosition(1, endVecPos);
                    for (int i = 0; i < col_List.Count; i++)
                    {
                        col_List[i].gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    }
                }
                else
                {
                    DementiaGame3_DataManager.instance.TimerManager_sc.Question_Success();
                }

            }
            else
            {
                DementiaGame3_DataManager.instance.TimerManager_sc.Question_Fail();
                this.gameObject.GetComponent<LineRenderer>().positionCount = 0;
            }
        }

    }
}
