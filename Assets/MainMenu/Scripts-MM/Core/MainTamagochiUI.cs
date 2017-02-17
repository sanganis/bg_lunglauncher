using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainTamagochiUI : MonoBehaviour {

    public GameObject setTriggersPanel;

    public Text starsCollected;


    void Start()
    {
        if (PlayerPrefs.HasKey("Stars"))
        {
            starsCollected.text = PlayerPrefs.GetInt("Stars").ToString();
        }            
    }


    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber, LoadSceneMode.Single);
    }

    public void TriggersPanel()
    {
        setTriggersPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
