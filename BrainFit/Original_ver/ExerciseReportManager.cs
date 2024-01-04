using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseReportManager : MonoBehaviour
{

    public ToggleGroup ReportToggels;
    public Toggle[] ReportToggle;
    public GameObject BrainCan, DementiaCan, RealMotionCan, KinectStandCan, KinectSitCan;
    public GameObject [] brain_list, dementia_list, real_list, kStand_list, kSit_list;

    public Text TotalDay_txt , MonthDay_txt; 
    int Total_training, Month_training;

    public Toggle ReportToggleCurrentSeletion
    {
        get { return ReportToggels.ActiveToggles().FirstOrDefault(); }
    }
    public void Check_toggle()
    {
        if (ReportToggels.ActiveToggles().Any())
        {
            if (ReportToggleCurrentSeletion.name.Equals("BrainToggle")) { OnClick_Toggle_OpenCanvas(true, false, false, false, false); }
            else if (ReportToggleCurrentSeletion.name.Equals("DementiaToggle")) { OnClick_Toggle_OpenCanvas(false, true, false, false, false); }
            else if (ReportToggleCurrentSeletion.name.Equals("RealMotionToggle")) { OnClick_Toggle_OpenCanvas(false, false, true, false, false); }
            else if (ReportToggleCurrentSeletion.name.Equals("KinectStandToggle")) { OnClick_Toggle_OpenCanvas(false, false, false, true, false); }
            else if (ReportToggleCurrentSeletion.name.Equals("KinectSitToggle")) { OnClick_Toggle_OpenCanvas(false, false, false, false, true); }
        }
    }

    void OnClick_Toggle_OpenCanvas(bool brain, bool dementia, bool real, bool Kstand, bool Ksit)
    {
        BrainCan.SetActive(brain);
        DementiaCan.SetActive(dementia);
        RealMotionCan.SetActive(real);
        KinectStandCan.SetActive(Kstand);
        KinectSitCan.SetActive(Ksit);
    }

    //일동안의 데이터
    public void Report_DataSet()
    {
        TotalDay_txt.text = Total_training.ToString();
        MonthDay_txt.text = Month_training.ToString();
    }
}
