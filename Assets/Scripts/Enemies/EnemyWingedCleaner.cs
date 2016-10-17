using UnityEngine;
using System.Collections;

public class EnemyWingedCleaner : EnemyBaseController {

   
    public override void Start()
    {
        base.Start();
        enemyType = EnemyType.WINGEDCLEANER;        
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
    public override void HitPlayer()
    {
        base.HitPlayer();
        GameController.lungCharacter.LoseLives(1);
        DestroyEnemy();
    }
}
