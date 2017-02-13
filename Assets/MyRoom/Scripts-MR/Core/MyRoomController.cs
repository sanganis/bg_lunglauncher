using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomController : MonoBehaviour {
    
    [HideInInspector]
    public static MyRoomController instance;

    public MyRoomPlaceableItemController selectedItem;

    Camera cam;
    Vector2 mousePos;
   
    public enum InputState
    {
        VIEWING,
        SELECTING,
        PLACING
    }

    public InputState currentInputState;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {        
        cam = Camera.main;        
    }


    void InstantiateMySavedItems()
    {
       


    }


    void Update()
    {
        UpdateMousePos();
        if (currentInputState == InputState.VIEWING)
        {
            ViewingControls();
        }
        else if (currentInputState == InputState.PLACING)
        {
            AttachItemToMouse();
            PlacementControls();
        }
    }

    void UpdateMousePos()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }


    void ViewingControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                if(hit.transform.gameObject.tag == "PlaceableItem")
                {
                    selectedItem = hit.transform.gameObject.GetComponent<MyRoomPlaceableItemController>();
                    currentInputState = InputState.PLACING;
                }
            }
       }
    }

    void PlacementControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentInputState = InputState.VIEWING;
            selectedItem = null;
        }
    }
    
    void AttachItemToMouse()
    {
        selectedItem.transform.position = mousePos;
    }

    public void ItemBought(MyRoomPlaceableItemController item)
    {        
        MyRoomPlaceableItemController newItem = (MyRoomPlaceableItemController)Instantiate(item, mousePos, item.transform.rotation);
        selectedItem = newItem;
        AddItemToOwnedList(item);
        currentInputState = InputState.PLACING;
    }    
    

}
