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
    float peakFlowVariable = 0f;
    float initTime;
    public float verticalLaunchSpeed = 100f;
    public float horizontalLaunchSpeed = 100f;

    public Slider aimStrengthSlider;
    public Transform cannonBarrel;
     

    public float maxAscendSpeed = 30f;
    public float maxDescendSpeed = -20f;

    // updated for calculating score
    [HideInInspector]
    public float currentHeight;
    public float currendDistance;
    
    public AudioClip launchSound;   

    [HideInInspector]
    public bool gameOver;

    Vector3 startPos;


    void Start()
    {
        startPos = transform.position;
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
            currendDistance = Vector3.Distance(startPos, transform.position);
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
            peakFlowVariable = CalculatePeakFlow();
            InvokeRepeating("AngleCannonUp",0,0.01f);            
            Invoke("LaunchPlayer", 1f);
            launchedYet = true;
        }    
    }

    void AngleCannonUp()
    {        
        float barrelAngle = peakFlowVariable * 45;
        if (cannonBarrel.eulerAngles.z < barrelAngle)
        {
            cannonBarrel.eulerAngles = new Vector3(cannonBarrel.eulerAngles.x, cannonBarrel.eulerAngles.y, cannonBarrel.eulerAngles.z + 1f);            
        }
        if(aimStrengthSlider.value < peakFlowVariable)
        {
            aimStrengthSlider.value += 0.01f;
        }        
    }

    void LaunchPlayer()
    {
        CancelInvoke("AngleCannonUp");
        float vertLaunchSpeed = peakFlowVariable * verticalLaunchSpeed;
        float horLaunchSpeed = peakFlowVariable * horizontalLaunchSpeed;
        UnlockScreenMovement();
        rb.velocity = new Vector2(horLaunchSpeed, vertLaunchSpeed);
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
