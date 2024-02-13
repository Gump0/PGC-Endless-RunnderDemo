using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfCarSpawnable : MonoBehaviour
{
    public bool carIsAllowedToSpawn = true;

    public void CheckIfSpawnable()
    {
        if(carIsAllowedToSpawn)
        {
            carIsAllowedToSpawn = false;
            StartCoroutine(PutOnSpawnCooldown());
        }
    }

    IEnumerator PutOnSpawnCooldown()
    {
        yield return new WaitForSeconds(3f);
        carIsAllowedToSpawn = true;
    }
}
