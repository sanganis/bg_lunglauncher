using UnityEngine;
using System.Collections;

public class EnemyBaseController : MonoBehaviour {

    // a base controller for all enemies

    // set in the inspector to determine where the enemy will spawn
    public bool spawnsAtBottom, spawnsAtRightSide, spawnsAtTop;

    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Rigidbody2D playerRb;

    public GameObject destroyedParticles;

	public enum EnemyType
    {
        BALLOONCAT,
        DUMBELL,
        ROCKETDOG,
        DUST,
        SWEATYCLOUD,
        CIGARETTE,
        VIRUS,
        WINGEDCLEANER
    }

    [HideInInspector]
    public EnemyType enemyType;

	public virtual void Start () {

        rb = GetComponent<Rigidbody2D>();
        playerRb = GameController.playerScreen.GetComponent<Rigidbody2D>();
	}
	
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Boundaries")
        {
            DestroyEnemey();
        }
    }

    public virtual void Movement()
    {
        
    }

    public void DestroyEnemey()
    {
        Instantiate(destroyedParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
