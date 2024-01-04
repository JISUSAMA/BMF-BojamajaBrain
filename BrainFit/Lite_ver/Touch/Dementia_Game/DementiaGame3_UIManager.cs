using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DementiaGame3_UIManager : MonoBehaviour
{
    [Header("UI")]
    public Image ObjectImg;
    public Sprite[] ObjectSpriteKinds;
    public Text[] ObjectText;

    public void OnClick_AnswerBtn(int btnNum)
    {
        //Á¤´ä ¸ÂÃã
        if (ObjectText[btnNum].text.Equals(DementiaGame3_DataManager.instance.QusetionStr))
        {
            SetUIGrup.instance.Question_Success();
        }
        else
        {
            SceneSoundCtrl.Instance.GameFailSound();
        }
    }
}
