using UnityEngine;
using System.Collections;

public class SmileyLaunch : MonoBehaviour {

    float speed = 100f;
    Rigidbody2D rb2D;
    Camera cameraFollow;
    public GameObject cannon;
    bool launchedYet = false;
    public float launchSpeed = 5f;
    // Use this for initialization
    void Start () {
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!launchedYet)
        {
            CheckLaunch();
        }
        
	}

    void CheckLaunch()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            launchedYet = true;

           

            gameObject.transform.parent = null;
            rb2D.isKinematic = false;
            rb2D.velocity = new Vector2(0, launchSpeed);
            

        }
    }
}
