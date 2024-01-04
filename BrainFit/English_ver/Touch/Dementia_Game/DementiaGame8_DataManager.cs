using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame8_DataManager : MonoBehaviour
{
    public Touch_TimerManager TimeManager_sc;
    public DementiaGame8_UIManager UIManager;
    public string Q1_str, Q2_str, Q3_str, Q4_str;
    [SerializeField] List<Dictionary<string, string>> data;
    public static DementiaGame8_DataManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        data = CSVReader.Read("CalculationList"); //낱말찾기
    }
    private void Start()
    {
        StopCoroutine(_GameStart());
        StartCoroutine(_GameStart());

    }
    IEnumerator _GameStart()
    {
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        DementiaGame8_Play();
        yield return null;
    }
    public void DementiaGame8_Play()
    {
        StopCoroutine(_DementiaGame8_Play());
        StartCoroutine(_DementiaGame8_Play());
    }
    IEnumerator _DementiaGame8_Play()
    {
        int rand = Random.Range(0, data.Count);
        Set_Question_Data(rand);
        TimeManager_sc.FindWord_sec_Timer(18, 10, 3);
        yield return null;
    }

    public void Set_Question_Data(int num)
    {

        Q1_str = data[num]["Q1"];
        Q2_str = data[num]["Q2"];
        Q3_str = data[num]["Q3"];
        Q4_str = data[num]["Q4"];

      
        UIManager.Q1.text = Q1_str;
        UIManager.Q2_answer = Q2_str;

        //+인 경우 텍스트가 안나와서 직접 넣어서 작성해줌
        if (Q3_str.Length.Equals(1))
        {
            Debug.Log("Q3_str.Length" + Q3_str.Length);
            UIManager.Q3.text = "+" + Q3_str;
            Debug.Log(" UIManager.Q3.text" + UIManager.Q3.text);
        }
        else
        {
            UIManager.Q3.text = Q3_str;
        }
        UIManager.Q4_answer = Q4_str;
    }

}
