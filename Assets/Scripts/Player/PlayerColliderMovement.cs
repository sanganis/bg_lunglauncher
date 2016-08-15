using UnityEngine;
using System.Collections;

public class PlayerColliderMovement : MonoBehaviour {

    public float defaultMoveSpeed = 2f;
    [HideInInspector]
    public float moveSpeed = 2f;
    
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    public PlayerScreenController playerScreen;
    public ScreenShake mainCamera;

    // materials for making the sprite flash when damaged
    public Material normalMaterial;
    public Material flashMaterial;

    bool canMove = true;

    float justBeenHit;
    float hitRecovery = 1f;

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
        //rb.velocity = dir * moveSpeed; 
        rb.velocity = new Vector2((dir.x * moveSpeed) + playerScreen.rb.velocity.x, (dir.y * moveSpeed) + playerScreen.rb.velocity.y);       
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            playerScreen.LockScreenMovement();
            LockPlayerMovement();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Enemy")
        {
            EnemyBaseController enemy = coll.gameObject.GetComponent<EnemyBaseController>();
            if(enemy.enemyType == EnemyBaseController.EnemyType.BALLOONCAT)
            {
                PlayerFalls(10);
                enemy.DestroyEnemey();
            }            
            if (enemy.enemyType == EnemyBaseController.EnemyType.DUMBELL)
            {
                PlayerFalls(15);
                enemy.DestroyEnemey();
            }            
            if (enemy.enemyType == EnemyBaseController.EnemyType.ROCKETDOG)
            {
                PlayerFalls(10);
                enemy.DestroyEnemey();
            }            
            if (enemy.enemyType == EnemyBaseController.EnemyType.VIRUS)
            {
                PlayerFalls(13);
                enemy.DestroyEnemey();
            }
            if (enemy.enemyType == EnemyBaseController.EnemyType.WINGEDCLEANER)
            {
                PlayerFalls(1);                
            }
            CallFlashOverDuration(1f);            
        }
    }
    
    void OnTriggerStay2D(Collider2D coll)
    {
        EnemyBaseController enemy = coll.gameObject.GetComponent<EnemyBaseController>();
        if (justBeenHit > hitRecovery + Time.time)
        {
            if (enemy.enemyType == EnemyBaseController.EnemyType.CIGARETTE)
            {
                PlayerFalls(0.1f);
            }
            if (enemy.enemyType == EnemyBaseController.EnemyType.DUST)
            {
                PlayerFalls(0.1f);
            }
            if (enemy.enemyType == EnemyBaseController.EnemyType.SWEATYCLOUD)
            {
                PlayerFalls(0.1f);
            }
            justBeenHit = Time.time;
        }
        CallFlashOverDuration(0.2f);
    }


    void PlayerFalls(float ammount)
    {
        playerScreen.KnockPlayerDown(ammount);
    }

    IEnumerator PlayerSlowed(float ammount, float duration)
    {
        moveSpeed = defaultMoveSpeed / 2;
        yield return new WaitForSeconds(0.5f);
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
