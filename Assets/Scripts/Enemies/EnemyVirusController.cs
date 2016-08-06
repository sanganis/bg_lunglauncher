using UnityEngine;
using System.Collections;

public class EnemyVirusController : EnemyBaseController {

    // the maximum movement speed it can go at, which is chosen randomly from between these ints
    // using ints for the virus, so that the speed changes are more dramatic
    public int randomHorizontalMovementMax = 1;
    public int randomVerticalMovementMax = 1;
    // how long it stops and doesn't move for
    public float stopTime = 1f;
    // how long before it resets and starts moving again
    public float waitBeforeMoveTime = 2f;

    // temporary variables, used to set the random movement
    int currentHorizontalMovement;
    int currentVerticalMovement;

    public override void Start()
    {
        base.Start();
        StartCoroutine("RandomMovement");
    }

    
    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        base.Movement();
        rb.velocity = new Vector2(currentHorizontalMovement, currentVerticalMovement);
    }

    // moves at a random x and y direction until stopTime, then waits for waitBeforeMoveTime before resetting
    IEnumerator RandomMovement()
    {
        currentHorizontalMovement = Random.Range(-randomHorizontalMovementMax, randomHorizontalMovementMax);
        currentVerticalMovement = Random.Range(-randomVerticalMovementMax, randomVerticalMovementMax);
        yield return new WaitForSeconds(stopTime);
        currentHorizontalMovement = 0;
        currentVerticalMovement = 0;
        yield return new WaitForSeconds(waitBeforeMoveTime);
        StartCoroutine("RandomMovement");
    }
}
