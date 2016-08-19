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
    public Rigidbody2D playerRb;

    public GameObject destroyedParticles;


    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameController.playerScreen.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Boundaries")
        {
            DestroyPowerup();
        }        
    }

    public virtual void Movement()
    {

    }

    public void DestroyPowerup()
    {
        Instantiate(destroyedParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
