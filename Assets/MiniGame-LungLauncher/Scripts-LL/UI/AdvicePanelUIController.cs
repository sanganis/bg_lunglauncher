using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdvicePanelUIController : MonoBehaviour
{

    public Text messageText;

    public TextAsset textFile;

    string[] allMessages;

    public float messageMinTime = 2f;
    float activationTime;


    void OnEnable()
    {
        activationTime = Time.time;
        DivideUpMessages();
        SetRandomMessage();        
    }

    void Update()
    {
        if(Time.time > activationTime + messageMinTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                this.gameObject.SetActive(false);
            }
        }
    }


    void DivideUpMessages()
    {
        allMessages = textFile.text.Split("*".ToCharArray());
    }


    void SetRandomMessage()
    {
        messageText.text = allMessages[Random.Range(0, allMessages.Length)];
    }

    

}
