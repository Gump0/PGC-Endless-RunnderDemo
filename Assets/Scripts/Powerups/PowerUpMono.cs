using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMono : MonoBehaviour
{
    public ObstacleSpawner obstaclespawner; // Instance of Obstacle Spawner Monobehavior to reference obstacle stats
    [SerializeField] ScoreCount scoreCount; // Instance of scorecount
    
    [SerializeField] protected float powerUpVerticalSpeed, horizontalMoveRatio;
        
    public bool isMovingUp;

    void Awake()
    {
        SetRandomBool();
    }

    void Update()
    {
        MovePowerUp();
    }

    protected virtual void MovePowerUp()
    {
        if(obstaclespawner == null)
        {
            obstaclespawner = GameObject.Find("SpawnerObject").GetComponent<ObstacleSpawner>();
        }
        //Logic for horizontal movement
        transform.Translate(Vector3.left * obstaclespawner.obstacleSpeed * horizontalMoveRatio * Time.deltaTime);

        //Logic for vertical movement
        switch(isMovingUp)
        {
            case true:
            transform.Translate(Vector3.up * powerUpVerticalSpeed * Time.deltaTime);
            break;

            case false:
            transform.Translate(Vector3.down * powerUpVerticalSpeed * Time.deltaTime);
            break;
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Wall"))
        {
            switch(isMovingUp)
            {
                case true:
                isMovingUp = false;
                break;

                case false:
                isMovingUp = true;
                break;
            }
        }
        if(other.CompareTag("Player")){
            PowerUpSpecial();
            TelemetryLogger.Log(this, "PowerUp Consumption", scoreCount.playerScoreCount);
        }
        //On trigger logic shared between all powerups (if thats the case lol)
    }

    protected virtual void PowerUpSpecial(){
        //PLACE CODE HERE FOR FOLLOWING UNIQUE POWERUP
    }

    private void SetRandomBool()
    {
        int randomValue = Random.Range(0,2);
        isMovingUp = randomValue == 1;
    }
}
