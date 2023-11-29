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
    private float obstacleSpawnLocationx;
    private float obstacleSpawnLocationMaxx = 9;
    private float obstacleSpawnLocationy;
    private float obstacleSpawnLocationMaxy = 5;

    // More spawning algorithm stuff
    //TEMP SERIALIZEFEILD FOR TESTING
    [SerializeField] private float maxObjectCount = 1; // Maximum # of objects spawned in level
    //TEMP SERIALIZEFEILD FOR TESTING
    [SerializeField] private float destroyedObjectCount = 3; // Total Destroyed Objects throughout gamplay (by default its set to 3 to make the first object to spawn faster)
    //TEMP SERIALIZEFEILD FOR TESTING
    [SerializeField] private float maxObjectIncreaseInterval = 4; // Detirmines the how many destroyed objects it takes to increase the max object count
    public int maxGlobalObjectCount = 12; // Used as a hard cap for how many object instances can exist at a time.
    
    void Start()
    {
        Invoke("RespawnObstacle", 0.1f);
    }

    void Update()
    {
        UpdateObstacleSpeed();
    }

    public void RespawnObstacle()
    {
        FindRandomPoint();
        destroyedObjectCount++;

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

            if (destroyedObjectCount >= maxObjectIncreaseInterval)
            {
                if(maxObjectCount >= maxGlobalObjectCount)
                {
                    maxObjectCount = maxGlobalObjectCount;
                }
                maxObjectCount++;
                destroyedObjectCount = 0;
                break;
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
        obstacleSpawnLocation = transform.position;

        obstacleSpawnLocationx = Random.Range(0, obstacleSpawnLocationMaxx);
        obstacleSpawnLocationy = Random.Range(-6, obstacleSpawnLocationMaxy);

        obstacleSpawnLocation = new Vector3(obstacleSpawnLocationx, obstacleSpawnLocationy, 0f) + transform.position;
    }
}
