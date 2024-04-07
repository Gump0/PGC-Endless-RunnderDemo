using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    private Animator animator;

    //Name of defined states in animator
    private const string pFoward = "PlayerForward", pUpwards = "PlayerUpwards", pDownwards = "PlayerDownwards", pBraking = "PlayerBrake";

    void Start(){
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    //Constantly Check what animation state the player should be in!
    void Update(){
        if(!playerMovement.isBraking){
            if(playerMovement.currentPlayerAngle > 30){
                TransitionToState(pUpwards);
            }
            if(playerMovement.currentPlayerAngle < -30){
                TransitionToState(pDownwards);
            }
            if(playerMovement.currentPlayerAngle < 29 && playerMovement.currentPlayerAngle > -29){
                TransitionToState(pFoward);
            }
        } else{
            TransitionToState(pBraking);
        }
    }
    private void TransitionToState(string stateName){
        animator.Play(stateName);
    }
}
