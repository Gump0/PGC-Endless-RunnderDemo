using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacles : Obstacles
{
    void Start()
    {
        if (obstaclespawner == null)
        {
            obstaclespawner = GameObject.Find("SpawnerObject").GetComponent<ObstacleSpawner>();
        }
    }
    protected override void MoveObstacle()
    {
        transform.Translate(Vector2.left * obstaclespawner.obstacleSpeed / 1.5f * Time.deltaTime, Space.World);
    }
}