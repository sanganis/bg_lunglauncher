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
        Vector3 pos = new Vector3(0, -6, 0);
        trans.position = pos;
	}
	
}
