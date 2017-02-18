using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetTriggersPanel : MonoBehaviour
{

    private Image[] buttonImages;
    private GameObject[] buttonsChecks;
    private string[] PrefKeys;

    public GameObject[] buttons;

    void Start()
    {
        PrefKeys = new string[buttons.Length];
        buttonImages = new Image[buttons.Length];
        buttonsChecks = new GameObject[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonImages[i] = buttons[i].gameObject.GetComponent<Image>() as Image;
            GameObject CheckBox = GetChildGameObject(buttons[i].gameObject, "CheckBox") as GameObject;
            if (CheckBox != null) buttonsChecks[i] = GetChildGameObject(CheckBox, "Checked") as GameObject;
            PrefKeys[i] = buttons[i].name;
            SetButtonColor(i);
        }
    }


    static public GameObject GetChildGameObject(GameObject fromGameObject, string withName)
    {
        Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
        return null;
    }

    public void SetTrigger(int triggerNumber)
    {
        GameManager.instance.ToggleTrigger(triggerNumber);
        SetButtonColor(triggerNumber);
    }

    void SetButtonColor(int triggerNumber)
    {
        if (PlayerPrefs.HasKey(PrefKeys[triggerNumber]))
        {
            buttonImages[triggerNumber].color = new Color(0.714f, 1f, 0.647f, 1f);
            if (buttonsChecks[triggerNumber] != null) buttonsChecks[triggerNumber].SetActive(true);
        }
        else
        {
            buttonImages[triggerNumber].color = new Color(1, 1, 1, 1);
            if (buttonsChecks[triggerNumber] != null) buttonsChecks[triggerNumber].SetActive(false);
        }
        /*
        switch (triggerNumber)
        {
            case 0:
                if (PlayerPrefs.HasKey("CatTrigger"))
                {
                    buttonImage[0].color = new Color(0.714f, 1f, 0.647f, 1f);
                }
                else
                {
                    buttonImage[0].color = new Color(1, 1, 1, 1);
                }
                break;
            case 1:
                if (PlayerPrefs.HasKey("CigaretteTrigger"))
                {
                    buttonImage[1].color = new Color(0.714f, 1f, 0.647f, 1f);
                }
                else
                {
                    buttonImage[1].color = new Color(1, 1, 1, 1);
                }
                break;
            case 2:
                if (PlayerPrefs.HasKey("DumbellTrigger"))
                {
                    buttonImage[2].color = new Color(0.714f, 1f, 0.647f, 1f);
                }
                else
                {
                    buttonImage[2].color = new Color(1, 1, 1, 1);
                }
                break;
            case 3:
                if (PlayerPrefs.HasKey("DustTrigger"))
                {
                    buttonImage[3].color = new Color(0.714f, 1f, 0.647f, 1f);
                }
                else
                {
                    buttonImage[3].color = new Color(1, 1, 1, 1);
                }
                break;
            case 4:
                if (PlayerPrefs.HasKey("DogTrigger"))
                {
                    buttonImage[4].color = new Color(0.714f, 1f, 0.647f, 1f);
                }
                else
                {
                    buttonImage[4].color = new Color(1, 1, 1, 1);
                }
                break;
            case 5:
                if (PlayerPrefs.HasKey("SweatTrigger"))
                {
                    buttonImage[5].color = new Color(0.714f, 1f, 0.647f, 1f);
                }
                else
                {
                    buttonImage[5].color = new Color(1, 1, 1, 1);
                }
                break;
            case 6:
                if (PlayerPrefs.HasKey("VirusTrigger"))
                {
                    buttonImage[6].color = new Color(0.714f, 1f, 0.647f, 1f);
                }
                else
                {
                    buttonImage[6].color = new Color(1, 1, 1, 1);
                }
                break;
            case 7:
                if (PlayerPrefs.HasKey("CleanerTrigger"))
                {
                    buttonImage[7].color = new Color(0.714f, 1f, 0.647f, 1f);
                }
                else
                {
                    buttonImage[7].color = new Color(1, 1, 1, 1);
                }
                break;
        } */
    }

    public void BackButton()
    {
        this.gameObject.SetActive(false);
    }


}
