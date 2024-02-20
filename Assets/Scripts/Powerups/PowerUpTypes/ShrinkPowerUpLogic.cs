using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPowerUpLogic : PowerUpMono
{
    protected override void MovePowerUp()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            //Shrink Power-up stuff
        }
    }
}