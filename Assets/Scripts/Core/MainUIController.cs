using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour {

    public Text height;
    public Text speed;
    public Text breathing;
    public Text successScore;

    public GameObject mainPanel;
    public GameObject successPanel;
    public GameObject failurePanel;
    public GameObject peakFlowFailedPanel;

    public Slider timeSlider;

    void Start()
    {
        timeSlider.maxValue = GameController.playerScreen.gameLength;
    }
	
	void Update ()
    {
        SetHeightText();
        SetSpeedText();
        SetBreathingText();
        if (GameController.playerScreen.launchedYet)
        {
            SetTimeSlider();
        }
	}

    void SetHeightText()
    {
        float roundedHeight = Mathf.Round(GameController.playerScreen.currentHeight);
        height.text = roundedHeight.ToString() + "m";
    }

    void SetBreathingText()
    {
        breathing.text = (Mathf.Round(GameController.playerScreen.currentBreathingEfficiency * 100)).ToString() + "%";
    }

    void SetSpeedText()
    {
        float roundedSpeed = Mathf.Round(GameController.playerScreen.rb.velocity.magnitude);
        speed.text = roundedSpeed.ToString() + "mps";
    }

    void SetTimeSlider()
    {
        timeSlider.value = Time.time;        
    }



    public void SetSuccessPanel()
    {
        mainPanel.gameObject.SetActive(false);
        float roundedHeight = Mathf.Round(GameController.playerScreen.currentHeight);
        successScore.text = roundedHeight.ToString();     
        successPanel.gameObject.SetActive(true);
    }


    public void SetFailurePanel()
    {
        mainPanel.gameObject.SetActive(false);
        failurePanel.gameObject.SetActive(true);
    }

    public void NotifyPeakFlowFailure()
    {
        StartCoroutine("FlashPeakFlowFailedPanel");
    }
    IEnumerator FlashPeakFlowFailedPanel()
    {
        peakFlowFailedPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        peakFlowFailedPanel.SetActive(false);             
    }


    public void BreathingEfficiencyUp()
    {
        GameController.playerScreen.BreathingEfficiencyUp();
    }

    public void BreathingEfficiencyDown()
    {
        GameController.playerScreen.BreathingEfficiencyDown();
    }

    public void Quit()
    {
        Application.Quit();
    }


}
