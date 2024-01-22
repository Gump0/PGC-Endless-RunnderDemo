using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    public ObstacleSpawner obstaclespawner; // Instance of ObstacleSpawner

    public int destroyedObjCount;

    public bool isFirstObj;

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
        if (obstaclespawner == null)
        {
            obstaclespawner = GetComponent<ObstacleSpawner>();
        }

        if (obstaclespawner != null)
        {
            transform.Translate(Vector2.left * obstaclespawner.obstacleSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            Debug.LogError("ObstacleSpawner component not found.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("GOBACKTOYOURCOUNTRY"))
        {
            Destroy(gameObject);
            obstaclespawner.destroyedObjCount++;
            
            if(isFirstObj)
            {
                obstaclespawner.RespawnObstacle();
            }
            obstaclespawner.RespawnManager();
        }
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    } 
}
