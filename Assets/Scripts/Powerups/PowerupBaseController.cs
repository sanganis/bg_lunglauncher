using UnityEngine;
using System.Collections;

public class PowerupBaseController : MonoBehaviour {

    public enum PowerUpType
    {
        AERO,
        PUFFER
    }

    public PowerUpType powerUpType;

    public bool spawnsAtBottom, spawnsAtRightSide, spawnsAtTop;

    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public PlayerScreenController playerScreen;
    [HideInInspector]
    public Rigidbody2D playerRb;
    [HideInInspector]
    GameObject lungCharacter;
    
    // for movement
    public Vector2 directionOfTravel;
    public float moveSpeed = 1f;

    public GameObject pickupParticles;
    public GameObject destroyedParticles;


    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScreen = GameController.playerScreen.GetComponent<PlayerScreenController>();
        playerRb = playerScreen.GetComponent<Rigidbody2D>();
        lungCharacter = GameObject.Find("LungCharacter");        
    }

    public void UpdateDirectionOfTravel()
    {
        directionOfTravel = (lungCharacter.transform.position - transform.position).normalized;        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Boundaries")
        {
            DestroyPowerup(false);
        }
        if (coll.gameObject.tag == "Player")
        {
            HitPlayer();
        }
    }

    public virtual void Movement()
    {

    }

    public virtual void HitPlayer()
    {
        GameController.gameController.powerupsCollected++;
        GameController.mainUIController.SetPowerupsCollected();
        playerScreen.PlayPowerupSound();
        PickupPowerup();
    }

    // called from InputController when the player taps the enemy
    public virtual void TapDamage()
    {
        playerScreen.PlayPlayerHurtSound();        
        DestroyPowerup();
    }

    public void DestroyPowerup(bool displayParticles = true)
    {
        if (displayParticles)
        {
            Instantiate(destroyedParticles, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    public void PickupPowerup(bool displayParticles = true)
    {
        if (displayParticles)
        {
            Instantiate(pickupParticles, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

}
