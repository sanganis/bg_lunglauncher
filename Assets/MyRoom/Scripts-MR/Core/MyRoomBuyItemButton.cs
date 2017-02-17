using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomBuyItemButton : MonoBehaviour {

    public enum ItemID
    {
        SUNGLASSES,
        HAT,
        CHAIR,
        LAMP,
        DOG,
        CAT,
        BIKE,
        BALL,
        BABYBUG,
        PETBUG,
        TEDDYBEAR,
        TSHIRT_TOC,
        TSHIRT_BG,
        SKIRT_PINK,
        SHORTS_GREEN
    }

    public ItemID itemID;
    

    public void BuyItem()
    {
        MyRoomShopController.instance.BuyItem((int)itemID);
    }


}
