using UnityEngine;
using System.Collections;

public class CannonAimShoot : MonoBehaviour {
    bool notShotYet;
    float currentZRot;
    Rigidbody2D rb2D;
    public float speed = 150;
	// Use this for initialization
	void Start () {
        notShotYet = true;
        rb2D = GetComponent<Rigidbody2D>();
}
	
	// Update is called once per frame
	void Update ()
    {
        if (notShotYet)
        {
            RotateCannon();
        }
            
    }
    void RotateCannon()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {

            rb2D.MoveRotation(rb2D.rotation + speed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
                rb2D.MoveRotation(rb2D.rotation - speed * Time.fixedDeltaTime);
        }
        //keeps it from going too far over
        if (transform.localEulerAngles.z > 85 && transform.localEulerAngles.z < 180)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 85);
        }
        else if (transform.rotation.z < 0 || transform.localEulerAngles.z > 180)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0 );
        }

    }
}
