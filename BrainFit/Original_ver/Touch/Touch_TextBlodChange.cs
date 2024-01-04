using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touch_TextBlodChange : MonoBehaviour
{
    public Text[] TextGrup;
    public string SetStyle; //Bold, Normal
    private void Start()
    {
        ChangeText_FontStyle();
    }
    void ChangeText_FontStyle()
    {
        SetStyle = "Bold";
        if (SetStyle.Equals("Bold"))
        {
            for (int i = 0; i<TextGrup.Length; i++)
            {
                TextGrup[i].fontStyle = FontStyle.Bold;
            }

        }

    }
}
