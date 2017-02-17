using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyRoomMainUIController : MonoBehaviour {

    public static MyRoomMainUIController instance;

    public GameObject shopPanel;
    public GameObject shopMSButton;

    public Text stars;
    public GameObject notEnoughStarsNotification;


   
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetCurrentStars();
    }
   

    public void SetCurrentStars()
    {
        stars.text = MyRoomController.instance.currentStars.ToString();
    }

    public void CallFlashNotEnoughStars()
    {
        StartCoroutine("FlashNotEnoughStars");
    }
    IEnumerator FlashNotEnoughStars()
    {
        notEnoughStarsNotification.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        notEnoughStarsNotification.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        notEnoughStarsNotification.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        notEnoughStarsNotification.SetActive(false);       
    }

    public void ClearAllItems()
    {
        SaveLoadMyRoom.instance.ClearRoom();
    }

    public void ShopActive(bool active)
    {
        shopPanel.SetActive(active);
        shopMSButton.SetActive(!active);
    }

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber, LoadSceneMode.Single);
    }


}
