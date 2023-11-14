using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCollide : PlayerMovement
{
    private PlayerMovement playerMovement; // Instance of PlayerMovement

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            // Invert the Z-axis rotation
            float newRotationZ = -transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0f, 0f, newRotationZ);

            // Accessing upCount and downCount through the instance
            playerMovement.upCount *= -1;
            playerMovement.downCount *= -1;
        }
    }
}
