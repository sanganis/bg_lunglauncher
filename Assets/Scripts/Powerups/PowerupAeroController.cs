using UnityEngine;
using System.Collections;

public class PowerupAeroController : PowerupBaseController {
    
    // x movement speed
    public float horizontalMovement = -2;

    // temporary y movement speed, which is randomly set on initialization
    float verticalMovement;


    public override void Start()
    {
        base.Start();
        verticalMovement = directionOfTravelY * 2;
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
