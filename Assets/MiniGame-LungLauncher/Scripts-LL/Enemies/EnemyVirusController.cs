using UnityEngine;
using System.Collections;

public class EnemyVirusController : EnemyBaseController {

  
    // how long it stops and doesn't move for
    public float stopTime = 1f;
    // how long it moves for before stopping
    public float moveTime = 2f; 
    
    public override void Start()
    {
        base.Start();
        enemyType = EnemyType.VIRUS;
        StartCoroutine("RandomMovement");
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

    
    IEnumerator RandomMovement()
    {
        ChooseRandomDirection();        
        yield return new WaitForSeconds(moveTime / 2);
        ChooseNoDirection();
        yield return new WaitForSeconds(stopTime);
        ChoosePlayerDirection();        
        yield return new WaitForSeconds(moveTime);
        ChooseNoDirection();
        yield return new WaitForSeconds(stopTime);
        StartCoroutine("RandomMovement");
    }
      

}
