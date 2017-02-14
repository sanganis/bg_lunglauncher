using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomBackgroundController : MonoBehaviour {

    RectTransform trans;

    public int cost;


    public enum BackgroundID
    {
        BEDROOM,
        FIELD,
        SPACESHIP
    }

    public BackgroundID backgroundID;


    void Start () {
        trans = GetComponent<RectTransform>();
        trans.position = Vector3.zero;
	}
	
}
