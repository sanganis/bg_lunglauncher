using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomInputController : MonoBehaviour {


    public MyRoomPlaceableItemController selectedItem;

    Camera cam;

    public enum InputState
    {
        VIEWING,
        SELECTING,
        PLACING
    }

    public InputState currentInputState;


    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        if (currentInputState == InputState.PLACING)
        {
            AttachItemToMouse();
        }
    }

    void AttachItemToMouse()
    {
        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        selectedItem.transform.position = pos;
    }


}
