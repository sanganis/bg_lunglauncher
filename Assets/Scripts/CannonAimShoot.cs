﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CannonAimShoot : MonoBehaviour {
      
    bool shotYet;
    float currentZRot;
    Rigidbody2D rb2D;
    public float speed = 150;

    private PlayerLaunch player;


    private Slider aimStrengthSlider;
    // Use this for initialization
    void Start () {
        shotYet = false;
        rb2D = GetComponent<Rigidbody2D>();
        aimStrengthSlider = GetComponentInChildren<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLaunch>();
        aimStrengthSlider.value = player.GetLaunchVariable();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!shotYet && (Input.GetKey(KeyCode.Space)))
        {
            LaunchSpeed();
        }

        /*if (Input.GetKeyDown(KeyCode.R) && shotYet)
        {
            shotYet = !shotYet;
        }*/
            
    }

    void LaunchSpeed()
    {
        aimStrengthSlider.value = player.GetLaunchVariable();
    }
    //This is deprecated for now
    void RotateCannon()
    {

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow))
        {

            rb2D.MoveRotation(rb2D.rotation + speed * Time.fixedDeltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow))
        {
                rb2D.MoveRotation(rb2D.rotation - speed * Time.fixedDeltaTime);
        }
        //keeps it from going too far over
        print(transform.localEulerAngles);
        if (transform.localEulerAngles.z > 135)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 135);
        }
        if (transform.rotation.z > 45)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 45 );
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shotYet = true;
        }

    }
}
