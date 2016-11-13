using UnityEngine;
using System.Collections;

public class BackgroundRelativeMovement : MonoBehaviour {

    public float relativeSpeed = 0.5f;

    Rigidbody2D rb;
    Rigidbody2D playerRb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameController.playerScreen.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rb.velocity = new Vector2(playerRb.velocity.x * relativeSpeed, playerRb.velocity.y * relativeSpeed);
	}
}
