using UnityEngine;
using System.Collections;

public class PlayerColliderMovement : MonoBehaviour {

    public float moveSpeed = 2f;

    public Rigidbody2D rb;

    public PlayerScreenController player;

    bool canMove = true;
	
	void Update () {
        if (player.launchedYet)
        {
            if (Input.GetMouseButton(0) && canMove)
            {
                Move();
            }
            else
            {
                rb.velocity = new Vector2(player.rb.velocity.x, player.rb.velocity.y);
            }
        }
    }

    void Move()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (target - transform.position).normalized;
        //rb.velocity = dir * moveSpeed; 
        rb.velocity = new Vector2((dir.x * moveSpeed) + player.rb.velocity.x, (dir.y * moveSpeed) + player.rb.velocity.y).normalized;       
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            player.LockMovement();
            StopMovement();
        }
    }


    void StopMovement()
    {
        rb.velocity = new Vector2(0, 0);
        canMove = false;
    }



}
