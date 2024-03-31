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
    IEnumerator ProcImmunity(){
        elapsedTime = 0f;
        while(elapsedTime < maxImmunityTime){
            immunityCheck.playerIsImmune = true;
        }
        immunityCheck.playerIsImmune = false;
        return null;
    }
}
