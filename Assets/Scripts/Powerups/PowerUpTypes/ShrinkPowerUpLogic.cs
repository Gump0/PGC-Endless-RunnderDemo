using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPowerUpLogic : PowerUpMono
{
    protected override void PowerUpSpecial(){
        Debug.Log("Shrink Player");
    }
}