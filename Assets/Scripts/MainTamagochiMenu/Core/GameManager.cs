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
        TestMethodForTriggers();
    }

    // turns all triggers on. used for testing the game
    void TestMethodForTriggers()
    {
        //ClearAllKeys();
        SetChildsTriggers(0);
        SetChildsTriggers(1);
        SetChildsTriggers(2);
        SetChildsTriggers(3);
        SetChildsTriggers(4);
        SetChildsTriggers(5);
        SetChildsTriggers(6);
        SetChildsTriggers(7);        
    }

    void ClearAllKeys()
    {
        PlayerPrefs.DeleteAll();
    }
    
    // use to create a key that will be looked for to see if it is a trigger
    public void SetChildsTriggers(int triggerNumber)
    {
        switch (triggerNumber)
        {
            case 0:
                PlayerPrefs.SetString("CatTrigger", "");
                break;
            case 1:
                PlayerPrefs.SetString("CigaretteTrigger", "");
                break;
            case 2:
                PlayerPrefs.SetString("DumbellTrigger", "");
                break;
            case 3:
                PlayerPrefs.SetString("DustTrigger", "");
                break;
            case 4:
                PlayerPrefs.SetString("DogTrigger", "");
                break;
            case 5:
                PlayerPrefs.SetString("SweatTrigger", "");
                break;
            case 6:
                PlayerPrefs.SetString("VirusTrigger", "");
                break;
            case 7:
                PlayerPrefs.SetString("CleanerTrigger", "");
                break;
        }
    }

    void RemoveChildsTrigger(int triggerNumber)
    {
        switch (triggerNumber)
        {
            case 0:
                PlayerPrefs.DeleteKey("CatTrigger");
                break;
            case 1:
                PlayerPrefs.DeleteKey("CigaretteTrigger");
                break;
            case 2:
                PlayerPrefs.DeleteKey("DumbellTrigger");
                break;
            case 3:
                PlayerPrefs.DeleteKey("DustTrigger");
                break;
            case 4:
                PlayerPrefs.DeleteKey("DogTrigger");
                break;
            case 5:
                PlayerPrefs.DeleteKey("SweatTrigger");
                break;
            case 6:
                PlayerPrefs.DeleteKey("VirusTrigger");
                break;
            case 7:
                PlayerPrefs.DeleteKey("CleanerTrigger");
                break;
        }
    }

}
