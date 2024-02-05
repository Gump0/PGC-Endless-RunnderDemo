using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform constrainObject;

    public float playerMoveSpeed = 4;
    public float maximumPlayerAngle = 70, defualtPlayerAngle = 0, currentPlayerAngle,
    wheelRotationSpeed = 1.35f, rotationAngle; // REQUIRED values needed for rotation logic
    
    private float inputHoldDelay, timeSinceLastInput; // Stuff needed for input timing
    void FixedUpdate()
    {
        PlayerRotate();
        MovePlayer(6f, 4f, 7f, 8.25f); // Values assosiated with this function corresponds to each float introduced in the function 'PlayerBrake()'  
    }

   private void PlayerRotate()
    {
        //Clamp rotation angle so that the players angle cannot surpass the maximum angle at any point
        float rotationAngle = Mathf.Clamp(currentPlayerAngle, -maximumPlayerAngle, maximumPlayerAngle);

        //Convert our values into a quaternion.euler in order to use it in transform.rotation
        Quaternion targetRotation = Quaternion.Euler(0, 0, rotationAngle);
    
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * wheelRotationSpeed);

        // UP INPUT
        if(Input.GetKeyDown("w") && currentPlayerAngle != maximumPlayerAngle)
        {
            currentPlayerAngle += 10;
        }
        if(Input.GetKey("w") && currentPlayerAngle != maximumPlayerAngle)
        {
            if(Time.time - timeSinceLastInput > inputHoldDelay)
            {
                timeSinceLastInput = Time.time;
                currentPlayerAngle += 10;
            }
        }

        // DOWN INPUT
        if(Input.GetKeyDown("s") && currentPlayerAngle != -maximumPlayerAngle)
        {
            currentPlayerAngle -= 10;
        }
        if(Input.GetKey("s") && currentPlayerAngle != -maximumPlayerAngle)
        {
            if(Time.time - timeSinceLastInput > inputHoldDelay)
            {
                timeSinceLastInput = Time.time;
                currentPlayerAngle -= 10;
            }
        }
    }

    private void MovePlayer(float brakeSpeed, float returnspeed, float defaultConstrainValue, float maxBrakeDistance)
    {   
        //Apply repeated force to the player allowing them to turn when rotating
        transform.Translate(Vector2.right * playerMoveSpeed * Time.deltaTime, Space.Self);

        // Takes player transform value and creates a new Vector3 value
        // with the created 'updatedPlayerPosition' we assign the constrain objects x value
        Vector3 updatedPlayerPosition = transform.position;
        updatedPlayerPosition.x = constrainObject.transform.position.x;
        // And apply it here :D
        transform.position = updatedPlayerPosition;
        float timeInterprelation;

        if(Input.GetMouseButton(0))
        {
            timeInterprelation = Mathf.Lerp(0f, 1f, brakeSpeed * Time.deltaTime);
            // set updatedPlayerPosition.x to -8.25f overtime
            updatedPlayerPosition.x = Mathf.Lerp(updatedPlayerPosition.x, -maxBrakeDistance, timeInterprelation);
        }
        else
        {
            timeInterprelation = Mathf.Lerp(0f, 1f, returnspeed * Time.deltaTime);
            // set updatedPlayerPosition.x to -7f overtime
            updatedPlayerPosition.x = Mathf.Lerp(updatedPlayerPosition.x, -defaultConstrainValue, timeInterprelation);
        }
        // Update transform
        transform.position = updatedPlayerPosition;
    }
}
