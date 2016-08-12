using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour {

    public Text height;
    public Text speed;
    public Text breathing;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        SetHeightText();
        SetSpeedText();
        SetBreathingText();
	}

    void SetHeightText()
    {
        float roundedHeight = Mathf.Round(GameController.playerScreen.currentHeight * 10f) / 10f;
        height.text = roundedHeight.ToString();
    }

    void SetBreathingText()
    {
        breathing.text = ((Mathf.Round(GameController.playerScreen.currentBreathingEfficiency * 1000) / 10).ToString() + "%");
    }

    void SetSpeedText()
    {
        float roundedSpeed = Mathf.Round(GameController.playerScreen.rb.velocity.magnitude * 10f) / 10f;
        speed.text = roundedSpeed.ToString();
    }


}
