using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame4_DataManager : MonoBehaviour
{
    public Touch_TimerManager TimerManager_sc;
    public DementiaGame4_UIManager UIManager;

    public int AnswerQuestionCount = 0;
    public string[] Question_Kind_str = { "Pizza","Cake","Donuts","Color","Bar" };
    public string Kind_str;
    public List<int> QuestionNum;

    public List<int> Fraction_int;
    public List<int> Food_int;
    //public List<int> FoodShuffle_int;
    
    public static DementiaGame4_DataManager instance { get; private set; }
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
        DementiaGame4_play();
        yield return null;
    }
    public void DementiaGame4_play()
    {
        StopCoroutine("_DementiaGame4_play");
        StartCoroutine("_DementiaGame4_play");
    }
    IEnumerator _DementiaGame4_play()
    {
        Set_Kind_Data();
        TimerManager_sc.FindWord_sec_Timer(20, 10, 5);
        UIManager.DrawLine_Start();
        yield return null;
    }

    void Set_Kind_Data()
    {
     
        int Kind_rand = Random.Range(0, Question_Kind_str.Length);
        Kind_str = Question_Kind_str[Kind_rand].ToString();
        Debug.Log("Kind_rand" + Kind_rand + "Kind_str" + Kind_str);
        if (Kind_str.Equals("Pizza")) { Set_Kind_Img(UIManager.Fraction_kind1_img.Length); }
        else if (Kind_str.Equals("Cake")) { Set_Kind_Img(UIManager.Fraction_kind2_img.Length); }
        else if (Kind_str.Equals("Donuts")) { Set_Kind_Img(UIManager.Fraction_kind3_img.Length); }
        else if (Kind_str.Equals("Color")) { Set_Kind_Img(UIManager.Fraction_kind4_img.Length); }
        else if (Kind_str.Equals("Bar")) { Set_Kind_Img(UIManager.Fraction_kind5_img.Length); }

   
    }
    void Set_Kind_Img(int listLength)
    {
        Debug.Log("listLength" + listLength);
        StopCoroutine("_Set_Kind_Img");
        StartCoroutine("_Set_Kind_Img", listLength);
    }
    IEnumerator _Set_Kind_Img(int listLength)
    {
        int random;
        //출제할 문항 4개를 고름
        while (Fraction_int.Count < 5)
        {
            random= Random.Range(0, listLength);
            if (Fraction_int.Count!=0)
            {
                Fraction_int.Add(random);
                Food_int.Add(random);
            }
            else
            {
                Fraction_int.Add(random);
                Food_int.Add(random);
            }
            Debug.Log("random" + random);
            yield return null;
        }

        GameAppManager.instance.GetShuffleList(Food_int);
        //UI 이미지 적용
        for(int i =0; i<5; i++)
        {
            if (Kind_str.Equals("Pizza"))
            {
                UIManager.StartGrup_img[i].sprite = UIManager.Fraction_kind1_img[Fraction_int[i]];
                UIManager.EndGrup_img[i].sprite = UIManager.Food_kind1_img[Food_int[i]];
            }
            else if (Kind_str.Equals("Cake")) 
            { 
                UIManager.StartGrup_img[i].sprite = UIManager.Fraction_kind2_img[Fraction_int[i]]; 
                UIManager.EndGrup_img[i].sprite = UIManager.Food_kind2_img[Food_int[i]];
            }
            else if (Kind_str.Equals("Donuts")) 
            {
                UIManager.StartGrup_img[i].sprite = UIManager.Fraction_kind3_img[Fraction_int[i]];
                UIManager.EndGrup_img[i].sprite = UIManager.Food_kind3_img[Food_int[i]];
            }
            else if (Kind_str.Equals("Color")) 
            { 
                UIManager.StartGrup_img[i].sprite = UIManager.Fraction_kind4_img[Fraction_int[i]];
                UIManager.EndGrup_img[i].sprite = UIManager.Food_kind4_img[Food_int[i]];
            }
            else if (Kind_str.Equals("Bar")) 
            { 
                UIManager.StartGrup_img[i].sprite = UIManager.Fraction_kind5_img[Fraction_int[i]];
                UIManager.EndGrup_img[i].sprite = UIManager.Food_kind5_img[Food_int[i]];
            }

        }
        yield return null;
    }

}
