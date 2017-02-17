using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomShopController : MonoBehaviour {

    public MyRoomPlaceableItemController[] items;

    public MyRoomBackgroundController[] backgrounds;

    public static MyRoomShopController instance;

    void Awake()
    {
        instance = this;
    }


    public void BuyItem(int itemNumber)
    {
        if (MyRoomController.instance.CanAffordItem(items[itemNumber].cost))
        {
            MyRoomController.instance.ItemBought(items[itemNumber]);
        }
    }

    public void BuyBackground(int backgroundNumber)
    {
        if (MyRoomController.instance.CanAffordItem(backgrounds[backgroundNumber].cost))
        {
            MyRoomController.instance.BackgroundBought(backgrounds[backgroundNumber]);
        }
    }

}
