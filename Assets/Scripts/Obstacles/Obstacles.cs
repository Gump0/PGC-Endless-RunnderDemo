using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//PRIMARY OBSTACLE CLASS//
public class Obstacles : MonoBehaviour
{
    public ObstacleSpawner obstaclespawner; // Instance of ObstacleSpawner
    private GameObject playerObjReference; // For some reason the game decided to not detect the player anymore!!! So we slap this here :D
    
    [SerializeField] private ImmunityCheck immunityCheck; // Lazy Immunity Patch :P
    
    public int destroyedObjCount;

    public bool isFirstObj;

    public AudioClip crashSound;
    public AudioSource audioForCrash;

    // Set the ObstacleSpawner instance
    public void SetObstacleSpawner(ObstacleSpawner spawner)
    {
        obstaclespawner = spawner;
    }

    void Start(){
        if(immunityCheck == null){
            immunityCheck = GameObject.Find("Player").GetComponent<ImmunityCheck>();
        }
        if(playerObjReference == null){
            playerObjReference = GameObject.Find("Player");
        }

        audioForCrash = GetComponent<AudioSource>();
        audioForCrash.clip = crashSound;
    }

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
        if(other.gameObject == playerObjReference && !immunityCheck.playerIsImmune)
        {
            audioForCrash.Play();
            TelemetryLogger.Log(this, "Died at this time", Time.deltaTime);
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    } 
}
