using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour {
	public float maxSpeed =10f;
	private float movementY;
	private SpringJoint2D spring;
	private bool ClickedOn;
	public Transform launcher; 
	public float maxStretch = 3.0f;
	private Ray rayToMouse;
	private Ray launcherToProjectile;
	private float maxStretchSqr;
	private Vector2 prevVelocity;


	void Awake(){

		spring = GetComponent<SpringJoint2D> ();
	}

	void Start () {
		rayToMouse = new Ray (launcher.position, Vector3.zero);
		maxStretchSqr = maxStretch * maxStretch;
		launcherToProjectile = new Ray (launcher.position, Vector3.zero);
	}
	
	void Update(){
		if(ClickedOn){
			Dragging ();
		}
		if (spring != null) {
			if(!GetComponent<Rigidbody2D> ().isKinematic &&prevVelocity.sqrMagnitude > GetComponent<Rigidbody2D> ().velocity.sqrMagnitude){
				Destroy (spring);
				GetComponent<Rigidbody2D> ().velocity = prevVelocity;
			}
			if (!ClickedOn) {
				prevVelocity = GetComponent<Rigidbody2D> ().velocity;

			}
			Vector2 launchProjectile = transform.position - launcher.position;
			launcherToProjectile.direction = launchProjectile;
		}

	}
	void FixedUpdate () {
		//after launch, press up arrow to go up
		if (!ClickedOn && spring == null) {
			movementY = Input.GetAxis ("Vertical");

			GetComponent<Rigidbody2D> ().velocity = new Vector2 (3.0f, movementY * maxSpeed);
		}
	}

	void OnMouseDown(){
		spring.enabled = false;
		ClickedOn = true;
	}
	void OnMouseUp(){
		spring.enabled = true;
		GetComponent<Rigidbody2D> ().isKinematic = false;
		ClickedOn = false;
	}

	void Dragging(){
		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 launchToMouse = mouseWorldPoint - launcher.position;


		if (launchToMouse.sqrMagnitude > maxStretchSqr) {
			rayToMouse.direction = launchToMouse;
			mouseWorldPoint = rayToMouse.GetPoint (maxStretch);
		}

		mouseWorldPoint.z = 0f;
		transform.position = mouseWorldPoint;


	}
}
