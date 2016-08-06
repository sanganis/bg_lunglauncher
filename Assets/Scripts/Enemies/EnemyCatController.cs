using UnityEngine;
using System.Collections;

public class EnemyCatController : EnemyBaseController {

    // y movement speed    
    public float verticalMovement = 1;

    // temporary x movement speed, which is randomly set on initialization
    float horizontalMovement;

    public override void Start()
    {
        base.Start();
        horizontalMovement = Random.Range(-0.5f, 0.5f);
    }


    void Update () {
        Movement();
	}

    public override void Movement()
    {
        base.Movement();
        rb.velocity = new Vector2(horizontalMovement, verticalMovement);
    }

}
