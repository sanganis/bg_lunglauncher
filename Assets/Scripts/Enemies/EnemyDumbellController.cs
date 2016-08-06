using UnityEngine;
using System.Collections;

public class EnemyDumbellController : EnemyBaseController {

    public float horizontalMovement = 0;
    public float verticalMovement = -3;

    // Update is called once per frame
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
