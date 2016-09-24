using UnityEngine;
using System.Collections;

public class PeakFlowPlayerObjectController : MonoBehaviour
{

    // components
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    public PeakFlowPlayerController playerScreen;
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

    void Update()
    {
        if (playerScreen.launchedYet)
        {
            
                rb.velocity = new Vector2(playerScreen.rb.velocity.x, playerScreen.rb.velocity.y);
            
        }
    }
    
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            playerScreen.LockScreenMovement();
            LockPlayerMovement();
            playerScreen.GameOverSuccess();
        }
    }
    
    void PlayerFalls(float ammount)
    {
        playerScreen.KnockPlayerDown(ammount);
    }
    
    // cannot move the player when active
    public void LockPlayerMovement()
    {
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;
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
