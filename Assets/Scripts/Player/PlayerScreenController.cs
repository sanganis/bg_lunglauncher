using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScreenController : MonoBehaviour {

    // the player object, which moves independently from the PlayerScreen    
    public PlayerColliderMovement playerObject;

    // how many seconds the game should last for
    public float gameLength = 300f;

    [HideInInspector]
    public Rigidbody2D rb;    

    // launching variables
    public bool launchedYet = false;
    bool clickedYet = false;
    float launchSpeedVariable = 0f;
    float initTime;
    public float maxLaunchSpeed = 100f;
    public float horizontalLaunchSpeed = 18f;        
    Slider aimStrengthSlider;

    // once launched, how much force is applied over how much time at best breathing rate
    float refreshTime = 0.1f;
    float climbForce = 2f;

    public float maxAscendSpeed = 30f;
    public float maxDescendSpeed = -10f;

    // updated for calculating score
    public float currentHeight;

    // ultimately to be determined by PEP
    public float currentBreathingEfficiency = 1f;

    [HideInInspector]
    public bool gameOver;

    void Start()
    {
        aimStrengthSlider = GameObject.FindGameObjectWithTag("Power Slider").GetComponent<Slider>();
        rb = GetComponent<Rigidbody2D>();            
    }

    void Update()
    {
        if (!launchedYet)
        {
            CheckLaunch();
        }
        else
        {
            ControlClimbRate();
            currentHeight = transform.position.y;            
        }
        if (!gameOver)
        {
            if(Time.time > gameLength)
            {
                GameOverSuccess();
            }
        }   
        SetCurrentBrathingEfficiency();        
    }

    void ControlClimbRate()
    {
        if (rb.velocity.y > maxAscendSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxAscendSpeed);
        }
        if (rb.velocity.y < maxDescendSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxDescendSpeed);
        }
    }

    // multiplies the force pushing the player up with the current breathing efficiency
    IEnumerator AdjustClimbToRhythem()
    {
        yield return new WaitForSeconds(refreshTime);

        Vector2 verticalSpeed = new Vector2(0, climbForce * ReturnBreathingEfficiency());
        rb.AddForce(verticalSpeed, ForceMode2D.Impulse);
                
        StartCoroutine("AdjustClimbToRhythem");
    }
    

    void CheckLaunch()
    {
        if (!clickedYet && (Input.GetKeyDown(KeyCode.Space)) || Input.GetButtonDown("Fire1"))
        {
            clickedYet = true;
            initTime = Time.time;
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
        {
            launchSpeedVariable = Mathf.Abs(Mathf.Sin(Time.time - initTime));
            aimStrengthSlider.value = launchSpeedVariable;            
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1"))
        {
            launchedYet = true;
            float verticalLaunchSpeed = launchSpeedVariable * maxLaunchSpeed;
            UnlockScreenMovement();
            rb.velocity = new Vector2(horizontalLaunchSpeed, verticalLaunchSpeed);
            
            StartCoroutine("AdjustClimbToRhythem");
        }
    }

       // a temporary method to simulate breathing being better or worse
    void SetCurrentBrathingEfficiency()
    {
        if (Input.GetKeyDown("up"))
        {
            currentBreathingEfficiency += 0.1f;
            if(currentBreathingEfficiency > 1f)
            {
                currentBreathingEfficiency = 1f;
            }
        }
        if (Input.GetKeyDown("down"))
        {
            currentBreathingEfficiency -= 0.1f;
            if (currentBreathingEfficiency < 0f)
            {
                currentBreathingEfficiency = 0f;
            }
        }
    }
    
    float ReturnBreathingEfficiency()
    {                
        return currentBreathingEfficiency;
    }    

    // stops the PlayerScreen from being able to move
    public void LockScreenMovement()
    {
        rb.isKinematic = true;
    }
    public void UnlockScreenMovement()
    {
        rb.isKinematic = false;
    }

    public void KnockPlayerDown(float ammount)
    {
        Vector2 dir = new Vector2(rb.velocity.x, -ammount);
        rb.velocity = dir;        
    }
    

    public void GameOverSuccess()
    {
        GameController.mainUIController.SetSuccessPanel();
        playerObject.LockPlayerMovement();
        gameOver = true;
        InvokeRepeating("KillAllEnemies", 0, 0.1f);
    }

    public void GameOverFailure()
    {
        GameController.mainUIController.SetFailurePanel();
        playerObject.LockPlayerMovement();
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
            currentEnemies[i].DestroyEnemey();
        }
    }

}
