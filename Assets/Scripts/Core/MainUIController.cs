using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour {

    public Text height;
    //public Text speed;
    public Text breathing;
    public Text successScore;
    public Text enemiesDestroyed;
    public Text powerupsCollected;

    public Image[] lives;

    public GameObject mainPanel;
    public GameObject successPanel;
    public GameObject gameOverLivesPanel;
    public GameObject gameOverGroundPanel;
    public GameObject peakFlowFailedPanel;

    public GameObject debugPanel1;
    public GameObject debugPanel2;
    public Text debugPanel2Text;

    public Slider timeSlider;

    void Start()
    {
        timeSlider.maxValue = GameController.gameController.gameLength;
    }
	
	void Update ()
    {
        SetHeightText();
        //SetSpeedText();
        SetBreathingText();
        if (GameController.gameController.timerStarted)
        {
            SetTimeSlider();
        }
	}

    void SetHeightText()
    {
        float roundedHeight = Mathf.Round(GameController.playerScreen.currentHeight) * 10;
        height.text = roundedHeight.ToString() + "m";
    }

    void SetBreathingText()
    {
        breathing.text = (Mathf.Round(GameController.playerScreen.currentBreathingEfficiency * 100)).ToString() + "%";
    }

   /* void SetSpeedText()
    {
        float roundedSpeed = Mathf.Round(GameController.playerScreen.rb.velocity.magnitude);
        speed.text = roundedSpeed.ToString() + "mps";
    }*/

    void SetTimeSlider()
    {
        timeSlider.maxValue = GameController.gameController.gameLength;
        timeSlider.value = Time.time - GameController.gameController.startTime;        
    }

    public void SetLivesNumber()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].enabled = false;
        }
        for (int i = 0; i < GameController.lungCharacter.currentLives; i++)
        {
            lives[i].enabled = true;
        }
    }

    public void SetEnemiesDestroyed()
    {
        enemiesDestroyed.text = GameController.gameController.enemiesDestroyed.ToString();
    }

    public void SetPowerupsCollected()
    {
        powerupsCollected.text = GameController.gameController.powerupsCollected.ToString();
    }


    public void SetSuccessPanel()
    {
        mainPanel.gameObject.SetActive(false);
        successScore.text = GameController.gameController.CalculateScore().ToString();     
        successPanel.gameObject.SetActive(true);
    }


    public void SetGameOverPanel(int panel)
    {
        if (panel == 0)
        {
            mainPanel.gameObject.SetActive(false);
            gameOverLivesPanel.gameObject.SetActive(true);
        }
        if (panel == 1)
        {
            mainPanel.gameObject.SetActive(false);
            gameOverGroundPanel.gameObject.SetActive(true);
        }
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

    public void DebugTouchInput()
    {
        debugPanel1.SetActive(true);
    }

    public void DebugPeakFlows(string text)
    {
        debugPanel2.SetActive(true);
        debugPanel2Text.text = text;
    }


}
