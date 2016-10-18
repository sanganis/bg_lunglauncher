using UnityEngine;
using System.Collections;

public class PowerupAeroController : PowerupBaseController {

    public int livesGiven = 1;
  
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
        UpdateDirectionOfTravel();
        rb.velocity = directionOfTravel * moveSpeed;
        rb.velocity = new Vector2(rb.velocity.x + playerRb.velocity.x, rb.velocity.y + playerRb.velocity.y);
    }

    public override void HitPlayer()
    {
        base.HitPlayer();
        GameController.lungCharacter.GainLives(livesGiven);
        DestroyPowerup();
    }
}
