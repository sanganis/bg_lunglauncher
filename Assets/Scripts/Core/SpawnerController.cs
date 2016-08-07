using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour
{
    
    public Transform[] allSpawners;

    public EnemyBaseController[] allEnemies;

    public GameObject player;



    public float startSpawningHeight = 3f;
    public float firstEnemySpawnIncreaseHeight = 100f, secondEnemySpawnIncreaseHeight = 200f, thirdEnemySpawnIncreaseHeight = 300f, fourthEnemySpawnIncreaseHeight = 400f;
    float currentPlayerHeight = 0;
    float currentSpawnTime = 0;

    void Start()
    {
        // waits for a random ammount of time set between spawnTimeMin and spawnTimeMax before calling an enemy spawn
        StartCoroutine("SpawnRandomEnemy");
    }

    void Update()
    {
        currentPlayerHeight = player.transform.position.y;
        SetSpawnTimeAccordingToHeight();
    }

    void SetSpawnTimeAccordingToHeight()
    {
        if(currentPlayerHeight <= startSpawningHeight)
        {
            currentSpawnTime = 0;
        }
        if(currentPlayerHeight > startSpawningHeight)
        {
            currentSpawnTime = 2f;
        }
        if (currentPlayerHeight > firstEnemySpawnIncreaseHeight)
        {
            currentSpawnTime = 1.75f;
        }
        if (currentPlayerHeight > secondEnemySpawnIncreaseHeight)
        {
            currentSpawnTime = 1.5f;
        }
        if (currentPlayerHeight > thirdEnemySpawnIncreaseHeight)
        {
            currentSpawnTime = 1.25f;
        }
        if (currentPlayerHeight > fourthEnemySpawnIncreaseHeight)
        {
            currentSpawnTime = 1f;
        }
    }


    // chooses randomly from one of the spawner transforms and instantiates a new enemy chosen by the method below
    IEnumerator SpawnRandomEnemy()
    {
        yield return new WaitForSeconds(currentSpawnTime);
        if (currentSpawnTime > 0)
        {
            Transform newSpawnLocation = allSpawners[Random.Range(0, allSpawners.Length)];
            Instantiate(ChooseRandomEnemyToSpawn(newSpawnLocation), newSpawnLocation.position, newSpawnLocation.rotation);
        }
        StartCoroutine("SpawnRandomEnemy");
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

}
