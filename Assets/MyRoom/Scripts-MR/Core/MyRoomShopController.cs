using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomShopController : MonoBehaviour {

    public MyRoomPlaceableItemController[] items;



    public void BuyItem(int itemNumber)
    {
        if (MyRoomController.instance.CanAffordItem(items[itemNumber].cost))
        {
            MyRoomController.instance.ItemBought(items[itemNumber]);
        }
    }
}
