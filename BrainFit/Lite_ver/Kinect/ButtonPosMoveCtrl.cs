using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPosMoveCtrl : MonoBehaviour
{
    public static ButtonPosMoveCtrl instance { get; private set; }


    public Transform[] butPos;  //버튼 생성될 위치 - R_T, R_B, L_T, L_B

    public GameObject[] btn_Obj;  //버튼

    public int clickNumber = 1; //클릭 횟수


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    //한팔 위로올리기, 내리기 운동
    public void ArmLeftRight_UpDownExercise(int _clickNumber)
    {
        //나머지가 0일 때 - Left Botton
        if((_clickNumber % 4).Equals(0))
        {
            btn_Obj[0].transform.position = new Vector3(butPos[3].position.x, butPos[3].position.y, butPos[3].position.z);
        }
        //나머지가 1일때 - Right Up
        else if((_clickNumber % 4).Equals(1))
        {
            btn_Obj[0].transform.position = new Vector3(butPos[0].position.x, butPos[0].position.y, butPos[0].position.z);
        }
        //나머지가 2일때 - Right Botton
        else if((_clickNumber % 4).Equals(2))
        {
            btn_Obj[0].transform.position = new Vector3(butPos[1].position.x, butPos[1].position.y, butPos[1].position.z);
        }
        //나머지가 3일떄 - Left Up
        else if((_clickNumber % 4).Equals(3))
        {
            btn_Obj[0].transform.position = new Vector3(butPos[2].position.x, butPos[2].position.y, butPos[2].position.z);
        }
    }
}
