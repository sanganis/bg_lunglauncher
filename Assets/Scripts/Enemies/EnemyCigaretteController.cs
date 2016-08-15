using UnityEngine;
using System.Collections;

public class EnemyCigaretteController : EnemyBaseController {

    // y movement speed    
    public float verticalMovement = 1;

    // temporary x movement speed, which is randomly set on initialization
    float horizontalMovement;

    public override void Start()
    {
        base.Start();
        enemyType = EnemyType.CIGARETTE;
        horizontalMovement = Random.Range(-2f, 0f);
    }


    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        base.Movement();
        rb.velocity = new Vector2(horizontalMovement + playerRb.velocity.x, verticalMovement + playerRb.velocity.y);
    }
}
