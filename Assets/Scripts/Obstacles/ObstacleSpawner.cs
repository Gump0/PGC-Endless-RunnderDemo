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
        rand = Random.Range(0, ObstacleObjects.Length);
        GameObject newObstacle = Instantiate(ObstacleObjects[rand], transform.position, ObstacleObjects[rand].transform.rotation);
        
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
}
