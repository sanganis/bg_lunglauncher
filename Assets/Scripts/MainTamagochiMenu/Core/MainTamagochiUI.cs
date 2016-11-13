using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainTamagochiUI : MonoBehaviour {

    public GameObject setTriggersPanel;

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber, LoadSceneMode.Single);
    }

    public void TriggersPanel()
    {
        setTriggersPanel.SetActive(true);
    }
}
