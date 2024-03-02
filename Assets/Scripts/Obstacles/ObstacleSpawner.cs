using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObstacleSpawnValues obstacleSpawnValues;
    public GameObject[] ListOfObstacleObjects;
    public int rand;

    //Speed Increase Stuff//
    public int obstacleSpeed = 4;
    public float speedIncreaseInterval = 10;
    private float speedIncreaseTimer;

    //Spawn Algorithm Stuff//
    private Vector3 obstacleSpawnLocation;
    public int randomSpawnRadiusModifier;
    private HashSet<Vector3> spawnedPositions = new HashSet<Vector3>(); // Store positions of spawned objects

    public int maxObjectCount = 1;
    public int destroyedObjCount;

    //Car Spawn Stuff
    public CheckIfCarSpawnable checkIfCarSpawnable; // Reference to car spawner script
    public float spawnChanceFactor;
    public GameObject[] spawnableCarArray;
    
    void Start()
    {
        Invoke("RespawnObstacle", 0.1f);
        InvokeRepeating("CallRollCarSpawn", 2, 7);
    }

    //Apparently Invoke methods cant call functions with parameters lol so imma do this for now
    private void CallRollCarSpawn()
    {
        RollCarSpawn(1, 8f, 25f);
    }

    void Update()
    {
        UpdateObstacleSpeed();
        // For car spawn alorithm
        spawnChanceFactor += Time.deltaTime;
    }

    public void RespawnManager()
    {
        if(destroyedObjCount >= 4)
        {
            //Hard caps number of maximum static objects to 5
            if (maxObjectCount < 5)
            {
                maxObjectCount++;
            }
            destroyedObjCount = 0;
        }
    }

    public void RespawnObstacle()
    {
        for(int i = 0; i < maxObjectCount; i++)
        {
            if(i == 0)
            {
                FindRandomPoint();
                rand = Random.Range(0, ListOfObstacleObjects.Length);
                GameObject newObstacle = Instantiate(ListOfObstacleObjects[rand], obstacleSpawnLocation, ListOfObstacleObjects[rand].transform.rotation);

                Obstacles obstacle = newObstacle.GetComponent<Obstacles>();
                Obstacles obstaclesScript = newObstacle.GetComponent<Obstacles>();

                if(obstacle != null)
                {
                    obstacle.isFirstObj = true;
                    obstaclesScript.SetObstacleSpawner(this);
                }
            }else
            {
                FindRandomPoint();
                rand = Random.Range(0, ListOfObstacleObjects.Length);
                GameObject newObstacle = Instantiate(ListOfObstacleObjects[rand], obstacleSpawnLocation, ListOfObstacleObjects[rand].transform.rotation);

                // Set the Obstacles script's obstaclespawner
                Obstacles obstaclesScript = newObstacle.GetComponent<Obstacles>();
                if (obstaclesScript != null)
                {
                    obstaclesScript.SetObstacleSpawner(this);
                }
            }
        }
    }

    private void UpdateObstacleSpeed()
    {
        speedIncreaseTimer += Time.deltaTime;
        if(speedIncreaseTimer >= speedIncreaseInterval)
        {
            obstacleSpeed++;
            speedIncreaseTimer = 0.0f;
            Debug.Log("Obstacle Velocity: " + obstacleSpeed);
        }
    }

    private void FindRandomPoint()
    {
        float minDistance = 2.0f; // Minimum distance between spawns
        float maxAttempts = 10; // Maximum attempts to find a suitable position
        
        for(int i = 0; i < maxAttempts; i++)
        {
            Vector2 randomSpawnOffset = Random.insideUnitCircle * randomSpawnRadiusModifier;
            obstacleSpawnLocation = new Vector3(randomSpawnOffset.x + transform.position.x, randomSpawnOffset.y + transform.position.y, transform.position.z);

            if(!IsPositionOccupied(obstacleSpawnLocation, minDistance))
            {
                spawnedPositions.Add(obstacleSpawnLocation);
                return;
            }
        }
    }

    private bool IsPositionOccupied(Vector3 position, float minDistance)
{
    foreach (Vector3 spawnedPosition in spawnedPositions)
    {
        if (Vector3.Distance(position, spawnedPosition) < minDistance)
        {
            return true;
        }
    }
    return false;
}


    private void RollCarSpawn(int maxNumberOfCars, float probabilityIncrease, float carRollProbability)
    {
        carRollProbability = carRollProbability + maxNumberOfCars + (spawnChanceFactor/10);
        float rand = Random.Range(0,100f);
        if(carRollProbability >= rand)
        {
            probabilityIncrease = 0;

            GameObject[] carSpawners = GameObject.FindGameObjectsWithTag("Spawner");

            foreach (GameObject carSpawner in carSpawners)
            {
                if(checkIfCarSpawnable.carIsAllowedToSpawn)
                {
                    checkIfCarSpawnable.CheckIfSpawnable();
                    Instantiate(spawnableCarArray[Random.Range(0, spawnableCarArray.Length)], carSpawners[Random.Range(0, carSpawners.Length)].transform.position, Quaternion.identity);
                    //Debug.Log("WEGOTACAR!");
                }
            }
        }
        else{
            probabilityIncrease += 5;
        }
    }
}
