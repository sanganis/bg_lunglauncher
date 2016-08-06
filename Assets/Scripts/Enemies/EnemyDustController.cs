using UnityEngine;
using System.Collections;

public class EnemyDustController : EnemyBaseController {

    public float randomHorizontalMovementMax = 1;
    public float randomVerticalMovementMax = 1;
    public float changeDirectionTime = 0.5f;

    float currentHorizontalMovement;
    float currentVerticalMovement;

    public override void Start()
    {
        base.Start();
        StartCoroutine("RandomMovement");
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        base.Movement();
        rb.velocity = new Vector2(currentHorizontalMovement, currentVerticalMovement);
    }

    IEnumerator RandomMovement()
    {
        currentHorizontalMovement = Random.Range(-1f, 1f);
        currentVerticalMovement = Random.Range(-1f, 1f);
        yield return new WaitForSeconds(changeDirectionTime);
        StartCoroutine("RandomMovement");
    }



}
