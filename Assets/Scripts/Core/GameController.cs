using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static PlayerControllerNew player;
    public static MainUIController mainUIController;

    // A class to keep static references for all major game elements, to easily facilitate referencing between classes

    void Awake ()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControllerNew>();
        mainUIController = GameObject.Find("MainUI").GetComponent<MainUIController>();
	}
	
	
}
