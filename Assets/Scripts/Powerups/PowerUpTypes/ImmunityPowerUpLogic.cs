using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImmunityPowerUpLogic : PowerUpMono
{
    [SerializeField] ImmunityCheck immunityCheck;
    [SerializeField] Image immunityIndicator;
    private float elapsedTime, maxImmunityTime = 8f;
    private Color indicatorAlpha;

    void Start(){
        immunityIndicator = GameObject.Find("Immunity-Indicator").GetComponent<Image>();
        immunityCheck = GameObject.Find("Player").GetComponent<ImmunityCheck>();
    }
    protected override void PowerUpSpecial(){
        StartCoroutine(ProcImmunity());
    }
    public IEnumerator ProcImmunity(){
        elapsedTime = 0f;
        //Alpha change for UI (set to 100% opacity)
        indicatorAlpha.a = 1;
        immunityIndicator.color = indicatorAlpha;

        while(elapsedTime < maxImmunityTime){
            elapsedTime++;
            immunityCheck.playerIsImmune = true;
            Debug.Log(immunityCheck.playerIsImmune);
            yield return new WaitForSeconds(1);
        }
        immunityCheck.playerIsImmune = false;

        //Alpha change for UI (Set to 0% opacity)
        indicatorAlpha.a = 0;
        immunityIndicator.color = indicatorAlpha;
    }
}
