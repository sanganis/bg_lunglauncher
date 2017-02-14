using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyRoomMainUIController : MonoBehaviour {

    public static MyRoomMainUIController instance;

    public Text stars;
   
    void Awake()
    {
        instance = this;
    }

   

    public void SetCurrentStars(int number)
    {
        stars.text = number.ToString();
    }



}
