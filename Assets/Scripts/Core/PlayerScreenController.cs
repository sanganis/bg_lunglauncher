using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScreenController : MonoBehaviour {
    
    [HideInInspector]
    public Rigidbody2D rb;
    public AudioSource source; 

    // launching variables
    [HideInInspector]
    public bool launchedYet = false;
    bool clickedYet = false;
    float launchSpeedVariable;
    float initTime;
    public float verticalLaunchSpeed = 100f;
    public float horizontalLaunchSpeed = 18f;

    public Slider aimStrengthSlider;
    public Transform cannonBarrel;

    // once launched, how much force is applied over how much time at best breathing rate
    float refreshTime = 0.1f;
    float climbForce = 2f;

    public float maxAscendSpeed = 30f;
    public float maxDescendSpeed = -20f;

    // updated for calculating score
    [HideInInspector]
    public float currentHeight;

    // ultimately to be determined by PEP
    
    public float currentBreathingEfficiency = 0.7f;

    public AudioClip launchSound;
    public AudioClip hitEnemySound;
    public AudioClip powerupSound;

    
    // peak flow variables
    public float minimumPeakFlow = 0.3f;
    int peakFlowsCompleted = 0;
    public Slider[] peakFlowSliders;
    float[] peakFlowResults = new float[3];
    // gets set by a combination of all 3 peak flow results
    float peakFlowMultiplier;

    float peakFlowInputDelay = 1f;
    float lastPeakFlow;


    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();            
    }

    void Update()
    {
        if (!launchedYet)
        {
            CheckForPeakFlowInput();
        }
        else
        {
            ControlClimbRate();
            currentHeight = transform.position.y;            
        }
        
        SetCurrentBrathingEfficiency();        
    }

    // stops the player from climbing faster than a desired rate
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
    IEnumerator AdjustClimbRateToBreathing()
    {
        yield return new WaitForSeconds(refreshTime);

        Vector2 verticalSpeed = new Vector2(0, climbForce * ReturnBreathingEfficiency());
        rb.AddForce(verticalSpeed, ForceMode2D.Impulse);
                
        StartCoroutine("AdjustClimbRateToBreathing");
    }



    void CheckForPeakFlowInput()
    {
        // needs delay

        if ((Input.GetKeyDown(KeyCode.Space)) || Input.GetButtonDown("Fire1") && Time.time > peakFlowInputDelay + lastPeakFlow)
        {
            float peakFlow = PerformPeakFlowTest();
            if (peakFlow > minimumPeakFlow)
            {
                PlayerStatisticsManager.Instance.AddPeakFlowResult(peakFlow);
                peakFlowSliders[peakFlowsCompleted].value = peakFlow;
                peakFlowResults[peakFlowsCompleted] = peakFlow;
                peakFlowsCompleted++;
            }
            else
            {                
                GameController.mainUIController.NotifyPeakFlowFailure();           
            }
            lastPeakFlow = Time.time;
        }
        if(peakFlowsCompleted == 3)
        {
            peakFlowMultiplier = peakFlowResults[0] + peakFlowResults[1] + peakFlowResults[2];
            InvokeRepeating("AngleCannonUp", 0, 0.01f);
            Invoke("LaunchPlayer", 1f);
            launchedYet = true;
        }
    }

    // random result to represent the peak flow results we will get
    float PerformPeakFlowTest()
    {
        return Random.Range(0f, 1f);
    }

 

    void AngleCannonUp()
    {       
        float barrelAngle = peakFlowMultiplier * 25;        
        if (cannonBarrel.eulerAngles.z < barrelAngle)
        {
            cannonBarrel.eulerAngles = new Vector3(cannonBarrel.eulerAngles.x, cannonBarrel.eulerAngles.y, cannonBarrel.eulerAngles.z + 1f);
        }       
    }

    void LaunchPlayer()
    {
        CancelInvoke("AngleCannonUp"); 
        float vertLaunchSpeed = peakFlowMultiplier * verticalLaunchSpeed;
        float horLaunchSpeed = peakFlowMultiplier * horizontalLaunchSpeed;
        UnlockScreenMovement();
        rb.velocity = new Vector2(horLaunchSpeed, vertLaunchSpeed);
        StartCoroutine("AdjustClimbRateToBreathing");
        source.PlayOneShot(launchSound);
        GameController.gameController.SetGameTime();
    }

    // a temporary method to simulate breathing being better or worse
    void SetCurrentBrathingEfficiency()
    {
        if (Input.GetKeyDown("up"))
        {
            BreathingEfficiencyUp();
        }
        if (Input.GetKeyDown("down"))
        {
            BreathingEfficiencyDown();
        }
    }

    // for debugging different breathing efficiency's
    public void BreathingEfficiencyUp()
    {
        currentBreathingEfficiency += 0.1f;
        if (currentBreathingEfficiency > 1f)
        {
            currentBreathingEfficiency = 1f;
        }
    }
    public void BreathingEfficiencyDown()
    {
        currentBreathingEfficiency -= 0.1f;
        if (currentBreathingEfficiency < 0f)
        {
            currentBreathingEfficiency = 0f;
        }
    }
    
    // returns the current breathing efficiency
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

   

    public void CallPowerupInvincible(float duration = 5f)
    {
        StartCoroutine(PowerUpInvincible(duration));        
    }
    IEnumerator PowerUpInvincible(float duration)
    {
        //lungCharacter.invincible = true;
        yield return new WaitForSeconds(duration);
        //lungCharacter.invincible = false;
    }
    
    

    public void PlayEnemyHitSound()
    {
        source.PlayOneShot(hitEnemySound);
    }
    public void PlayPowerupSound()
    {
        source.PlayOneShot(powerupSound);
    }

}
