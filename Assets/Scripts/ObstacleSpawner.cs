using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] ObstacleObjects;
    public int rand;

    void Start()
    {
        Invoke("RespawnObstacle", 0.1f);
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
}
