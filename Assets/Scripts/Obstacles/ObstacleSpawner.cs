using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] ObstacleObjects;
    public int rand;

    //Speed Increase Stuff//
    public int obstacleSpeed = 4;
    public float speedIncreaseInterval = 10;
    private float speedIncreaseTimer;

    //Spawn Algorithm Stuff//
    private Vector3 obstacleSpawnLocation;
    public int randomSpawnRadiusModifier;

    // More spawning algorithm stuff
    //TEMP SERIALIZEFEILD FOR TESTING
    [SerializeField] private int maxObjectCount = 1; // Maximum # of objects spawned in level
    public int destroyedObjectCount = 3; // Total Destroyed Objects throughout gamplay (by default its set to 3 to make the first object to spawn faster)
    //TEMP SERIALIZEFEILD FOR TESTING
    [SerializeField] private int maxObjectIncreaseInterval = 4; // Detirmines the how many destroyed objects it takes to increase the max object count
    public int maxGlobalObjectCount = 12; // Used as a hard cap for how many object instances can exist at a time.

    private bool hasCompletedFirstSpawn = false;
    
    void Start()
    {
        if(!hasCompletedFirstSpawn)
        {
            Invoke("RespawnManager", 0.1f);
        }
    }

    void Update()
    {
        UpdateObstacleSpeed();
    }

    public void RespawnManager()
    {
        switch(destroyedObjectCount)
        {
            //// IN THE CASE THAT DESTROYED OBJ COUNT DOESN'T MOG ANY OTHER VALUE :P ////
            case int destroyedObjectCount when destroyedObjectCount < maxObjectIncreaseInterval:
            Debug.Log("Spawn Object Without Increasing Count");
            hasCompletedFirstSpawn = true;
            
            for(int i = 0; i < maxObjectCount; i++)
            {
                RespawnObstacle(); // Execute "RespawnObstacle()" dependant of the "maxObjectCount" value
            }
            break;
            
            //// IN THE CASE THAT DESTROYED OBJECT COUNT EXCEEDS INCRASE INTERVAL, BUMP THE MAX OBJ COUNT!! ////
            case int destroyedObjectCount when destroyedObjectCount >= maxObjectIncreaseInterval:
            Debug.Log("Spawn Object As Well As Increase Max Obstacle Count");
            destroyedObjectCount = 0; // Reset total count
            maxObjectCount++; // Increase max obstacle count

            for(int i = 0; i < maxObjectCount; i++)
            {
                RespawnObstacle(); // Execute "RespawnObstacle()" dependant of the "maxObjectCount" value
            }
            break;

            //// IN THE CASE THAT DESTROYED OBJ COUNT AND MAX OBJ COUNT EXCEEDS GLOBAL LIMIT ////
            case int destroyedObjectCount when destroyedObjectCount >= maxObjectIncreaseInterval && maxObjectCount >= maxGlobalObjectCount:
            Debug.Log("Spawn Obstacles Without Exceeding Global Spawn Limit");

            for(int i = 0; i < maxGlobalObjectCount; i++)
            {
                RespawnObstacle(); // Execute "RespawnObstacle()" dependant of the "maxGlobalObjectCount" value
            }
            break;
        }
    }

    private void RespawnObstacle()
    {
        FindRandomPoint();

        for(int i = 0; i < maxObjectCount; i++)
        {
            FindRandomPoint();
            rand = Random.Range(0, ObstacleObjects.Length);
            GameObject newObstacle = Instantiate(ObstacleObjects[rand], obstacleSpawnLocation, ObstacleObjects[rand].transform.rotation);
            
            // Set the Obstacles script's obstaclespawner
            Obstacles obstaclesScript = newObstacle.GetComponent<Obstacles>();
            if (obstaclesScript != null)
            {
                obstaclesScript.SetObstacleSpawner(this);
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
        Vector2 randomSpawnOffset = Random.insideUnitCircle * randomSpawnRadiusModifier;
        obstacleSpawnLocation = new Vector3(randomSpawnOffset.x + transform.position.x, randomSpawnOffset.y + transform.position.y, transform.position.z);
    }
}
