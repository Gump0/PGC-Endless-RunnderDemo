using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDoublePowerUpLogic : PowerUpMono
{
    [SerializeField] private Image doubleImageComponent;
    [SerializeField] private float pointIncreaseDuration, elaspedDoubleTime;
    private WaitForSeconds delay = new WaitForSeconds(1f); // Delay of 1 second
    private Color indicatorAlpha;

    void Start(){
        scoreCount = GameObject.Find("GameManager").GetComponent<ScoreCount>();
        doubleImageComponent = GameObject.Find("2X-DoubleScore-Indicator").GetComponent<Image>();
        indicatorAlpha = doubleImageComponent.color;
    }
    protected override void PowerUpSpecial(){
        StartCoroutine(DoubleTimeCoroutine());
    }
    private IEnumerator DoubleTimeCoroutine(){
       elaspedDoubleTime = 0f;
       //Alpha change for UI (set to 100% opacity)
       indicatorAlpha.a = 1;
       doubleImageComponent.color = indicatorAlpha;

       while(elaspedDoubleTime < 8){
        yield return delay;
        elaspedDoubleTime++;
        scoreCount.playerScoreCount++;  
       }
       //Alpha change for UI (Set to 0% opacity)
       indicatorAlpha.a = 0;
       doubleImageComponent.color = indicatorAlpha;
    }
}
