using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDoublePowerUpLogic : PowerUpMono
{
    [SerializeField] private float pointIncreaseDuration, elaspedDoubleTime;
    private WaitForSeconds delay = new WaitForSeconds(0.5f); // Delay of 1 second
    [SerializeField] ScoreCount scoreCount; // Instance of scorecount

    protected override void PowerUpSpecial(){
        StartCoroutine(DoubleTimeCoroutine());
    }
    private IEnumerator DoubleTimeCoroutine(){
       elaspedDoubleTime = 0f;
       while(elaspedDoubleTime < 5){
        elaspedDoubleTime++;
        scoreCount.playerScoreCount++;
       }
       yield return delay;
    }
}
