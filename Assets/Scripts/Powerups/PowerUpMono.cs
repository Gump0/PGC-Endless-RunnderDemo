using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMono : MonoBehaviour
{
    public ObstacleSpawner obstaclespawner; // Instance of Obstacle Spawner Monobehavior to reference obstacle stats

    protected float powerUpVerticalSpeed;

    void Update()
    {
        MovePowerUp();
    }

    private void MovePowerUp()
    {

    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        //On trigger logic shared between all powerups (if thats the case lol)
    }
}
