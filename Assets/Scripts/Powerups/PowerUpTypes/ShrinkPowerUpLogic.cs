using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPowerUpLogic : PowerUpMono // inheriting from 'PowerUpMono'
{
    private Vector3 defaultPlayerScale = new Vector3(0.25f,0.25f,0.25f); // Defualt scale value
    private Vector3 shrunkenPlayerScale = new Vector3(0.125f,0.125f,0.125f); // Shrunken scale value
    
    [SerializeField] private float elapsedTime, timeToShrink, maxShrinkTime, t; // Time related float values
    [SerializeField] private GameObject playerObj; // ref to player object
    private bool isFullyShrunk; // used to track timing

    private WaitForSeconds delay = new WaitForSeconds(0.5f); // Delay of 1 second

    void Start(){
        playerObj = GameObject.Find("Player");
    }

    protected override void PowerUpSpecial(){
        StartCoroutine(ShrinkPlayer());
        Debug.Log("Shrink Player");
    }

    private IEnumerator ShrinkPlayer(){
        
        elapsedTime = 0f;

        while(elapsedTime < timeToShrink)
        {
            elapsedTime++;
            t = elapsedTime / timeToShrink;
            playerObj.transform.localScale = Vector3.Lerp(defaultPlayerScale, shrunkenPlayerScale, t);

            if(t >= 1){
                StartCoroutine(ReturnPlayer());
            }
            yield return delay;
        }
    }

    private IEnumerator ReturnPlayer(){
                
        elapsedTime = 0f;

        while(elapsedTime < maxShrinkTime)
        {
            elapsedTime++;
            if(elapsedTime >= maxShrinkTime)
            {
                elapsedTime++;
                t = elapsedTime / timeToShrink;
                playerObj.transform.localScale = Vector3.Lerp(shrunkenPlayerScale, defaultPlayerScale, t);
            }
            yield return delay;
        }
    }
}