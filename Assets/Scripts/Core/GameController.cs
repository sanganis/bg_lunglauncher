using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static PlayerScreenController playerScreen;    
    public static LungCharacterController lungCharacter;
    public static MainUIController mainUIController;
    public static MusicController musicController;    

    // A class to keep static references for all major game elements, to easily facilitate referencing between classes

    void Awake ()
    {
        playerScreen = GameObject.Find("PlayerScreen").GetComponent<PlayerScreenController>();
        lungCharacter = GameObject.Find("LungCharacter").GetComponent<LungCharacterController>();
        mainUIController = GameObject.Find("MainUI").GetComponent<MainUIController>();
        musicController = GetComponent<MusicController>();
	}
	
	
}
