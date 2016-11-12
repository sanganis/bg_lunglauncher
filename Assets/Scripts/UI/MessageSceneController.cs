using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessageSceneController : MonoBehaviour {

    public Text messageText;

    public TextAsset textFile;

    string[] allMessages;

    public float messageTime = 5f;

    	
	void Start ()
    {
        DivideUpMessages();
        SetRandomMessage();
        Invoke("StartNextScene", messageTime);
    }

    void DivideUpMessages()
    {
        allMessages = textFile.text.Split("*".ToCharArray());        
    }
	
	
    void SetRandomMessage()
    {
        messageText.text = allMessages[Random.Range(0, allMessages.Length)];
    }

    void StartNextScene()
    {
        GameManager.manager.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
