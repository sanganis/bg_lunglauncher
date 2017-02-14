using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Invoke("LoadNextScene", 5);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            CancelInvoke();
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

}