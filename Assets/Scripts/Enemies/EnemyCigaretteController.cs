using UnityEngine;
using System.Collections;

public class EnemyCigaretteController : EnemyBaseController {

   

    public override void Start()
    {
        base.Start();
        enemyType = EnemyType.CIGARETTE;        
    }


    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        base.Movement();
        rb.velocity = directionOfTravel * moveSpeed;
        rb.velocity = new Vector2(rb.velocity.x + playerRb.velocity.x, rb.velocity.y + playerRb.velocity.y);
    }
}
