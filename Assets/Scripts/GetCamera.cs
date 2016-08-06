using UnityEngine;
using System.Collections;

public class GetCamera : MonoBehaviour {
    private Camera mainCam;
	// Use this for initialization
	void Start () {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mainCam.transform.parent = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
