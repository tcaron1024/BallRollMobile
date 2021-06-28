/*****************************************************************************
// File Name :         Pyramid.cs
// Author :            Kyle Grenier
// Creation Date :     06/27/2021
//
// Brief Description : The Pyramid obstacle moves back and forth, attempting to bump into
                       the player and throw them off course.
*****************************************************************************/
using UnityEngine;
using System.Collections;

// We don't need to inherit from IColliderObstacle here since we're just moving
// it back and forth.
// I believe deriving from IColliderObstacle would just make everything less performant.
public class Pyramid : MonoBehaviour
{
    #region -- Ground Check Fields --
    [Tooltip("The Transforms that the ground check ray will extend from.")]
    [SerializeField] private Transform[] groundChecks;
    
    [Tooltip("The length of the ground check ray.")]
    [SerializeField] private float groundCheckDistance;

    // True if the Pyramid is completely on the platform.
    private bool completelyOnPlatform;

    #endregion

    #region -- Pyramid Movement Fields --
    // The direction the pyramid is moving in.
    private Vector3 movementDirection;

    [Tooltip("The speed of the Pyramid's movement.")]
    [SerializeField] private float movementSpeed;

    #endregion

    // Time (in seconds) per call to check if grounded.
    const float GROUNDCHECK_TIME = 0.25f;


    private void Awake()
    {
        completelyOnPlatform = true;
        AssignRandomDirection();
    }

    private void Start()
    {
        // Start performing a ground check.
        InvokeRepeating("PerformGroundCheck", 0f, GROUNDCHECK_TIME);
    }

    private void Update()
    {
        // Moves the Pyramid.
        Move();
    }

    /// <summary>
    /// Checks to see if the pyramid is completely on the platform.
    /// </summary>
    private void PerformGroundCheck()
    {
        foreach(Transform groundCheck in groundChecks)
        {
            completelyOnPlatform = Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance);

            // If we ever are NOT completely on the platform,
            // start moving in the opposite direction.
            if (!completelyOnPlatform)
            {
                InvertMovementDirection();
            }
        }
    }

    /// <summary>
    /// Translates the Pyramid in the appropriate direction.
    /// </summary>
    private void Move()
    {
        transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Inverts the direction of the Pyramid's movement.
    /// </summary>
    private void InvertMovementDirection()
    {
        print("INVERTING");
        movementDirection = -movementDirection;
    }

    /// <summary>
    /// Randomly assigns the Pyramid's movement direction
    /// to be left or right.
    /// </summary>
    private void AssignRandomDirection()
    {
        int rand = Random.Range(0, 2);
        movementDirection = (rand == 0 ? Vector3.right : Vector3.left);
    }
}