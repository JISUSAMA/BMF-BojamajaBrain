using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DementiaGame4_DataManager : MonoBehaviour
{

    public DementiaGame4_UIManager UIManager;

    public int AnswerQuestionCount = 0;
    public string[] Question_Kind_str = { "Pizza","Cake","Donuts","Color","Bar" };
    public string Kind_str;
    public List<int> QuestionNum;
    public int AnswerNumber;

    //2022.11.21 수정 사항

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
        StartCoroutine(_Set_Kind_Data());
        yield return new WaitUntil(() => GameAppManager.instance.playBool == true); //트루가 되면 게임 시작
        SetUIGrup.instance.TimeToScore(25);
        yield return null;
    }

    IEnumerator _Set_Kind_Data()
    {
        int Kind_rand = Random.Range(0, Question_Kind_str.Length);
        Kind_str = Question_Kind_str[Kind_rand].ToString();
        Debug.Log("Kind_rand" + Kind_rand + "Kind_str" + Kind_str);
        int random = Random.Range(0, 10); //문제 10개 중에 한개 _ 종류와 분수의 번호
        AnswerNumber = random;//정답의 번호 
        //출제할 문항 4개를 고름
        while (QuestionNum.Count < 4)
        {
            //리스트가 0이 아니면 랜덤으로 숫자3개를 고름
            if (QuestionNum.Count != 0)
            {
                random = Random.Range(0, 10); 
                if (!QuestionNum.Contains(random))
                {
                    QuestionNum.Add(random);
                }
            }
            else
            {
                //정답 카드 번호 저장
                QuestionNum.Add(random);
            }
            yield return null;
        }
        //번호 섞음
        GameAppManager.instance.GetShuffleList(QuestionNum);

        if (Kind_str.Equals("Pizza"))
        {
            UIManager.QusetionImg.sprite = UIManager.Food_Pizza_img[AnswerNumber];
            for(int i =0; i<QuestionNum.Count; i++)
            {
                Debug.Log("I : " + i);
                UIManager.PreViews[i].sprite = UIManager.Fraction_Pizza_img[QuestionNum[i]];
            }
        }
        else if (Kind_str.Equals("Cake"))
        {
            UIManager.QusetionImg.sprite = UIManager.Food_Cake_img[AnswerNumber];
            for (int i = 0; i < QuestionNum.Count; i++)
            {
                UIManager.PreViews[i].sprite = UIManager.Fraction_Cake_img[QuestionNum[i]];
            }
        }
        else if (Kind_str.Equals("Donuts"))
        {
            UIManager.QusetionImg.sprite = UIManager.Food_Donuts_img[AnswerNumber];
            for (int i = 0; i < QuestionNum.Count; i++)
            {
                UIManager.PreViews[i].sprite = UIManager.Fraction_Donuts_img[QuestionNum[i]];
            }
        }
        else if (Kind_str.Equals("Color"))
        {
            UIManager.QusetionImg.sprite = UIManager.Food_Color_img[AnswerNumber];
            for (int i = 0; i < QuestionNum.Count; i++)
            {
                UIManager.PreViews[i].sprite = UIManager.Fraction_Color_img[QuestionNum[i]];
            }
        }
        else if (Kind_str.Equals("Bar"))
        {
            UIManager.QusetionImg.sprite = UIManager.Food_Bar_img[AnswerNumber];
            for (int i = 0; i < QuestionNum.Count; i++)
            {
                UIManager.PreViews[i].sprite = UIManager.Fraction_Bar_img[QuestionNum[i]];
            }
        }
    
    }
 
}
