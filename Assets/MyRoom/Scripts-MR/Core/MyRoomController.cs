﻿using System.Collections;
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
#if UNITY_STANDALONE || UNITY_EDITOR
            PlacementControls();
#endif
#if UNITY_ANDROID
            TouchDetect();
#endif
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
                    SaveLoadMyRoom.instance.RemovePlayerPosition(playerSelected.transform.position);
                }
            }
        }       
    }

    void TouchDetect()
    {
        Vector3 touchPosWorld;
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (playerSelected)
            {
                SaveLoadMyRoom.instance.AddToPlayerPositions(playerSelected.transform.position);
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
            MyRoomSoundController.instance.PlaySound(0);
        }
    }

    void PlacementControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (playerSelected)
            {
                SaveLoadMyRoom.instance.AddToPlayerPositions(playerSelected.transform.position);
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
            MyRoomSoundController.instance.PlaySound(0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (selectedItem)
            {
                SaveLoadMyRoom.instance.RemoveFromMyItems((int)selectedItem.itemID, selectedItem.transform.position);
                currentInputState = InputState.VIEWING;
                Destroy(selectedItem.gameObject);
                MyRoomSoundController.instance.PlaySound(1);
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
        MyRoomMainUIController.instance.ShowCurrentStars();
        MyRoomMainUIController.instance.ShopActive(false);
    }

    public void BackgroundBought(MyRoomBackgroundController background)
    {
        Vector3 pos = new Vector3(0, -5, 0);
        MyRoomBackgroundController newBackground = Instantiate(background, pos, background.transform.rotation);        
        currentBackground = newBackground;
        SubtractStars(background.cost);
        SaveLoadMyRoom.instance.AddToMyBackground((int)background.backgroundID);
        MyRoomMainUIController.instance.ShowCurrentStars();
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

    public MyRoomPlayerController FindPlayer()
    {
        return GameObject.Find("MyPlayer").GetComponent<MyRoomPlayerController>();
    }

}
