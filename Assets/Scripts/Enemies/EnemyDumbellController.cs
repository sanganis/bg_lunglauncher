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
        rb.velocity = new Vector2(horizontalMovement, verticalMovement);
    }
}
