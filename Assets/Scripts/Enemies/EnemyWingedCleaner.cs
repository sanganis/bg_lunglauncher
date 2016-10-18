using UnityEngine;
using System.Collections;

public class EnemyWingedCleaner : EnemyBaseController {

   
    public override void Start()
    {
        base.Start();
        enemyType = EnemyType.WINGEDCLEANER;
        StartCoroutine("FlapUpAndDown");
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

    IEnumerator FlapUpAndDown()
    {
        ChoosePlayerDirection();
        yield return new WaitForSeconds(0.5f);
        directionOfTravel = new Vector2 (directionOfTravel.x,directionOfTravel.y + 1);
        yield return new WaitForSeconds(0.5f);
        ChoosePlayerDirection();
        yield return new WaitForSeconds(0.5f);
        directionOfTravel = new Vector2(directionOfTravel.x, directionOfTravel.y - 1);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("FlapUpAndDown");
    }
      
}

