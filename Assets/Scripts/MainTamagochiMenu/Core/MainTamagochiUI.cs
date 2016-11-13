using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainTamagochiUI : MonoBehaviour {

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber, LoadSceneMode.Single);
    }
}
