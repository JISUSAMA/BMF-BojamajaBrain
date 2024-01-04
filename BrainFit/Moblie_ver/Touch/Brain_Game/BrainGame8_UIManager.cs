
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BrainGame8_UIManager : MonoBehaviour
{
    public Image QuestionImg;
    public Sprite[] QuestionSprite; //20개

    public InputField[] Q_answer_InF;
    public string answer;
    public string[] Q_answer;
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
            if (Q_answer_InF[3].text.Length >= 1)
            {
                Q_answer_InF[3].text = "";
            }
            else if (Q_answer_InF[2].text.Length >= 1)
            {
                Q_answer_InF[2].text = "";
            }
            else if (Q_answer_InF[1].text.Length >= 1)
            {
                Q_answer_InF[1].text = "";
            }
            else if (Q_answer_InF[0].text.Length >= 1)
            {
                Q_answer_InF[0].text = "";
            }
        }
        else if (name.name.Equals("OK"))
        {
            for (int i = 0; i < 4; i++)
            {
                Debug.Log(Q_answer_InF[i].text + "  " + Q_answer[i]);
            }
            //둘다 입력했을 때 
            if (Q_answer_InF[0].text.Length >= 1 && Q_answer_InF[1].text.Length >= 1
                && Q_answer_InF[2].text.Length >= 1 && Q_answer_InF[3].text.Length >= 1)
            {
                if (Q_answer_InF[0].text.Equals(Q_answer[0]) 
                    && Q_answer_InF[1].text.Equals(Q_answer[1])
                   && Q_answer_InF[2].text.Equals(Q_answer[2]) 
                   && Q_answer_InF[3].text.Equals(Q_answer[3]))
                {
                    SetUIGrup.instance.Question_Success();
                    Debug.Log("clear");
                }
                else
                {
                    //SetUIGrup.instance.Question_Fail();
                    SceneSoundCtrl.Instance.GameFailSound();
                    Debug.Log("fail");
                }
            }
          
        }
    }
    //버튼에 붙일 매서드
    public void ResponseButton_click()
    {
        if (Q_answer_InF[0].isFocused == false && Q_answer_InF[0].text.Length < 1)
        {
            EventSystem.current.SetSelectedGameObject(Q_answer_InF[0].gameObject, null);
            Q_answer_InF[0].OnPointerClick(new PointerEventData(EventSystem.current));
            Q_answer_InF[0].text = answer;

        }
        else if (Q_answer_InF[1].isFocused == false && Q_answer_InF[1].text.Length < 1)
        {
            EventSystem.current.SetSelectedGameObject(Q_answer_InF[1].gameObject, null);
            Q_answer_InF[1].OnPointerClick(new PointerEventData(EventSystem.current));
            Q_answer_InF[1].text = answer;
        }
        else if (Q_answer_InF[2].isFocused == false && Q_answer_InF[2].text.Length < 1)
        {
            EventSystem.current.SetSelectedGameObject(Q_answer_InF[2].gameObject, null);
            Q_answer_InF[2].OnPointerClick(new PointerEventData(EventSystem.current));
            Q_answer_InF[2].text = answer;
        }
        else if (Q_answer_InF[3].isFocused == false && Q_answer_InF[3].text.Length < 1)
        {
            EventSystem.current.SetSelectedGameObject(Q_answer_InF[3].gameObject, null);
            Q_answer_InF[3].OnPointerClick(new PointerEventData(EventSystem.current));
            Q_answer_InF[3].text = answer;
        }
    }

}
