using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControllerNew : MonoBehaviour {

    public float moveSpeed = 2f;
    public float currentHeight;

    public Rigidbody2D rb;

    bool launchedYet = false;
    bool clickedYet = false;

    private float launchSpeedVariable = 0f;
    private float initTime;
    public float maxLaunchSpeed = 100f;

    private float movementY;
    public float maxSpeed = 50f;
    private bool goingDown;

    private Slider aimStrengthSlider;


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

            if (Input.GetMouseButton(0))
            {
                Move();
            }
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
            rb.velocity = new Vector2(0, launchSpeed);
            Debug.Log(launchedYet);
            //StartCoroutine (launchedCounter ());
        }
    }

    void Move()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (target - transform.position).normalized;
        rb.velocity = dir * moveSpeed;
    }




    IEnumerator launchedCounter()
    {
        yield return new WaitForSeconds(3);
        goingDown = true;
        Debug.Log(goingDown);
    }

}
