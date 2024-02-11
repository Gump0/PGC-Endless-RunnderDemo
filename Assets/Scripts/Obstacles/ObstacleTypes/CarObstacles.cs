using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarObstacles : Obstacles
{
    public PlayerMovement playerMovement;// Instance of player move script to reference player speed :3

    protected override void MoveObstacle()
    {
    //     base.MoveObstacle();
    //     GameObject dynObst = GameObject.FindGameObjectWithTag("DynamicObstacle");
    }
}