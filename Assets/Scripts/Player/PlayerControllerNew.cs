using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControllerNew : MonoBehaviour {

    public PlayerColliderMovement playerCollider;
    
    public float currentHeight;

    public Rigidbody2D rb;

    public bool launchedYet = false;
    bool clickedYet = false;

    float launchSpeedVariable = 0f;
    float initTime;
    public float maxLaunchSpeed = 100f;

    float movementY;
    public float maxSpeed = 50f;
    bool goingDown;

    Slider aimStrengthSlider;


    void Start()
    {
        aimStrengthSlider = GameObject.FindGameObjectWithTag("Power Slider").GetComponent<Slider>();
        rb = GetComponent<Rigidbody2D>();
        goingDown = false;
    }


    void Update()
    {
        if (!launchedYet)
        {
            CheckLaunch();
        }
        else
        {
            currentHeight = transform.position.y;

            
        }
    }

    void FixedUpdate()
    {
        if (launchedYet && goingDown)
        {
            if (Input.GetKey("up"))
            {
                movementY = Input.GetAxis("Vertical");
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, movementY * maxSpeed);
            }
            else
            {
                movementY = -20.0f;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, movementY);
            }
            Debug.Log(GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void CheckLaunch()
    {
        if (!clickedYet && (Input.GetKeyDown(KeyCode.Space)) || Input.GetButtonDown("Fire1"))
        {
            clickedYet = true;
            initTime = Time.time;
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
        {
            launchSpeedVariable = Mathf.Abs(Mathf.Sin(Time.time - initTime));
            aimStrengthSlider.value = launchSpeedVariable;
            //print("I am ready to launch! " + launchSpeedVariable);
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1"))
        {

            launchedYet = true;
            float launchSpeed = launchSpeedVariable * maxLaunchSpeed;
            gameObject.transform.parent = null;
            rb.isKinematic = false;
            rb.velocity = new Vector2(launchSpeed, launchSpeed);
            Debug.Log(launchedYet);
            //StartCoroutine (launchedCounter ());
        }
    }
     




    IEnumerator launchedCounter()
    {
        yield return new WaitForSeconds(3);
        goingDown = true;
        Debug.Log(goingDown);
    }

}
