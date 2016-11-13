using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour
{

    public Transform[] allSpawners;

    public EnemyBaseController[] allEnemies;

    public PowerupBaseController[] allPowerups;

    public GameObject[] clouds;

    public GameObject stars;

    public GameObject groundTile;

    public GameObject backgroundHolder;

    public GameObject player;

    // the % chance that a powerup will spawn after currentPowerupSpawnTime;
    public float powerupSpawnChance = 0.5f;

    public float startSpawningHeight = 3f;
    public float firstEnemySpawnIncreaseHeight = 100f, secondEnemySpawnIncreaseHeight = 200f, thirdEnemySpawnIncreaseHeight = 300f, fourthEnemySpawnIncreaseHeight = 400f;

    public float startingEnemySpawnTiming = 3f;
    public float startingPowerupSpawnTiming = 10f;
    float currentPlayerHeight = 0;
    float currentEnemySpawnTime = 0;
    float currentPowerupSpawnTime = 0;

    // for determing how far the player has travelled, and when to spawn a new ground section
    public float groundXOffset = 62;
    float previousGroundXPos;
    float currentPlayerXPos;

    public float spawnStarsHeight = 3000f;
    float cloudSpawnTime = 2f;
    float starSpawnTime = 0.5f;

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
            currentPowerupSpawnTime = 0;
        }
        if (currentPlayerHeight > startSpawningHeight)
        {
            currentEnemySpawnTime = startingEnemySpawnTiming;
            currentPowerupSpawnTime = startingPowerupSpawnTiming;
        }
        if (currentPlayerHeight > firstEnemySpawnIncreaseHeight)
        {
            currentEnemySpawnTime = startingEnemySpawnTiming - 0.25f;
            currentPowerupSpawnTime = startingPowerupSpawnTiming - 0.25f;
        }
        if (currentPlayerHeight > secondEnemySpawnIncreaseHeight)
        {
            currentEnemySpawnTime = startingEnemySpawnTiming - 0.5f;
            currentPowerupSpawnTime = startingPowerupSpawnTiming - 0.5f;
        }
        if (currentPlayerHeight > thirdEnemySpawnIncreaseHeight)
        {
            currentEnemySpawnTime = startingEnemySpawnTiming - 0.75f;
            currentPowerupSpawnTime = startingPowerupSpawnTiming - 0.75f;
        }
        if (currentPlayerHeight > fourthEnemySpawnIncreaseHeight)
        {
            currentEnemySpawnTime = startingEnemySpawnTiming - 1f;
            currentPowerupSpawnTime = startingPowerupSpawnTiming - 1f;
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

    // checks on the player's height before spawning background objects
    IEnumerator SpawnSkyObjects()
    {
        if (currentPlayerHeight > 10)
        {
            Vector2 spawnVector = allSpawners[Random.Range(0, allSpawners.Length)].position;            
            GameObject backgroundObject;
            if (currentPlayerHeight < spawnStarsHeight)
            {                
                backgroundObject = (GameObject)Instantiate(ChooseBackgroundToSpawn(), spawnVector, transform.rotation);
                backgroundObject.transform.parent = backgroundHolder.transform;
                yield return new WaitForSeconds(cloudSpawnTime);
            }
            if (currentPlayerHeight > spawnStarsHeight)
            {
                spawnVector = new Vector2(spawnVector.x + Random.Range(-2, 2), spawnVector.y + Random.Range(-2, 2));   
                backgroundObject = (GameObject)Instantiate(ChooseBackgroundToSpawn(), spawnVector, transform.rotation);
                backgroundObject.transform.parent = backgroundHolder.transform;
                yield return new WaitForSeconds(starSpawnTime);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine("SpawnSkyObjects");
    }

    // returns a random background object dependent on player's height
    GameObject ChooseBackgroundToSpawn()
    {
        if (currentPlayerHeight < spawnStarsHeight)
        {
            return clouds[Random.Range(0, clouds.Length)];
        }
        else
        {
            return stars;
        }
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
