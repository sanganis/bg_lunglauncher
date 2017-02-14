using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyRoomMainUIController : MonoBehaviour {

    public static MyRoomMainUIController instance;

    public Text stars;
    public GameObject notEnoughStarsNotification;
   
    void Awake()
    {
        instance = this;
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



    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber, LoadSceneMode.Single);
    }


}
