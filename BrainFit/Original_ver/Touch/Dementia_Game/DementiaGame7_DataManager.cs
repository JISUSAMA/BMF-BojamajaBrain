using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DementiaGame7_DataManager : MonoBehaviour
{
    public Touch_TimerManager TimerManager_sc;

    public static DementiaGame7_DataManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;
    }
    private void Start()
    {
        StopCoroutine(_GameStart());
        StartCoroutine(_GameStart());

    }
    IEnumerator _GameStart()
    {
        int rand = Random.Range(0, DementiaGame7_UIManager.instance.Question_Kinds.Length);
        DementiaGame7_UIManager.instance.Question_Kinds[rand].gameObject.SetActive(true);//게임 판넬 활성화 
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        TimerManager_sc.FindWord_sec_Timer(35, 20, 8);
        yield return null;
    }


}
