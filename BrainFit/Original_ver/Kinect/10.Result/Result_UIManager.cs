using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Result_UIManager : MonoBehaviour
{
    public ToggleGroup ReportToggels;
    public Toggle[] ReportToggle;
    public GameObject BrainCan, DementiaCan, RealMotionCan;
    public TextMeshProUGUI[] brain_numberm, dementia_numberm, real_numberm; //횟수
    public TextMeshProUGUI[] brain_total, dementia_total, real_total; //총점
    public Slider[] brain_slider, dementia_slider, real_slider; //바
    public Text TotalDay_txt, MonthDay_txt;

    private void Awake()
    {
        Result_DataManager.instance.TapKinectExerciseDataShow("Brain", brain_numberm, brain_total, brain_slider);
        Result_DataManager.instance.Calculate_Total_traing();
        Result_DataManager.instance.Calculate_Month_traing();

    }
    private void Start()
    {
        TotalDay_txt.text = Result_DataManager.instance.Total_count.ToString();
        MonthDay_txt.text = Result_DataManager.instance.Month_count.ToString();
    }
    public Toggle ReportToggleCurrentSeletion
    {
        get { return ReportToggels.ActiveToggles().FirstOrDefault(); }
    }
    public void Check_toggle()
    {
        if (ReportToggels.ActiveToggles().Any())
        {
            if (ReportToggleCurrentSeletion.name.Equals("BrainToggle"))
            {
                OnClick_Toggle_OpenCanvas(true, false, false);
                Result_DataManager.instance.TapKinectExerciseDataShow("Brain", brain_numberm, brain_total, brain_slider);
            }
            else if (ReportToggleCurrentSeletion.name.Equals("DementiaToggle"))
            {
                OnClick_Toggle_OpenCanvas(false, true, false); //Debug.Log("------------------" + name);
                Result_DataManager.instance.TapKinectExerciseDataShow("Dementia", dementia_numberm, dementia_total, dementia_slider);
            }
            else if (ReportToggleCurrentSeletion.name.Equals("RealMotionToggle"))
            {
                OnClick_Toggle_OpenCanvas(false, false, true);// Debug.Log("------------------" + name);
                Result_DataManager.instance.TapKinectExerciseDataShow("Real", real_numberm, real_total, real_slider);
            }

        }
    }

    void OnClick_Toggle_OpenCanvas(bool brain, bool dementia, bool real)
    {
        BrainCan.SetActive(brain);
        DementiaCan.SetActive(dementia);
        RealMotionCan.SetActive(real);
      //  Debug.Log("------------------" + name);
    }

    public void Back_UserExercise()
    {
        SceneManager.LoadScene("5.UserExercise");
    }
}
