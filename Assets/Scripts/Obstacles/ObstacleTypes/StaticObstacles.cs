using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacles : Obstacles
{
    protected override void MoveObstacle()
    {
        base.MoveObstacle();
        transform.Translate(Vector2.left * obstaclespawner.obstacleSpeed * Time.deltaTime, Space.World);
    }
}