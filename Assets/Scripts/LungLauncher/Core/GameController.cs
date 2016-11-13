using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController gameController;
    public static PlayerScreenController playerScreen;    
    public static LungCharacterController lungCharacter;
    public static MainUIController mainUIController;        

    // score variables
    public int enemiesDestroyed;
    public int powerupsCollected;

    // core game variables
    [HideInInspector]
    public bool gameOver;
    // how many seconds the game should last for
    public float gameLength = 300f;

    // what time the player completed peak flows and launched
    [HideInInspector]
    public float startTime;
    [HideInInspector]
    public bool timerStarted;


    // A class to keep static references for all major game elements, to easily facilitate referencing between classes

    void Awake ()
    {
        gameController = GetComponent<GameController>();
        playerScreen = GameObject.Find("PlayerScreen").GetComponent<PlayerScreenController>();
        lungCharacter = GameObject.Find("LungCharacter").GetComponent<LungCharacterController>();
        mainUIController = GameObject.Find("MainUI").GetComponent<MainUIController>();                
	}

    void Update()
    {
        if (!gameOver)
        {
            if (timerStarted && Time.time - startTime > gameLength)
            {
                GameOverSuccess();
            }
        }
    }

    public void SetGameTime()
    {
        startTime = Time.time;
        timerStarted = true;
    }


    public int CalculateScore()
    {
        int score;        
        score = Mathf.RoundToInt(playerScreen.currentHeight);
        score += enemiesDestroyed * 10;
        score += powerupsCollected * 10;
        score += lungCharacter.currentLives * 10;
        return score;
    }

    public void GameOverSuccess()
    {
        mainUIController.SetSuccessPanel();
        MusicController.instance.PlayVictoryJingle();        
        gameOver = true;
        InvokeRepeating("KillAllEnemies", 0, 0.1f);
    }

    public void GameOverHitGround()
    {
        mainUIController.SetGameOverPanel(1);
        MusicController.instance.PlayFailureJingle();        
        gameOver = true;
        InvokeRepeating("KillAllEnemies", 0, 0.1f);
    }
    public void GameOverOutOfLives()
    {
        mainUIController.SetGameOverPanel(0);
        MusicController.instance.PlayFailureJingle();        
        gameOver = true;
        InvokeRepeating("KillAllEnemies", 0, 0.1f);
    }


    // for getting rid of existing enemies, for instance when the game is over
    public void KillAllEnemies()
    {
        EnemyBaseController[] currentEnemies;
        currentEnemies = Object.FindObjectsOfType<EnemyBaseController>();
        for (int i = 0; i < currentEnemies.Length; i++)
        {
            currentEnemies[i].DestroyEnemy(false);
        }
        PowerupBaseController[] currentPowerups;
        currentPowerups = Object.FindObjectsOfType<PowerupBaseController>();
        for (int i = 0; i < currentPowerups.Length; i++)
        {
            currentPowerups[i].DestroyPowerup(false);
        }
    }

}
