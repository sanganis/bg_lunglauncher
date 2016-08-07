using UnityEngine;
using System.Collections;

public class EnemyWingedCleaner : EnemyBaseController {

    // x movement speed
    public float horizontalMovement = -2;

    // temporary y movement speed, which is randomly set on initialization
    float verticalMovement;


    public override void Start()
    {
        base.Start();
        verticalMovement = Random.Range(-1f, 1f);
    }

    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        base.Movement();
        rb.velocity = new Vector2(horizontalMovement, verticalMovement + playerRb.velocity.y);
    }
}
