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
    [HideInInspector]
    GameObject lungCharacter;

    // for movement
    public Vector2 directionOfTravel;
    public float moveSpeed = 1f;

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
        lungCharacter = GameObject.Find("LungCharacter");
        ChoosePlayerDirection();
    }

    public void ChoosePlayerDirection()
    {
        directionOfTravel = (lungCharacter.transform.position - transform.position).normalized;
    }
    public void ChooseRandomDirection()
    {
        directionOfTravel = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
    }
    public void ChooseNoDirection()
    {
        directionOfTravel = new Vector2(0, 0);
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

    // called from InputController when the player taps the enemy
    public virtual void TapDamage()
    {
        DestroyEnemey();
    }

    public void DestroyEnemey()
    {
        Instantiate(destroyedParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
       

}
