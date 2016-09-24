using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PeakFlowMainUIController : MonoBehaviour
{

    public Text height;
    public Text speed;
    public Text distance;    
    public Text successScore;

    public GameObject mainPanel;
    public GameObject successPanel;
    public GameObject failurePanel;

    public Slider timeSlider;
       

    void Update()
    {
        SetHeightText();
        SetSpeedText();
        SetDistanceText();    
        if (PeakFlowGameController.playerScreen.launchedYet)
        {
            SetTimeSlider();
        }
    }

    void SetHeightText()
    {
        float roundedHeight = Mathf.Round(PeakFlowGameController.playerScreen.currentHeight);
        height.text = roundedHeight.ToString() + "m";
    }

    void SetSpeedText()
    {
        float roundedSpeed = Mathf.Round(PeakFlowGameController.playerScreen.rb.velocity.magnitude);
        speed.text = roundedSpeed.ToString() + "mps";
    }

    void SetDistanceText()
    {
        float roundedDistance = Mathf.Round(PeakFlowGameController.playerScreen.currendDistance);
        distance.text = roundedDistance.ToString() + "m";
    }

    void SetTimeSlider()
    {
        timeSlider.value = Time.time;
    }



    public void SetSuccessPanel()
    {
        mainPanel.gameObject.SetActive(false);
        float roundedDistance = Mathf.Round(PeakFlowGameController.playerScreen.currendDistance);
        successScore.text = roundedDistance.ToString();
        successPanel.gameObject.SetActive(true);
    }


    public void SetFailurePanel()
    {
        mainPanel.gameObject.SetActive(false);
        failurePanel.gameObject.SetActive(true);
    }
       

    public void Quit()
    {
        Application.Quit();
    }


}
