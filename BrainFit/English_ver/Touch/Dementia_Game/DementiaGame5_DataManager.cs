using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame5_DataManager : MonoBehaviour
{
    public Touch_TimerManager TimerManager_sc;
    public DementiaGame5_UIManager UIManager;
    public static DementiaGame5_DataManager instance { get; private set; }
    /// <summary>
    /// 그림 속에 숨겨진 사물 5개를 정해진 시간 안에 찾는 게임
    /// 1) 시간 내 완료하지 못하면 실패
    /// </summary>
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
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        DementiaGame5_Play();
        yield return null;
    }

    public void DementiaGame5_Play()
    {
        StopCoroutine("_DementiaGame5_Play");
        StartCoroutine("_DementiaGame5_Play");
    }
    IEnumerator _DementiaGame5_Play()
    {
        UIManager.PaintGrup[Random.Range(0, UIManager.PaintGrup.Length)].gameObject.SetActive(true);
        TimerManager_sc.FindWord_sec_Timer(28,10,5);
        yield return null;
    }

}
