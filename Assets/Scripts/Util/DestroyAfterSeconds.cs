using UnityEngine;
using System.Collections;

public class DestroyAfterSeconds : MonoBehaviour {

    public float destroyAfter = 5f;

	
	void Start () {
        Invoke("Destroy", destroyAfter);
	}
	
	void Destroy()
    {
        Destroy(gameObject);
    }
}
