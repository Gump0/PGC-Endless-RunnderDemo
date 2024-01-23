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

    public int maxObjectCount = 1;
    public int destroyedObjCount;
    
    void Start()
    {
        Invoke("RespawnObstacle", 0.1f);
    }

    void Update()
    {
        UpdateObstacleSpeed();
    }

    public void RespawnManager()
    {
        if(destroyedObjCount >= 4)
        {
            maxObjectCount++;

            destroyedObjCount = 0;
            
            //ListOfObstacleObjects.Length = maxObjectCount;
        }
    }

    //The first obstacle is in charge of keeping count of 'destroyedObjCount'
    // public void SpawnFirstObstacle()
    // {
    //     FindRandomPoint();
    //     rand = Random.Range(0, ListOfObstacleObjects.Length);
    //     GameObject firstObject = Instantiate(ListOfObstacleObjects[rand], obstacleSpawnLocation, ListOfObstacleObjects[rand].transform.rotation);

    //     Obstacles obstacles = firstObject.GetComponent<Obstacles>();
    //     obstacles.isFirstObj = true;
    // }

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
                
                //Debug.Log("DOES THIS WORK? HELLO!!!! :dddd PLEASE DOOO");
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
        Vector2 randomSpawnOffset = Random.insideUnitCircle * randomSpawnRadiusModifier;
        obstacleSpawnLocation = new Vector3(randomSpawnOffset.x + transform.position.x, randomSpawnOffset.y + transform.position.y, transform.position.z);
    }
}
