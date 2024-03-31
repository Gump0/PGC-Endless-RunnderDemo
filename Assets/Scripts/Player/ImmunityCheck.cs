using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityCheck : MonoBehaviour
{
    public bool playerIsImmune;
    private SpriteRenderer sr;
    private Color oGColor;
    [SerializeField] private Color32[] colorArray;
    private float lerpTimer, lerpDuration = 0.25f;
    private int currentColorIndex;
    private Color targetColor;

    void Start(){
        sr = GetComponent<SpriteRenderer>();
        oGColor = sr.color;
    }
    void Update(){
        if(playerIsImmune == true){
            RainbowSheenOnImmunity();
        } else{
            sr.color = oGColor;
        }
    }
    void RainbowSheenOnImmunity(){
        targetColor = colorArray[currentColorIndex];
        sr.color = Color.Lerp(sr.color, targetColor, lerpTimer / lerpDuration);
        lerpTimer += Time.deltaTime;
        if(lerpTimer >= lerpDuration){
            lerpTimer = 0f;
            currentColorIndex = (currentColorIndex + 1) % colorArray.Length;
        }
    }
}