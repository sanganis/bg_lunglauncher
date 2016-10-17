using UnityEngine;
using System.Collections;

public class PowerupPufferController : PowerupBaseController {
    

    public override void Start()
    {
        base.Start();        
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
}
