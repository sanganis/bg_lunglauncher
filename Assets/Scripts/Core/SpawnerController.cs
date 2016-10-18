using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour
{

    public Transform[] allSpawners;

    public EnemyBaseController[] allEnemies;

    public PowerupBaseController[] allPowerups;

    public GameObject[] allBackgroundObjects;

    public GameObject groundTile;

    public GameObject backgroundHolder;

    public GameObject player;

    // the % chance that a powerup will spawn after currentPowerupSpawnTime;
    public float powerupSpawnChance = 0.5f;

    public float startSpawningHeight = 3f;
    public float firstEnemySpawnIncreaseHeight = 100f, secondEnemySpawnIncreaseHeight = 200f, thirdEnemySpawnIncreaseHeight = 300f, fourthEnemySpawnIncreaseHeight = 400f;
    float currentPlayerHeight = 0;
    float currentEnemySpawnTime = 0;
    float currentPowerupSpawnTime = 0;

    // for determing how far the player has travelled, and when to spawn a new ground section
    public float groundXOffset = 62;
    float previousGroundXPos;
    float currentPlayerXPos;

    void Start()
    {        
        StartCoroutine("SpawnRandomEnemy");
        StartCoroutine("SpawnRandomPowerup");
        StartCoroutine("SpawnSkyObjects");
    }

    void Update()
    {
        currentPlayerHeight = player.transform.position.y;
        currentPlayerXPos = player.transform.position.x;
        SetSpawnTimeAccordingToHeight();
        SpawnGroundSegments();
    }

    void SetSpawnTimeAccordingToHeight()
    {
        if (currentPlayerHeight <= startSpawningHeight)
        {
            currentEnemySpawnTime = 0;
        }
        if (currentPlayerHeight > startSpawningHeight)
        {
            currentEnemySpawnTime = 2f;
            currentPowerupSpawnTime = 5f;
        }
        if (currentPlayerHeight > firstEnemySpawnIncreaseHeight)
        {
            currentEnemySpawnTime = 1.75f;
            currentPowerupSpawnTime = 4.75f;
        }
        if (currentPlayerHeight > secondEnemySpawnIncreaseHeight)
        {
            currentEnemySpawnTime = 1.5f;
            currentPowerupSpawnTime = 4.5f;
        }
        if (currentPlayerHeight > thirdEnemySpawnIncreaseHeight)
        {
            currentEnemySpawnTime = 1.25f;
            currentPowerupSpawnTime = 4.25f;
        }
        if (currentPlayerHeight > fourthEnemySpawnIncreaseHeight)
        {
            currentEnemySpawnTime = 1f;
            currentPowerupSpawnTime = 4f;
        }
    }


    // chooses randomly from one of the spawner transforms and instantiates a new enemy chosen by the method below
    IEnumerator SpawnRandomEnemy()
    {
        yield return new WaitForSeconds(currentEnemySpawnTime);
        if (currentEnemySpawnTime > 0)
        {
            Transform newSpawnLocation = allSpawners[Random.Range(0, allSpawners.Length)];
            Instantiate(ChooseRandomEnemyToSpawn(newSpawnLocation), newSpawnLocation.position, newSpawnLocation.rotation);            
        }
        StartCoroutine("SpawnRandomEnemy");
    }

    IEnumerator SpawnRandomPowerup()
    {
        yield return new WaitForSeconds(currentPowerupSpawnTime);
        if (currentPowerupSpawnTime > 0)
        {
            if (powerupSpawnChance > Random.Range(0f, 1f))
            {
                Transform newPowerupSpawnLocation = allSpawners[Random.Range(0, allSpawners.Length)];
                Instantiate(allPowerups[Random.Range(0, allPowerups.Length)], newPowerupSpawnLocation.position, newPowerupSpawnLocation.rotation);
            }
        }
        StartCoroutine("SpawnRandomPowerup");
    }


        // Randomly chooses from Enemies that should be spawnable at a given spawner location (bottom, side or top)
        // creates a temporary list and cycles through all the enemies, adding them to the list if they
        // have the bool checked in EnemyBaseController to be spawnable from that location
        // then randomly returns an enemy from that list
        EnemyBaseController ChooseRandomEnemyToSpawn(Transform newSpawnLocation)
    {
        List<EnemyBaseController> enemiesToSpawn = new List<EnemyBaseController>();
        if (newSpawnLocation.gameObject.tag == "BottomSpawner")
        {
            for (int i = 0; i < allEnemies.Length; i++)
            {
                if (allEnemies[i].spawnsAtBottom)
                {
                    enemiesToSpawn.Add(allEnemies[i]);
                }
            }
        }
        if (newSpawnLocation.gameObject.tag == "RightSideSpawner")
        {
            for (int i = 0; i < allEnemies.Length; i++)
            {
                if (allEnemies[i].spawnsAtRightSide)
                {
                    enemiesToSpawn.Add(allEnemies[i]);
                }
            }
        }
        if (newSpawnLocation.gameObject.tag == "TopSpawner")
        {
            for (int i = 0; i < allEnemies.Length; i++)
            {
                if (allEnemies[i].spawnsAtTop)
                {
                    enemiesToSpawn.Add(allEnemies[i]);
                }
            }
        }
        return enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];
    }


    IEnumerator SpawnSkyObjects()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        if (currentPlayerHeight > 10)
        {
            Transform newSpawnLocation = allSpawners[Random.Range(0, allSpawners.Length)];
            GameObject backgroundObject;
            backgroundObject = (GameObject)Instantiate(ChooseRandomBackgroundToSpawn(), newSpawnLocation.position, newSpawnLocation.rotation);
            backgroundObject.transform.parent = backgroundHolder.transform;
        }  
        StartCoroutine("SpawnSkyObjects");
    }

    GameObject ChooseRandomBackgroundToSpawn()
    {        
        return allBackgroundObjects[Random.Range(0, allBackgroundObjects.Length)];
    }

    void SpawnGroundSegments()
    {
        if (currentPlayerXPos > previousGroundXPos + groundXOffset)
        {
            Vector3 spawnLoc = new Vector3(currentPlayerXPos + groundXOffset, -8);
            GameObject newGround;
            newGround = (GameObject)Instantiate(groundTile, spawnLoc, transform.rotation);
            newGround.transform.parent = backgroundHolder.transform;

            previousGroundXPos = currentPlayerXPos;
        }
    }
}
