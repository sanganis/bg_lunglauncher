using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomShopController : MonoBehaviour {

    public MyRoomPlaceableItemController[] items;



    public void BuyItem(int itemNumber)
    {
        MyRoomController.instance.ItemBought(items[itemNumber]);
    }
}
