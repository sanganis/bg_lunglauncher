﻿using UnityEngine;
using System.Collections;

public class DestroyAfterDistance : MonoBehaviour {

    public float xDistanceFromPlayer = 200;

   void Update()
    {
        if (GameController.playerScreen.transform.position.x - transform.position.x > xDistanceFromPlayer)
        {
            Destroy(gameObject);
        }
    }
    
}
