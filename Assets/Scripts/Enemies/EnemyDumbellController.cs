using UnityEngine;
using System.Collections;

public class EnemyDumbellController : EnemyBaseController {

    // x and y movement speeds
    public float horizontalMovement = 0;
    public float verticalMovement = -3;

    
    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        base.Movement();
        enemyType = EnemyType.DUMBELL;
        rb.velocity = new Vector2(horizontalMovement + playerRb.velocity.x, verticalMovement + playerRb.velocity.y);
    }

    public override void HitPlayer()
    {
        base.HitPlayer();
        GameController.lungCharacter.LoseLives(1);
        DestroyEnemy();
    }
}
