using UnityEngine;
using System.Collections;

public class EnemyBaseController : MonoBehaviour {

    // a base controller for all enemies

    public int hitPoints = 1;

    // set in the inspector to determine where the enemy will spawn
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
    [HideInInspector]
    public Vector2 directionOfTravel;
    public float moveSpeed = 1f;

    public int damageToPlayer = 1;

    public GameObject[] damageParticles;
    public GameObject[] destroyedParticles;

    public bool isChildsTrigger;

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
        playerScreen = GameController.playerScreen.GetComponent<PlayerScreenController>();
        playerRb = playerScreen.GetComponent<Rigidbody2D>();
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
        if (coll.gameObject.tag == "Boundaries")
        {
            DestroyEnemy();
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
        playerScreen.PlayPlayerHurtSound();        
        if (!GameController.lungCharacter.invincible)
        {
            GameController.lungCharacter.LoseLives(damageToPlayer);
            DestroyEnemy();
        }
        else
        {
            GameController.gameController.enemiesDestroyed++;
            GameController.mainUIController.SetEnemiesDestroyed();
            DestroyEnemy();
        }
    }

    // called from InputController when the player taps the enemy
    public virtual void TapDamage()
    {
        hitPoints--;
        if(hitPoints > 0)
        {
            for (int i = 0; i < damageParticles.Length; i++)
            {
                Instantiate(damageParticles[i], transform.position, transform.rotation);
            }
            GameController.playerScreen.PlayEnemyHitSound();
        }
        if (hitPoints == 0)
        {
            GameController.gameController.enemiesDestroyed++;
            GameController.mainUIController.SetEnemiesDestroyed();
            GameController.playerScreen.PlayEnemyDestroyedSound();
            DestroyEnemy();
        }
    }

    public void DestroyEnemy(bool displayParticles = true)
    {
        if (displayParticles)
        {
            for (int i = 0; i < destroyedParticles.Length; i++)
            {
                Instantiate(destroyedParticles[i], transform.position, transform.rotation);
            }
        }
        Destroy(gameObject);
    }
       

}
