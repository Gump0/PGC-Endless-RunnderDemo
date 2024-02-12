using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerMoveSpeed = 4;
    public float maximumPlayerAngle = 70, defualtPlayerAngle = 0, currentPlayerAngle,
    wheelRotationSpeed = 1.35f, rotationAngle; // REQUIRED values needed for rotation logic
    float elapsedBrakeTime;
    
    // Stuff needed for input timing
    [SerializeField]
    private float inputHoldDelay = 0.050f; 
    private float timeSinceLastInput; 
    void FixedUpdate()
    {
        PlayerRotate();
        MovePlayer(1.25f, 0.5f, 7f, 8.25f); // Values assosiated with this function corresponds to each float introduced in the function 'PlayerBrake()'  
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

    private void MovePlayer(float timeToBrake, float timeToReturn, float defaultConstrainValue, float maxBrakeDistance)
    {   
        transform.Translate(Vector2.right * playerMoveSpeed * Time.deltaTime, Space.Self);
        Vector3 updatedPlayerPosition = transform.position;

        if(Input.GetMouseButton(0))
        {
           elapsedBrakeTime += Time.deltaTime;
           float t = Mathf.Clamp01(elapsedBrakeTime/timeToBrake);
           updatedPlayerPosition.x = Mathf.Lerp(-defaultConstrainValue, -maxBrakeDistance, t);
        }
        else
        {
            elapsedBrakeTime = 0f;
            float t = Mathf.Clamp01(Time.deltaTime/timeToReturn);
            updatedPlayerPosition.x = Mathf.MoveTowards(updatedPlayerPosition.x, -defaultConstrainValue, timeToReturn * Time.deltaTime);
        }
        // Ensure the player does not exceed the defaultConstrainValue
        updatedPlayerPosition.x = Mathf.Clamp(updatedPlayerPosition.x, -maxBrakeDistance, -defaultConstrainValue);

        // Update transform
        transform.position = updatedPlayerPosition;
    }
}
