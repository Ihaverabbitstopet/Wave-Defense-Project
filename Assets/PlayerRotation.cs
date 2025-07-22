using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationFlip : MonoBehaviour
{
    // A reference to the main camera, automatically assigned in Start
    private Camera mainCamera;

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main; 
    }

    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mouseScreenPosition = Input.mousePosition; 

        // Convert the mouse position from screen coordinates to world coordinates
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        // Ignore the z-axis for 2D calculations
        mouseWorldPosition.z = 0f; 

        // Calculate the direction vector from the player to the mouse
        Vector3 directionToMouse = mouseWorldPosition - transform.position; 

        // Determine if the mouse is to the left or right of the player
        if (directionToMouse.x < 0)
        {
            // Mouse is to the left, flip the player 180 degrees on the y-axis (face left)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f); 
        }
        else
        {
            // Mouse is to the right, reset rotation (face right)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
        }
    }
}