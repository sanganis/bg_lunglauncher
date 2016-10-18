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
        StartCoroutine("FlapUpAndDown");
    }

    IEnumerator FlapUpAndDown()
    {
        ChoosePlayerDirection();
        yield return new WaitForSeconds(1f);
        directionOfTravel = new Vector2 (directionOfTravel.x,directionOfTravel.y + 1);
        yield return new WaitForSeconds(1f);
        ChoosePlayerDirection();
        yield return new WaitForSeconds(1f);
        directionOfTravel = new Vector2(directionOfTravel.x, directionOfTravel.y - 1);
        yield return new WaitForSeconds(1f);
        StartCoroutine("FlapUpAndDown");
    }
    


    public override void HitPlayer()
    {
        base.HitPlayer();
        GameController.lungCharacter.LoseLives(1);
        DestroyEnemy();
    }
}
