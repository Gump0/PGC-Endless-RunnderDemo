using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityCheck : MonoBehaviour
{
    public bool playerIsImmune;
    void Update(){
        if(playerIsImmune){
            Debug.Log(playerIsImmune);
        }
    }
}
