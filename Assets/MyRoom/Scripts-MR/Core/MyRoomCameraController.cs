using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomCameraController : MonoBehaviour {

    public static MyRoomCameraController instance;

    [HideInInspector]
    public Camera camera;
	
	void Awake ()
    {
        instance = this;
        camera = GetComponent<Camera>();
	}
	
}
