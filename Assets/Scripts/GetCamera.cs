using UnityEngine;
using System.Collections;

public class GetCamera : MonoBehaviour {
    private Camera mainCam;
	

    //Finds the Main Camera and parents it to the player
	void Start () {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mainCam.transform.parent = gameObject.transform;
	}
	
}
