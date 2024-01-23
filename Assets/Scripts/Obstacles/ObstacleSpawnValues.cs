using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleSpawnValues
{
    //Speed Increase Stuff//
    public int obstacleSpeed = 4, randomSpawnRadiusModifier, maxObjectCount = 1, destroyedObjCount;
    public float speedIncreaseInterval = 10, speedIncreaseTimer;

    //Samples
    [Range(0f,1f)]
    public float a, b, c;
}
