using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    private ObstacleSpawner obstaclespawner; // Instance of ObstacleSpawner

    //public ObstacleStat obstacleStats; //Save this for scriptable object implementation
    
    void Start()
    {
        if (obstaclespawner != null)
        {
            int destroyedObjCount = obstaclespawner.destroyedObjectCount;
        }
    }

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
            obstaclespawner.RespawnManager();
            Destroy(gameObject);
        }
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    } 
}
