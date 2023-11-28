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
    private float obstacleSpawnLocationMaxy = 9;

    private float maxObjectCount;


    void Start()
    {
        Invoke("RespawnObstacle", 0.1f);
    }

    void Update()
    {
        UpdateObstacleSpeed();
        FindRandomPoint();
    }

    public void RespawnObstacle()
    {
        rand = Random.Range(0, ObstacleObjects.Length);
        GameObject newObstacle = Instantiate(ObstacleObjects[rand], obstacleSpawnLocation, ObstacleObjects[rand].transform.rotation);

        // Set the Obstacles script's obstaclespawner
        Obstacles obstaclesScript = newObstacle.GetComponent<Obstacles>();
        if (obstaclesScript != null)
        {
            obstaclesScript.SetObstacleSpawner(this);
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
        obstacleSpawnLocationy = Random.Range(0, obstacleSpawnLocationMaxy);

        obstacleSpawnLocation = new Vector3(obstacleSpawnLocationx, obstacleSpawnLocationy, 0f);
    }
}
