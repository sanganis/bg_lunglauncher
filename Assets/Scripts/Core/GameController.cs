using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static PlayerControllerNew playerScreen;
    public static MainUIController mainUIController;

    // A class to keep static references for all major game elements, to easily facilitate referencing between classes

    void Awake ()
    {
        playerScreen = GameObject.Find("PlayerScreen").GetComponent<PlayerControllerNew>();
        mainUIController = GameObject.Find("MainUI").GetComponent<MainUIController>();
	}
	
	
}
