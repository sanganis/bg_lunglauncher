using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTriggersPanel : MonoBehaviour {

    [SerializeField]
    Image[] buttonImage;

    void OnEnable()
    {
        for(int i = 0; i < buttonImage.Length; i++)
        {
            SetButtonColor(i);
        }
    }
	
    public void SetTrigger(int triggerNumber)
    {
        GameManager.instance.ToggleTrigger(triggerNumber);
        SetButtonColor(triggerNumber);
    }



    void SetButtonColor(int triggerNumber)
    {        
        switch (triggerNumber)
        {
            case 0:
                if (PlayerPrefs.HasKey("CatTrigger"))
                {
                    buttonImage[0].color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
                else
                {
                    buttonImage[0].color = new Color(1, 1, 1, 1);
                }
                break;
            case 1:
                if (PlayerPrefs.HasKey("CigaretteTrigger"))
                {
                    buttonImage[1].color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
                else
                {
                    buttonImage[1].color = new Color(1, 1, 1, 1);
                }
                break;
            case 2:
                if (PlayerPrefs.HasKey("DumbellTrigger"))
                {
                    buttonImage[2].color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
                else
                {
                    buttonImage[2].color = new Color(1, 1, 1, 1);
                }
                break;
            case 3:
                if (PlayerPrefs.HasKey("DustTrigger"))
                {
                    buttonImage[3].color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
                else
                {
                    buttonImage[3].color = new Color(1, 1, 1, 1);
                }
                break;
            case 4:
                if (PlayerPrefs.HasKey("DogTrigger"))
                {
                    buttonImage[4].color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
                else
                {
                    buttonImage[4].color = new Color(1, 1, 1, 1);
                }
                break;
            case 5:
                if (PlayerPrefs.HasKey("SweatTrigger"))
                {
                    buttonImage[5].color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
                else
                {
                    buttonImage[5].color = new Color(1, 1, 1, 1);
                }
                break;
            case 6:
                if (PlayerPrefs.HasKey("VirusTrigger"))
                {
                    buttonImage[6].color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
                else
                {
                    buttonImage[6].color = new Color(1, 1, 1, 1);
                }
                break;
            case 7:
                if (PlayerPrefs.HasKey("CleanerTrigger"))
                {
                    buttonImage[7].color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
                else
                {
                    buttonImage[7].color = new Color(1, 1, 1, 1);
                }
                break;
        }
    }

    public void BackButton()
    {
        this.gameObject.SetActive(false);
    }
       

}
