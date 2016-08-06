using UnityEngine;
using System.Collections;

public class EnemyRocketDog : EnemyBaseController {

    public float horizontalMovement = -3;
    float verticalMovement;


    public override void Start()
    {
        base.Start();
        verticalMovement = Random.Range(-1f, 1f);
    }

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
