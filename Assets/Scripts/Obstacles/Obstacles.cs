using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    private ObstacleSpawner obstaclespawner; // Instance of ObstacleSpawner

    private float maxObjectCount = 1; // Maximum # of objects spawned in level
    private float destroyedObjectCount = 3; // Total Destroyed Objects throughout gamplay (by default its set to 3 to make the first object to spawn faster)
    private float maxObjectIncreaseInterval = 4; // Detirmines the how many destroyed objects it takes to increase the max object count

    // Set the ObstacleSpawner instance
    public void SetObstacleSpawner(ObstacleSpawner spawner)
    {
        obstaclespawner = spawner;
    }

    public void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        transform.Translate(Vector2.left * obstaclespawner.obstacleSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("GOBACKTOYOURCOUNTRY") && destroyedObjectCount < 4)
        {
            for(int i = 0; i < maxObjectCount; i++)
            {
                obstaclespawner.RespawnObstacle();
                Destroy(gameObject);
                destroyedObjectCount ++;
                
                if (destroyedObjectCount >= maxObjectIncreaseInterval)
                {
                    maxObjectCount++;
                }
            }
        }
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    } 
}
