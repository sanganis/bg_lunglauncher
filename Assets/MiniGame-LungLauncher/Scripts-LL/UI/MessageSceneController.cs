using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessageSceneController : MonoBehaviour {

    public Text messageText;

    public TextAsset textFile;

    string[] allMessages;

    public float nextSceneTime = 10f;
    public float messageTime = 5f;

    	
	void Start ()
    {
        DivideUpMessages();
        SetRandomMessage();
        Invoke("SetRandomMessage", messageTime);
        Invoke("StartNextScene", nextSceneTime);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }

}
