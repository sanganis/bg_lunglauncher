using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager _manager = null;
    public static GameManager manager
    {
        get { return _manager; }
    }

	
	void Awake ()
    {
        if(manager != null && manager != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _manager = this;
        }
        DontDestroyOnLoad(this.gameObject);
	}
	
	public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber, LoadSceneMode.Single);
    }


}
