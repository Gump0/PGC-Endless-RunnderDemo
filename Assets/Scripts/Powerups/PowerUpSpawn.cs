using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public GameObject[] ListOfPowerUps;

    //Time related stuff
    [SerializeField] private float lastTimeChecked, minimumSpawnTime;
    //Spawn-roll stuff
    [SerializeField] private int spawnPercentChance;

    [SerializeField] private Transform startPoint, endPoint;

    void Update(){
        SpawnPowerUpsRandomly();
        MovePowerUpSpawner();
    }

    void SpawnPowerUpsRandomly(){
        
        lastTimeChecked += Time.deltaTime;

        if(lastTimeChecked > minimumSpawnTime){
            SpawnPowerUpRoll();
            lastTimeChecked = 0f;
        }
    }

    private void SpawnPowerUpRoll(){
        
        float rollPowerUpSpawn = Random.Range(0f, 100f);

        if(rollPowerUpSpawn <= spawnPercentChance){
            Instantiate(ListOfPowerUps[Random.Range(0,ListOfPowerUps.Length)], transform.position, 
            Quaternion.identity);
        }
    }

    private void MovePowerUpSpawner(){
        float pingPongValue = Mathf.PingPong(Time.time, 1f);
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, pingPongValue);
    }
}
