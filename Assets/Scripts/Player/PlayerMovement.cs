using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerMoveSpeed = 4;
    public float maximumPlayerAngle = 70, defualtPlayerAngle = 0, currentPlayerAngle, wheelRotationSpeed = 0.25f
    , rotationAngle; // REQUIRED values needed for rotation logic
    void Update()
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

        if(Input.GetKeyDown("w") && currentPlayerAngle != maximumPlayerAngle)
        {
            currentPlayerAngle += 10;
        }
        if(Input.GetKeyDown("s") && currentPlayerAngle != -maximumPlayerAngle)
        {
            currentPlayerAngle -= 10;
        }
    }
}
