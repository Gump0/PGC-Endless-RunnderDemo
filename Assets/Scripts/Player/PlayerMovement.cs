using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerMoveSpeed = 4;
    public float maximumPlayerAngle = 70, defualtPlayerAngle = 0, currentPlayerAngle,
    wheelRotationSpeed = 0.65f, rotationAngle; // REQUIRED values needed for rotation logic
    
    private float inputHoldDelay, timeSinceLastInput; // Stuff needed for input timing
    void FixedUpdate()
    {
        MovePlayer();
        PlayerRotate();
        Debug.Log(currentPlayerAngle);
    }

    private void MovePlayer()
    {
        Vector3 playerConstrain = new Vector3(-7, transform.position.y, transform.position.z);
        transform.position = playerConstrain;
        transform.Translate(Vector2.right * playerMoveSpeed * Time.deltaTime, Space.Self);

        if(currentPlayerAngle > maximumPlayerAngle)
        {
            currentPlayerAngle = maximumPlayerAngle;
        }
        
        if(currentPlayerAngle < -maximumPlayerAngle)
        {
            currentPlayerAngle = -maximumPlayerAngle;
        }
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
                currentPlayerAngle += 10;
                timeSinceLastInput = Time.time;
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
                currentPlayerAngle -= 10;
                timeSinceLastInput = Time.time;
            }
        }
    }
}
