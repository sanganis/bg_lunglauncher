using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour {

    public Text height;
    public Text speed;
    public Text breathing;
    
	
	void Update ()
    {
        SetHeightText();
        SetSpeedText();
        SetBreathingText();
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


}
