using UnityEngine;
using System.Collections;

public class PlayerColliderMovement : MonoBehaviour {

    public float moveSpeed = 2f;

    public Rigidbody2D rb;

    public PlayerControllerNew player;

    
	
	// Update is called once per frame
	void Update () {
        if (player.launchedYet)
        {
            if (Input.GetMouseButton(0))
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
        rb.velocity = new Vector2((dir.x * moveSpeed) + player.rb.velocity.x, (dir.y * moveSpeed) + player.rb.velocity.y);       
    }



}
