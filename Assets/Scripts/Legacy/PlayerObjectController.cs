using UnityEngine;
using System.Collections;

public class PlayerObjectController : MonoBehaviour {

    // movement variables
    public float defaultMoveSpeed = 2f;
    [HideInInspector]
    public float moveSpeed = 2f;
    public float stayInsideScreenBy = 10f;

    // components
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;    
    public PlayerScreenController playerScreen;
    public Camera mainCamera;
    public RectTransform UITransform;

    // materials for making the sprite flash when damaged
    public Material normalMaterial;
    // flash material is changed according to what the player encounteres
    Material flashMaterial;
    public Material enemyFlashMaterial;
    public Material powerUpFlashMaterial;

    // util veriables
    bool canMove = true;
    float justBeenHit;
    float hitRecovery = 1f;
    [HideInInspector]
    public bool invincible;

    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void Update () {
        if (playerScreen.launchedYet)
        {
            if (Input.GetMouseButton(0) && canMove)
            {
                Move();
                KeepPlayerWithinScreen();
            }
            else
            {
                rb.velocity = new Vector2(playerScreen.rb.velocity.x, playerScreen.rb.velocity.y);
            }
        }        
    }

    void Move()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (target - transform.position).normalized;           
        rb.velocity = new Vector2((dir.x * moveSpeed) + playerScreen.rb.velocity.x, (dir.y * moveSpeed) + playerScreen.rb.velocity.y);        
    }

    // calculates if the player has gone beyond the boundaries of the current screen (so works on any export setting)
    // then moves the player back to the boundary
    void KeepPlayerWithinScreen()
    {
        Vector2 playerScreenPos = mainCamera.WorldToScreenPoint(transform.position);
        Vector3 playerAdjustedPos = playerScreenPos;           

        float screenWidth = UITransform.sizeDelta.x;
        float screenHeight = UITransform.sizeDelta.y;

        if (playerScreenPos.x > screenWidth)
        {            
            playerAdjustedPos = new Vector2(screenWidth - stayInsideScreenBy, playerAdjustedPos.y);
        }
        if (playerScreenPos.x < 0)
        {
            playerAdjustedPos = new Vector2(stayInsideScreenBy, playerAdjustedPos.y);
        }
        if (playerScreenPos.y > screenHeight)
        {
            playerAdjustedPos = new Vector2(playerAdjustedPos.x, screenHeight - stayInsideScreenBy);
        }
        if (playerScreenPos.y < 0)
        {
            playerAdjustedPos = new Vector2(playerAdjustedPos.x, stayInsideScreenBy);
        }        
        transform.position = mainCamera.ScreenToWorldPoint(playerAdjustedPos);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            playerScreen.LockScreenMovement();
            LockPlayerMovement();
            playerScreen.GameOverHitGround();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Enemy")
        {
            EnemyBaseController enemy = coll.gameObject.GetComponent<EnemyBaseController>();
            if (!invincible)
            {                
                if (enemy.enemyType == EnemyBaseController.EnemyType.BALLOONCAT)
                {
                    PlayerFalls(10);
                    enemy.DestroyEnemy();
                }
                if (enemy.enemyType == EnemyBaseController.EnemyType.DUMBELL)
                {
                    PlayerFalls(15);
                    enemy.DestroyEnemy();
                }
                if (enemy.enemyType == EnemyBaseController.EnemyType.ROCKETDOG)
                {
                    PlayerFalls(10);
                    enemy.DestroyEnemy();
                }
                if (enemy.enemyType == EnemyBaseController.EnemyType.VIRUS)
                {
                    PlayerFalls(13);
                    enemy.DestroyEnemy();
                }
                if (enemy.enemyType == EnemyBaseController.EnemyType.WINGEDCLEANER)
                {
                    PlayerFalls(1);
                }
                playerScreen.PlayEnemyHitSound();
                flashMaterial = enemyFlashMaterial;
                CallFlashOverDuration(1f);
            }
            if (invincible)
            {
                enemy.DestroyEnemy();
            }     
        }
        if (coll.gameObject.tag == "Powerup")
        {
            PowerupBaseController powerUp = coll.gameObject.GetComponent<PowerupBaseController>();
            if (powerUp.powerUpType == PowerupBaseController.PowerUpType.AERO)
            {
                playerScreen.CallPowerupInvincible();
                playerScreen.PlayPowerupSound();
                powerUp.DestroyPowerup();
            }
            if (powerUp.powerUpType == PowerupBaseController.PowerUpType.PUFFER)
            {
                StartCoroutine(PlayerSpeedUp(5f));
                //playerScreen.BumpPlayerUp(5f);
                playerScreen.PlayPowerupSound();
                powerUp.DestroyPowerup();
            }
            flashMaterial = powerUpFlashMaterial;
            CallFlashOverDuration(5f);
        }         
    }
    
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            EnemyBaseController enemy = coll.gameObject.GetComponent<EnemyBaseController>();
            if (!invincible)
            {
                if (justBeenHit + hitRecovery < Time.time)
                {
                    if (enemy.enemyType == EnemyBaseController.EnemyType.CIGARETTE)
                    {
                        PlayerFalls(1f);
                    }
                    if (enemy.enemyType == EnemyBaseController.EnemyType.DUST)
                    {
                        PlayerFalls(1f);
                    }
                    if (enemy.enemyType == EnemyBaseController.EnemyType.SWEATYCLOUD)
                    {
                        PlayerFalls(1f);
                    }
                    playerScreen.PlayEnemyHitSound();
                    justBeenHit = Time.time;
                    StartCoroutine("PlayerSlowed");
                }
                CallFlashOverDuration(0.2f);
            }
            if (invincible)
            {
                enemy.DestroyEnemy();
            }
        }
    }


    void PlayerFalls(float ammount)
    {
        //playerScreen.KnockPlayerDown(ammount);
    }

    IEnumerator PlayerSlowed()
    {
        moveSpeed = defaultMoveSpeed / 2;
        yield return new WaitForSeconds(0.5f);
        moveSpeed = defaultMoveSpeed;
    }

    IEnumerator PlayerSpeedUp(float duration)
    {
        moveSpeed = defaultMoveSpeed * 2;
        yield return new WaitForSeconds(duration);
        moveSpeed = defaultMoveSpeed;
    }
       
    
    // cannot move the player when active
    public void LockPlayerMovement()
    {
        rb.velocity = new Vector2(0, 0);
        canMove = false;
    }
    public void UnlockPlayerMovement()
    {
        canMove = true;
    }



    // for calling various sprite flashes from other scripts, like powerups
    public void CallFlashOverDuration(float duration)
    {                
        StartCoroutine(CallFlashCoroutine(duration));        
    }
    // invokes the flashing, cancels after duration
    IEnumerator CallFlashCoroutine(float duration)
    {
        CancelInvoke("InvokeFlash");        
        InvokeRepeating("InvokeFlash", 0, 0.01f);
        yield return new WaitForSeconds(duration);
        CancelInvoke("InvokeFlash");
    }
    // determins how many flashes to run
    void InvokeFlash()
    {
        StartCoroutine("Flash");
    }
    IEnumerator Flash()
    {
        spriteRenderer.material = flashMaterial;        
        yield return new WaitForSeconds(0.0002f);
        spriteRenderer.material = normalMaterial;        
    }

}
