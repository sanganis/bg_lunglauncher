using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    private static GameManager _manager = null;
    public static GameManager instance
    {
        get { return _manager; }
    }    
          

	void Awake ()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _manager = this;
        }
        DontDestroyOnLoad(this.gameObject);
        //TurnOnAllTriggers();
    }

    // turns all triggers on. used for testing the game
    void TurnOnAllTriggers()
    {
        ClearAllKeys();
        ToggleTrigger(0);
        ToggleTrigger(1);
        ToggleTrigger(2);
        ToggleTrigger(3);
        ToggleTrigger(4);
        ToggleTrigger(5);
        ToggleTrigger(6);
        ToggleTrigger(7);        
    }

    void ClearAllKeys()
    {
        PlayerPrefs.DeleteAll();
    }
    
    // use to create a key that will be looked for to see if it is a trigger
    public void ToggleTrigger(int triggerNumber)
    {
        switch (triggerNumber)
        {
            case 0:
                if (PlayerPrefs.HasKey("CatTrigger"))
                {
                    PlayerPrefs.DeleteKey("CatTrigger");
                }
                else
                {
                    PlayerPrefs.SetString("CatTrigger", "");
                }
                break;
            case 1:
                if (PlayerPrefs.HasKey("CigaretteTrigger"))
                {
                    PlayerPrefs.DeleteKey("CigaretteTrigger");
                }
                else
                {
                    PlayerPrefs.SetString("CigaretteTrigger", "");
                }
                break;
            case 2:
                if (PlayerPrefs.HasKey("DumbellTrigger"))
                {
                    PlayerPrefs.DeleteKey("DumbellTrigger");
                }
                else
                {
                    PlayerPrefs.SetString("DumbellTrigger", "");
                }
                break;
            case 3:
                if (PlayerPrefs.HasKey("DustTrigger"))
                {
                    PlayerPrefs.DeleteKey("DustTrigger");
                }
                else
                {
                    PlayerPrefs.SetString("DustTrigger", "");
                }
                break;
            case 4:
                if (PlayerPrefs.HasKey("DogTrigger"))
                {
                    PlayerPrefs.DeleteKey("DogTrigger");
                }
                else
                {
                    PlayerPrefs.SetString("DogTrigger", "");
                }
                break;
            case 5:
                if (PlayerPrefs.HasKey("SweatTrigger"))
                {
                    PlayerPrefs.DeleteKey("SweatTrigger");
                }
                else
                {
                    PlayerPrefs.SetString("SweatTrigger", "");
                }
                break;
            case 6:
                if (PlayerPrefs.HasKey("VirusTrigger"))
                {
                    PlayerPrefs.DeleteKey("VirusTrigger");
                }
                else
                {
                    PlayerPrefs.SetString("VirusTrigger", "");
                }
                break;
            case 7:
                if (PlayerPrefs.HasKey("CleanerTrigger"))
                {
                    PlayerPrefs.DeleteKey("CleanerTrigger");
                }
                else
                {
                    PlayerPrefs.SetString("CleanerTrigger", "");
                }
                break;
        }
    }    

}
