using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerMoveSpeed = 4;
    public float maximumPlayerAngle = 70, defualtPlayerAngle = 0, currentPlayerAngle,
    wheelRotationSpeed = 1.35f, rotationAngle; // REQUIRED values needed for rotation logic
    float elapsedBrakeTime;
    [SerializeField] private float rotationIncreaseVariable;
    public bool isBraking;
    
    // Stuff needed for input timing
    [SerializeField]
    private float inputHoldDelay = 0.050f; 
    private float timeSinceLastInput;

    //Used to for newly implemented bounce limit
    public bool canWallBounce;

    //Used for Telemetry Tracking related to APM tracking
    private float startTime, endtime = 5f;
    private int actionCount;
    
    void Start(){
        startTime = Time.time;
    }
    void FixedUpdate()
    {
        PlayerRotate();
        MovePlayer(1.25f, 0.5f, 7f, 8.25f); // Values assosiated with this function corresponds to each float introduced in the function 'PlayerBrake()'
        WallBounceLimit(45);  

        if(Time.time - endtime >= 60){
            APMCalculate();
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
            actionCount++;
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
            actionCount++;
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

        if(Input.GetMouseButtonDown(0)){
            actionCount++;
        }
        if(Input.GetMouseButton(0))
        {
           elapsedBrakeTime += Time.deltaTime;
           float t = Mathf.Clamp01(elapsedBrakeTime/timeToBrake);
           updatedPlayerPosition.x = Mathf.Lerp(-defaultConstrainValue, -maxBrakeDistance, t);
           isBraking = true;
        }
        else
        {
            elapsedBrakeTime = 0f;
            float t = Mathf.Clamp01(Time.deltaTime/timeToReturn);
            updatedPlayerPosition.x = Mathf.MoveTowards(updatedPlayerPosition.x, -defaultConstrainValue, timeToReturn * Time.deltaTime);
            isBraking = false;
        }
        // Ensure the player does not exceed the defaultConstrainValue
        updatedPlayerPosition.x = Mathf.Clamp(updatedPlayerPosition.x, -maxBrakeDistance, -defaultConstrainValue);

        // Update transform
        transform.position = updatedPlayerPosition;
    }

    // Can only wallbounce if the player is traveling with a high enough angle for a long enough peroid of time
    void WallBounceLimit(int angleBounceLimitValue)
    {
        if(currentPlayerAngle >= angleBounceLimitValue || currentPlayerAngle <= -angleBounceLimitValue){
            canWallBounce = true;
        }
        else{
            canWallBounce = false;
        }
    }
    
    void APMCalculate(float apmInterval = 60f){
        float elapsedAPMtime = Time.time - startTime;
        float apm = (actionCount/elapsedAPMtime) * apmInterval;

        TelemetryLogger.Log(this, "Actions Per Minuite", apm);

        actionCount = 0;
        startTime = Time.time;
        endtime = startTime;
    }
}
