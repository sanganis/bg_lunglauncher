using UnityEngine;
using System.Collections;

public class DestroyAfterDistance : MonoBehaviour {

   void Update()
    {
        if (GameController.playerScreen.transform.position.x - transform.position.x > 100)
        {
            Destroy(gameObject);
        }
    }
    
}
