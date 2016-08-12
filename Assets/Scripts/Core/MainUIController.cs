using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour {

    public Text height;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        height.text = GameController.player.currentHeight.ToString();
	}
}
