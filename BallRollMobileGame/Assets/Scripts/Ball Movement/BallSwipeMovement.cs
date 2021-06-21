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
        // If the player begins to touch the screen, store the position of their touch.
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        // If the player has already begun touching the screen and is now in the process of performing
        // a swipe, take the touch distance as a force magnitude and apply a force to the ball.
        else if (Input.touchCount > 0)
        {
            currentTouchPosition = Input.GetTouch(0).position;

            // Get the distance between the two touches.
            Vector2 touchDistance = currentTouchPosition - startTouchPosition;
            float forceMagnitude = touchDistance.x;
            MoveBall(forceMagnitude);
        }
    }
}