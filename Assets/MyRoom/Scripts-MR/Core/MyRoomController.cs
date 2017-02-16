using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomController : MonoBehaviour {
    
    [HideInInspector]
    public static MyRoomController instance;

    public MyRoomPlaceableItemController selectedItem;

    public MyRoomPlayerController playerSelected;

    public MyRoomBackgroundController currentBackground;

    Camera cam;
    Vector2 mousePos;

    public int currentStars;
   
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
        currentStars = SaveLoadMyRoom.instance.LoadStars();   
        if(PlayerPrefs.HasKey("FirstRun") == false)
        {
            currentStars = 100;
            PlayerPrefs.SetInt("FirstRun", 1);
        }     
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
                if (hit.transform.gameObject.tag == "PlaceableItem")
                {
                    selectedItem = hit.transform.gameObject.GetComponent<MyRoomPlaceableItemController>();
                    currentInputState = InputState.PLACING;
                    selectedItem.placing = true;
                    SaveLoadMyRoom.instance.RemoveFromMyItems((int)selectedItem.itemID, selectedItem.transform.position);
                }
                if (hit.transform.gameObject.tag == "Player")
                {
                    playerSelected = hit.transform.gameObject.GetComponent<MyRoomPlayerController>();
                    currentInputState = InputState.PLACING;
                    playerSelected.placing = true;
                }
            }
        }       
    }

    void PlacementControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (playerSelected)
            {
                playerSelected.placing = false;
                currentInputState = InputState.VIEWING;
                playerSelected = null;
            }
            else if (selectedItem)
            {
                SaveLoadMyRoom.instance.AddToMyItems((int)selectedItem.itemID, selectedItem.transform.position);
                selectedItem.placing = false;
                currentInputState = InputState.VIEWING;
                selectedItem = null;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (selectedItem)
            {
                SaveLoadMyRoom.instance.RemoveFromMyItems((int)selectedItem.itemID, selectedItem.transform.position);                
                currentInputState = InputState.VIEWING;
                Destroy(selectedItem.gameObject);                
            }
        }
    }
    
    void AttachItemToMouse()
    {
        if (playerSelected)
        {
            playerSelected.transform.position = mousePos;
        }
        else
        {
            selectedItem.transform.position = mousePos;
        }
    }

    public void ItemBought(MyRoomPlaceableItemController item)
    {        
        MyRoomPlaceableItemController newItem = Instantiate(item, mousePos, item.transform.rotation);
        selectedItem = newItem;        
        currentInputState = InputState.PLACING;
        selectedItem.placing = true;
        SubtractStars(item.cost);
        MyRoomMainUIController.instance.SetCurrentStars();
    }

    public void BackgroundBought(MyRoomBackgroundController background)
    {
        Vector3 pos = new Vector3(0, -5, 0);
        MyRoomBackgroundController newBackground = Instantiate(background, pos, background.transform.rotation);        
        currentBackground = newBackground;
        SubtractStars(background.cost);
        SaveLoadMyRoom.instance.AddToMyBackground((int)background.backgroundID);
        MyRoomMainUIController.instance.SetCurrentStars();
    }


    void SubtractStars(int numberToSubtract)
    {
        currentStars -= numberToSubtract;
        SaveLoadMyRoom.instance.SaveStars(currentStars);
    }


    public bool CanAffordItem(int itemPrice)
    {
        if (currentStars >= itemPrice)
        {
            return true;
        }
        MyRoomMainUIController.instance.CallFlashNotEnoughStars();
        return false;
    }

}
