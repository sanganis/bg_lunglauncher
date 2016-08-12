using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static PlayerScreenController playerScreen;
    public static MainUIController mainUIController;

    // A class to keep static references for all major game elements, to easily facilitate referencing between classes

    void Awake ()
    {
        playerScreen = GameObject.Find("PlayerScreen").GetComponent<PlayerScreenController>();
        mainUIController = GameObject.Find("MainUI").GetComponent<MainUIController>();
	}
	
	
}
