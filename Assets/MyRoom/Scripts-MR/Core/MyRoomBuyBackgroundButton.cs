using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyRoomBuyBackgroundButton : MonoBehaviour {


    public Text itemPriceText;

    public enum BackgroundID
    {
        BEDROOM,
        FIELD,
        SPACESHIP
    }

    public BackgroundID backgroundID;


    void Start()
    {
        SetButtonPrice();
    }

    void SetButtonPrice()
    {
        itemPriceText.text = MyRoomShopController.instance.backgrounds[(int)backgroundID].cost.ToString();
    }


    public void BuyItem()
    {
        MyRoomShopController.instance.BuyBackground((int)backgroundID);
    }
}
