using UnityEngine;
using System.Collections;

using UnityEngine.UI;
public class PlayerLaunch : MonoBehaviour {

    
    Rigidbody2D rb2D;
    bool launchedYet = false;
    bool clickedYet = false;

    private float launchSpeedVariable = 0f;
    private float initTime;
    public float maxLaunchSpeed = 100f;

    private Slider aimStrengthSlider;
    // Use this for initialization
    void Start () {

        aimStrengthSlider = GameObject.FindGameObjectWithTag("Power Slider").GetComponent<Slider>();
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!launchedYet)
        {
            CheckLaunch();
        }
        
	}
    public float GetLaunchVariable()
    {
        return launchSpeedVariable;
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
            launchSpeedVariable = Mathf.Abs( Mathf.Sin(Time.time - initTime));
            aimStrengthSlider.value = launchSpeedVariable;
            //print("I am ready to launch! " + launchSpeedVariable);
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1"))
        {
            
            launchedYet = true;
            float launchSpeed = launchSpeedVariable * maxLaunchSpeed;
            gameObject.transform.parent = null;
            rb2D.isKinematic = false;
            rb2D.velocity = new Vector2(0, launchSpeed);
            
        }
    }
}
