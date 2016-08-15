using UnityEngine;
using System.Collections;

public class BackgroundSpawner : MonoBehaviour {

    public GameObject[] backgroundTiles;

    public GameObject groundTile;

    float previousXPos;
    float currentXPos;
  
    float previousYPos;
    float currentYPos;

    float previousGroundXPos;
    

    void Update()
    {
        SpawnSkySegments();
        SpawnGroundSegments();
    }
	
	
    
    void SpawnSkySegments()
    {
        currentXPos = transform.position.x;
        currentYPos = transform.position.y;
        if(currentXPos > previousXPos + 20 || currentYPos > previousYPos + 16 || currentYPos < previousYPos - 16)
        {
            Vector3 horizSpawnLoc = new Vector3(currentXPos + 20, transform.position.y);
            GameObject newTileHoz;
            newTileHoz = (GameObject)Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], horizSpawnLoc, transform.rotation);
            newTileHoz.transform.parent = GameObject.Find("Background").transform;
                  
            Vector3 vertSpawnLoc = new Vector3(transform.position.x, currentYPos + 16);
            GameObject newTileVert;
            newTileVert = (GameObject)Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], vertSpawnLoc, transform.rotation);
            newTileVert.transform.parent = GameObject.Find("Background").transform;

            Vector3 diagSpawnLoc = new Vector3(currentXPos + 20, currentYPos + 16);
            GameObject newTileDiag;
            newTileDiag = (GameObject)Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], diagSpawnLoc, transform.rotation);
            newTileDiag.transform.parent = GameObject.Find("Background").transform;

            Vector3 vertNegativeSpawnLoc = new Vector3(transform.position.x, currentYPos - 16);
            GameObject newTileVertNeg;
            newTileVertNeg = (GameObject)Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], vertNegativeSpawnLoc, transform.rotation);
            newTileVertNeg.transform.parent = GameObject.Find("Background").transform;

            Vector3 diagNegativeSpawnLoc = new Vector3(currentXPos + 20, currentYPos - 16);
            GameObject newTileDiagNeg;
            newTileDiagNeg = (GameObject)Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], diagNegativeSpawnLoc, transform.rotation);
            newTileDiagNeg.transform.parent = GameObject.Find("Background").transform;

            previousXPos = currentXPos;
            previousYPos = currentYPos;
        }        
    }

    void SpawnGroundSegments()
    {        
        if(currentXPos > previousGroundXPos + 62)
        {
            Vector3 spawnLoc = new Vector3(currentXPos + 62, -7);
            GameObject newGround;
            newGround = (GameObject)Instantiate(groundTile, spawnLoc, transform.rotation);
            newGround.transform.parent = GameObject.Find("Background").transform;

            previousGroundXPos = currentXPos;
        }        
    }
}
