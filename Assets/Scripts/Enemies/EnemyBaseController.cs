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

    // movement stuff, for determining where the player is relative to the enemy
    Vector2 enemyScreenPos;
    Vector2 lungCharPos;
    Vector2 directionOfPlayer;
    [HideInInspector]
    public float directionOfTravelX;
    [HideInInspector]
    public float directionOfTravelY;
     

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
        UpdateDirectionOfTravel();
    }

    public void UpdateDirectionOfTravel()
    {
        // establish the relative direction of the player based on screen position        
        Transform lungTrans = lungCharacter.transform;
        lungCharPos = Camera.main.WorldToScreenPoint(lungTrans.position);
        enemyScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        directionOfPlayer = lungCharPos - enemyScreenPos;
        directionOfTravelX = directionOfPlayer.x / Screen.width;
        directionOfTravelY = directionOfPlayer.y / Screen.height;
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
