
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BrainGame1_UIManager : MonoBehaviour
{
    public Button[] Piece_btn;
    public Sprite[] Piece_sprite;
    public Text[] Piece_btn_text;
    public TextMeshProUGUI QuestionWord_text;
    public Text FindWord_text;
    public int Click_num = -1;


    public void OnClick_PieceBtn(Text Piece_btn_text)
    {
        Click_num += 1;
        AnswerQusetion(Piece_btn_text.text);
    }
    
    public void AnswerQusetion(string Piece_btn_text)
    {
        if(QuestionWord_text.text.Length>= FindWord_text.text.Length)
        {
            //FindWord_text.text += Piece_btn_text;
            string Pice_temp = FindWord_text.text + Piece_btn_text;
            if (Pice_temp.Equals(QuestionWord_text.text.Substring(0, Click_num + 1)))
            {
                FindWord_text.text += Piece_btn_text;
                GameObject currentBtn = EventSystem.current.currentSelectedGameObject;
                currentBtn.GetComponent<Image>().sprite = Piece_sprite[1];
                // Debug.Log("FindWord_text :" + FindWord_text.text + "QuestionWord_text :" + QuestionWord_text.text.Substring(0, Click_num + 1));
                if (QuestionWord_text.text.Equals(FindWord_text.text))
                {
                    SetUIGrup.instance.Question_Success();
                }
            }
            else
            {
                //Debug.Log("FindWord_text 1 :" + FindWord_text.text + "QuestionWord_text 1:" + QuestionWord_text.text.Substring(0, Click_num + 1));
                Click_num -= 1;
                //BrainGame1_DataManager.instance.TimerManager_sc.Question_Fail();
            }
        }
       
    }
   
}

