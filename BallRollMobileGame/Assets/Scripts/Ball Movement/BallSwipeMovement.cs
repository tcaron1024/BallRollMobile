/*
 * Created by: Kyle Grenier
 * Date: June 11th, 2021
 * Summary: Moves the ball via swiping on the screen.
 */
using UnityEngine;

public class BallSwipeMovement : IBallMovementBehaviour
{

    #region -- Swipe Control Fields --
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    #endregion

    private float forceMagnitude;

    protected override void Start()
    {
        base.Start();

        Input.multiTouchEnabled = false;
        Input.simulateMouseWithTouches = true;
    }


    /// <summary>
    /// Pushes the ball via swiping on the screen.
    /// </summary>
    public override void PushBall()
    {
        SwipeMovement();
    }

    /// <summary>
    /// Handles retrieving swipe input from the player.
    /// </summary>
    private void SwipeMovement()
    {
        const float SWIPE_MULTIPLIER = 200f;

        // If the player begins to touch the screen, store the position of their touch.
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        // If the player has already begun touching the screen and is now in the process of performing
        // a swipe, get the direction of the swipe and move the ball with a consistent force magnitude.
        else if (Input.touchCount > 0)
        {
            currentTouchPosition = Input.GetTouch(0).position;

            // Get a vector pointing in the direction of the swipe.
            Vector2 swipeVector = (currentTouchPosition - startTouchPosition);

            // I do this so I don't get values between 0 and 1 or -1 and 0 when swiping at an angle.
            int dir = (swipeVector.x > 0 ? 1 : -1);

            forceMagnitude = dir * SWIPE_MULTIPLIER;
        }
        else
            forceMagnitude = 0;
    }

    private void Update()
    {
        MoveBall(forceMagnitude);
    }
}