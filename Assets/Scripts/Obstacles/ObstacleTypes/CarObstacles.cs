using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacles : Obstacles
{
    public PlayerMovement playerMovement;// Instance of player move script to reference player speed :3
    private float dynamicSpeed;

    void Start()
    {
        if (obstaclespawner == null)
        {
            obstaclespawner = GameObject.Find("SpawnerObject").GetComponent<ObstacleSpawner>();
        }
        if (playerMovement == null)
        {
            playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }
    }

    protected override void MoveObstacle()
    {
        // make speed difference different for each car
        // in reality not everyone is following the limit!
        float randFactor = Random.Range(-1f, 3f);
        float dynamicSpeed = (float)obstaclespawner.obstacleSpeed / 1.2f + randFactor - playerMovement.playerMoveSpeed;

        transform.Translate(Vector2.left * dynamicSpeed * Time.deltaTime, Space.World);
    }
}