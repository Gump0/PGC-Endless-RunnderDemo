using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerMoveSpeed = 4;
    public float playerRotate;
    private int upCount;
    private int downCount;
    public int movementInputCountLimit = 3;

    void Update()
    {
        MovePlayer();
        PlayerRotate();
    }

    private void MovePlayer()
    {
        Vector3 playerConstrain = new Vector3(-7, transform.position.y, transform.position.z);
        transform.position = playerConstrain;
        transform.Translate(Vector2.right * playerMoveSpeed * Time.deltaTime, Space.Self);
    }

   private void PlayerRotate()
    {
        if (Input.GetKeyDown(KeyCode.W) && upCount < movementInputCountLimit)
        {
            upCount++;
            transform.Rotate(0, 0, playerRotate, Space.Self);
            downCount--;
        }

        if (Input.GetKeyDown(KeyCode.S) && downCount < movementInputCountLimit)
        {
            downCount++;
            transform.Rotate(0, 0, -playerRotate, Space.Self);
            upCount--;
        }
    }
}
