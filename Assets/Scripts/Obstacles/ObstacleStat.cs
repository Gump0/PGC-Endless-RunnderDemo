using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Obstacle", menuName = "Level Obstacle")]
public class ObstacleStat : ScriptableObject
{
    public string obstacleName;
    
    public int obstacleSpawnSizeOffset;

    public bool obstacleIsStationary = true;
    public bool obstacletIsDestructable = true;

    public Sprite obstacleArtwork; // for art implementation
}
