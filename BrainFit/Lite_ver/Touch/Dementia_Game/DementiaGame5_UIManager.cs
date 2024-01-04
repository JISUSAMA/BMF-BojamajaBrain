using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DementiaGame5_UIManager : MonoBehaviour
{
    public GameObject[] PaintGrup;

    int FindObjectNum=0;
    public void OnClickBtn_Find(GameObject checkMark)
    {
        Debug.Log("버튼 클릭했어! : " + checkMark.name);
        checkMark.SetActive(true); //체크 마크 활성화 시켜주기 
        if (FindObjectNum != 4) FindObjectNum += 1;
        else
        {
            //게임 끝
            GameAppManager.instance.playBool = false;
            SetUIGrup.instance.Question_Success();
           // GameAppManager.instance.GameLoadScene();
        } 
    }
}
