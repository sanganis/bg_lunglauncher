using UnityEngine;
using System.Collections;

public class PlayerColliderMovement : MonoBehaviour {

    public float moveSpeed = 2f;

    public Rigidbody2D rb;

    public PlayerScreenController playerScreen;

    bool canMove = true;
	
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
                PlayerFalls(5);
            }
        }
    }


    void PlayerFalls(float ammount)
    {
        playerScreen.KnockPlayerDown(ammount);
    }

    void PlayerSlowed()
    {

    }




    // cannot move the player when active
    public void LockPlayerMovement()
    {
        rb.velocity = new Vector2(0, 0);
        canMove = false;
    }
    public void UnlockPlayerMovement()
    {

    }



}
