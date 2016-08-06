using UnityEngine;
using System.Collections;

public class EnemyCatController : EnemyBaseController {

    public float horizontalMovement = -0.5f;
    public float verticalMovement = 1;


    // Update is called once per frame
    void Update () {
        Movement();
	}

    public override void Movement()
    {
        base.Movement();
        rb.velocity = new Vector2(0.5f, 1f);
    }

}
