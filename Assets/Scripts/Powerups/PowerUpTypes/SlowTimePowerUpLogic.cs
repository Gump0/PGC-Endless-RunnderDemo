using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowTImePowerUpLogic : PowerUpMono
{
    [SerializeField] Image slowTimeIndicator;
    private float elapsedTime, maxSlowTime = 8f;
    private int previousOBJSpeedValue, maxOBJSpeedSlow;
    private WaitForSeconds delay = new WaitForSeconds(1f); // Delay of 1 second
    private Color indicatorAlpha;

    void Start(){
        obstaclespawner = GameObject.Find("GameManager").GetComponent<ObstacleSpawner>();
        slowTimeIndicator = GameObject.Find("SlowSpeed-Indicator").GetComponent<Image>();
    }
    protected override void PowerUpSpecial(){
        StartCoroutine(SlowTimeNonchalantly());
    }
    IEnumerator SlowTimeNonchalantly(){
        elapsedTime = 0f;
        //Alpha change for UI (set to 100% opacity)
        indicatorAlpha.a = 1;
        slowTimeIndicator.color = indicatorAlpha;
        
        previousOBJSpeedValue = obstaclespawner.obstacleSpeed;
        maxOBJSpeedSlow = previousOBJSpeedValue / 2;
        while(elapsedTime < maxSlowTime){
            if(obstaclespawner.obstacleSpeed < maxOBJSpeedSlow){
                obstaclespawner.obstacleSpeed = maxOBJSpeedSlow;
            }
            yield return delay;
            elapsedTime++;
            obstaclespawner.obstacleSpeed = obstaclespawner.obstacleSpeed / 2;
        }
        obstaclespawner.obstacleSpeed = previousOBJSpeedValue;
        //Alpha change for UI (Set to 0% opacity)
        indicatorAlpha.a = 0;
        slowTimeIndicator.color = indicatorAlpha;
    }
}
