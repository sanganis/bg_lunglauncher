using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PeakFlowPlayerController : MonoBehaviour {

    // the player object, which moves independently from the PlayerScreen    
    public PeakFlowPlayerObjectController playerObject;
      

    [HideInInspector]
    public Rigidbody2D rb;
    public AudioSource source;

    // launching variables
    [HideInInspector]
    public bool launchedYet = false;
    bool clickedYet = false;
    float launchSpeedVariable = 0f;
    float initTime;
    public float maxLaunchSpeed = 100f;
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
    
    public AudioClip launchSound;   

    [HideInInspector]
    public bool gameOver;


    void Start()
    {
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
            currentHeight = transform.position.y;
        }        
    }

    public float CalculatePeakFlow()
    {
        // this is where info from the flow meter will come through
        return 0.8f;
    }
    public bool PeakFlowInitiated()
    {
        // this will be set positive when the breath is exhaled
        return true;
    }
    

    void CheckLaunch()
    {
        if (!clickedYet && (Input.GetKeyDown(KeyCode.Space)) || Input.GetButtonDown("Fire1"))
        {
            clickedYet = true;
            initTime = Time.time;
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1") && !launchedYet)
        {
            AngleCannonUp();
            Invoke("LaunchPlayer", 1f);
            launchedYet = true;
        }    
    }

    void AngleCannonUp()
    {
        launchSpeedVariable = CalculatePeakFlow();
        aimStrengthSlider.value = launchSpeedVariable;
        float barrelAngle = launchSpeedVariable * 45;
        cannonBarrel.eulerAngles = new Vector3(0, 0, barrelAngle);
    }

    void LaunchPlayer()
    {
        
        float verticalLaunchSpeed = launchSpeedVariable * maxLaunchSpeed;
        UnlockScreenMovement();
        rb.velocity = new Vector2(horizontalLaunchSpeed, verticalLaunchSpeed);
        source.PlayOneShot(launchSound);
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

    public void BumpPlayerUp(float ammount)
    {
        Vector2 dir = new Vector2(rb.velocity.x, rb.velocity.y + ammount);
        rb.velocity = dir;
    }

    public void KnockPlayerDown(float ammount)
    {
        Vector2 dir = new Vector2(rb.velocity.x, rb.velocity.y - ammount);
        rb.velocity = dir;
    }
       

    public void GameOverSuccess()
    {
        PeakFlowGameController.mainUIController.SetSuccessPanel();
        PeakFlowGameController.musicController.PlayVictoryJingle();
        playerObject.LockPlayerMovement();
        gameOver = true;        
    }
    

}
