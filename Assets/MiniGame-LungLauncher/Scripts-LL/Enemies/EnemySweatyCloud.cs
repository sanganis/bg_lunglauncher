﻿using UnityEngine;
using System.Collections;

public class EnemySweatyCloud : EnemyBaseController {

    // the maximum movement speed the virus can go at, which is chosen from randomly
    public float randomHorizontalMovementMax = 0.5f;
    public float randomVerticalMovementMax = 0.5f;
    // how long it moves for before random movement is reset
    public float changeDirectionTime = 10f;
    
    public override void Start()
    {
        base.Start();
        enemyType = EnemyType.SWEATYCLOUD;
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

    // randomly chooses an x and y movement speed, then resets after chaingeDirectionTime
    IEnumerator RandomMovement()
    {
        ChooseRandomDirection();        
        yield return new WaitForSeconds(changeDirectionTime);
        ChoosePlayerDirection();        
        yield return new WaitForSeconds(changeDirectionTime);
        StartCoroutine("RandomMovement");
    }

    
}
