using UnityEngine;
using System.Collections;

public class PowerupPufferController : PowerupBaseController {
    
    // y movement speed    
    public float verticalMovement = 1f;

    // temporary x movement speed, which is randomly set on initialization
    float horizontalMovement;

    public override void Start()
    {
        base.Start();
        horizontalMovement = directionOfTravelX * 2;
    }


    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        base.Movement();
        rb.velocity = new Vector2(horizontalMovement + playerRb.velocity.x, verticalMovement + playerRb.velocity.y);
    }
}
