using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//PRIMARY OBSTACLE CLASS//
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
    // void Start()
    // {
    //     obstaclespawner = GetComponent<ObstacleSpawner>();
    // }
    public void Update()
    {
        MoveObstacle(); // Constantly call and update MoveObstacle function
    }

    protected virtual void MoveObstacle()
    {
        if (obstaclespawner == null)
        {
            obstaclespawner = GetComponent<ObstacleSpawner>();
        }

        string obstTag = gameObject.tag;

        if (obstTag == "StaticObstacle")
        {
            StaticObstacles staticObstacle = GetComponent<StaticObstacles>();
            if (staticObstacle != null)
            {
                staticObstacle.MoveObstacle();
            }
            else
            {
                Debug.LogWarning("StaticObstacles component not found on the object with the tag 'StaticObstacle'.");
            }
        }
        else if (obstTag == "DynamicObstacle")
        {
            CarObstacles carObstacle = GetComponent<CarObstacles>();
            if (carObstacle != null)
            {
                carObstacle.MoveObstacle();
            }
            else
            {
                Debug.LogWarning("CarObstacles component not found on the object with the tag 'DynamicObstacle'.");
            }
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

    // public static Obstacles CreateObstaclesWithTag(string tag)
    // {
    //     if(tag == "StaticObstacle")
    //     {
    //         return new StaticObstacles();
    //     }
    //     else if(tag == "DynamicObstacle")
    //     {
    //         return new CarObstacles();
    //     }
    //     else{
    //         Debug.Log("Unkown obj tag: " +tag);
    //         return null;
    //     }
    // }
}
