using UnityEngine;
using System.Collections;

public class BackgroundSpawner : MonoBehaviour {

    public GameObject[] backgroundTiles;

    float previousXPos;
    float currentXPos;

    float previousYPos;
    float currentYPos;
    
    
    void Update()
    {
        SpawnBackgroundSegments();
    }
	
	
    
    void SpawnBackgroundSegments()
    {
        currentXPos = transform.position.x;
        currentYPos = transform.position.y;
        if(currentXPos > previousXPos + 20 || currentYPos > previousYPos + 16 || currentYPos < previousYPos - 16)
        {
            Vector3 horizSpawnLoc = new Vector3(currentXPos + 20, transform.position.y);
            Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], horizSpawnLoc, transform.rotation);
            
      
            Vector3 vertSpawnLoc = new Vector3(transform.position.x, currentYPos + 16);
            Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], vertSpawnLoc, transform.rotation);

            Vector3 diagSpawnLoc = new Vector3(currentXPos + 20, currentYPos + 16);
            Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], diagSpawnLoc, transform.rotation);

            Vector3 vertNegativeSpawnLoc = new Vector3(transform.position.x, currentYPos - 16);
            Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], vertNegativeSpawnLoc, transform.rotation);

            Vector3 diagNegativeSpawnLoc = new Vector3(currentXPos + 20, currentYPos - 16);
            Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], diagNegativeSpawnLoc, transform.rotation);

            previousXPos = currentXPos;
            previousYPos = currentYPos;
        }
        
    }
}
