using UnityEngine;
using System.Collections;

public class BackgroundSpawner : MonoBehaviour {

    public GameObject[] backgroundTiles;

    public GameObject groundTile;

    public GameObject backgroundTileHolder;

    float previousXPos;
    float currentXPos;
  
    float previousYPos;
    float currentYPos;

    float previousGroundXPos;

    public float skyXOffset;
    public float skyYOffset;
    public float groundXOffset = 62;

    
    void Update()
    {
        //SpawnSkySegments();
        SpawnGroundSegments();
    }
	
	
    
    void SpawnSkySegments()
    {
        currentXPos = transform.position.x;
        currentYPos = transform.position.y;
        if(currentXPos > previousXPos + skyXOffset || currentYPos > previousYPos + skyYOffset || currentYPos < previousYPos - skyYOffset)
        {
            Vector3 horizSpawnLoc = new Vector3(currentXPos + skyXOffset, transform.position.y);
            GameObject newTileHoz;
            newTileHoz = (GameObject)Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], horizSpawnLoc, transform.rotation);
            newTileHoz.transform.parent = backgroundTileHolder.transform;
                  
            Vector3 vertSpawnLoc = new Vector3(transform.position.x, currentYPos + skyYOffset);
            GameObject newTileVert;
            newTileVert = (GameObject)Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], vertSpawnLoc, transform.rotation);
            newTileVert.transform.parent = backgroundTileHolder.transform;

            Vector3 diagSpawnLoc = new Vector3(currentXPos + skyXOffset, currentYPos + skyYOffset);
            GameObject newTileDiag;
            newTileDiag = (GameObject)Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], diagSpawnLoc, transform.rotation);
            newTileDiag.transform.parent = backgroundTileHolder.transform;

            Vector3 vertNegativeSpawnLoc = new Vector3(transform.position.x, currentYPos - skyYOffset);
            GameObject newTileVertNeg;
            newTileVertNeg = (GameObject)Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], vertNegativeSpawnLoc, transform.rotation);
            newTileVertNeg.transform.parent = backgroundTileHolder.transform;

            Vector3 diagNegativeSpawnLoc = new Vector3(currentXPos + skyXOffset, currentYPos - skyYOffset);
            GameObject newTileDiagNeg;
            newTileDiagNeg = (GameObject)Instantiate(backgroundTiles[Random.Range(0, backgroundTiles.Length)], diagNegativeSpawnLoc, transform.rotation);
            newTileDiagNeg.transform.parent = backgroundTileHolder.transform;

            previousXPos = currentXPos;
            previousYPos = currentYPos;
        }        
    }
        

    void SpawnGroundSegments()
    {        
        if(currentXPos > previousGroundXPos + groundXOffset)
        {
            Vector3 spawnLoc = new Vector3(currentXPos + groundXOffset, -8);
            GameObject newGround;
            newGround = (GameObject)Instantiate(groundTile, spawnLoc, transform.rotation);
            newGround.transform.parent = backgroundTileHolder.transform;

            previousGroundXPos = currentXPos;
        }        
    }
}
