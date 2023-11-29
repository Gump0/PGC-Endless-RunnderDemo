using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    private ObstacleSpawner obstaclespawner; // Instance of ObstacleSpawner
    
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
        if(other.CompareTag("GOBACKTOYOURCOUNTRY"))
        {
            obstaclespawner.RespawnObstacle();
            Destroy(gameObject);
        }
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    } 
}
