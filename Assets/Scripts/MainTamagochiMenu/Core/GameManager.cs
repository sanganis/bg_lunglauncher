using UnityEngine;
using System.Collections;

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
	

}
