using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyRoomBuyItemButton : MonoBehaviour {

    public Text itemPriceText;

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
    

    void Start()
    {
        SetButtonPrice();
    }

    void SetButtonPrice()
    {
        itemPriceText.text = MyRoomShopController.instance.items[(int)itemID].cost.ToString();
    }


    public void BuyItem()
    {
        MyRoomShopController.instance.BuyItem((int)itemID);
    }


}
