
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DementiaGame8_UIManager : MonoBehaviour
{
    public Text Q1, Q2, Q3, Q4;
    public string Q2_answer, Q4_answer;
    public InputField[] Q_answer_InF;

    public string answer;
    public void SetValue(GameObject name)
    {
        answer = "";
        if (name.name.Equals("1")) { answer += "1"; }
        else if (name.name.Equals("2")) { answer += "2"; }
        else if (name.name.Equals("3")) { answer += "3"; }
        else if (name.name.Equals("4")) { answer += "4"; }
        else if (name.name.Equals("5")) { answer += "5"; }
        else if (name.name.Equals("6")) { answer += "6"; }
        else if (name.name.Equals("7")) { answer += "7"; }
        else if (name.name.Equals("8")) { answer += "8"; }
        else if (name.name.Equals("9")) { answer += "9"; }
        else if (name.name.Equals("0")) { answer += "0"; }
        else if (name.name.Equals("Back"))
        {
            if (Q_answer_InF[1].text.Length >= 1)
            {
                Q_answer_InF[1].text = "";
            }
            else
            {
                if (Q_answer_InF[0].text.Length >= 1)
                {
                    Q_answer_InF[0].text = "";
                }
            }
        }
        else if (name.name.Equals("OK"))
        {
            //둘다 입력했을 때 
            if (Q_answer_InF[0].text.Length >= 1 && Q_answer_InF[1].text.Length >= 1)
            {
                if(Q_answer_InF[0].text.Equals(Q2_answer) && Q_answer_InF[1].text.Equals(Q4_answer))
                {
                    Game_changeColor("clear");
                    DementiaGame8_DataManager.instance.TimeManager_sc.Question_Success();
                }
                else
                {
                    Game_changeColor("fail");
                    DementiaGame8_DataManager.instance.TimeManager_sc.Question_Fail();
                }
            }

        }
    }

    //버튼에 붙일 매서드
    public void ResponseButton_click()
    {
        if (Q_answer_InF[0].isFocused == false && Q_answer_InF[0].text.Length<1)
        {
            EventSystem.current.SetSelectedGameObject(Q_answer_InF[0].gameObject, null);
            Q_answer_InF[0].OnPointerClick(new PointerEventData(EventSystem.current));
            Q_answer_InF[0].text = answer;
            
        }
        else if(Q_answer_InF[1].isFocused == false && Q_answer_InF[1].text.Length < 1)
        {
            EventSystem.current.SetSelectedGameObject(Q_answer_InF[1].gameObject, null);
            Q_answer_InF[1].OnPointerClick(new PointerEventData(EventSystem.current));
            Q_answer_InF[1].text = answer;
        }
        
    }
    Color newColor;
    string newColor_code;
    void Game_changeColor(string state)
    {
        if (state.Equals("clear"))
        {
            //푸른색
            newColor_code = "#17B983";
            if (ColorUtility.TryParseHtmlString(newColor_code, out newColor))
            {
                Q2.color = newColor;
                Q4.color = newColor;
            }
        }
        else
        {
            //붉은색
            newColor_code = "#FD6C6C";
            if (ColorUtility.TryParseHtmlString(newColor_code, out newColor))
            {
                Q2.color = newColor;
                Q4.color = newColor;
            }
        }
    
    }
}
