using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IBallMovementBehaviour))]
public class BallController : MonoBehaviour
{
    #region -- Ball Movement Fields --
    [Header("Ball Movement")]

    // The unique ball movement behaviour component.
    // Determines how pushing the ball around will be handled.
    private IBallMovementBehaviour ballMovementBehaviour;

    [SerializeField] private float minMaxSpeedDiff = 4f;
    #endregion

    #region -- Swipe Controls --
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    #endregion

    //Shows the player how fast they are going
    public Slider speedReference;

    private float levelSpeed;

    private void Awake()
    {
        ballMovementBehaviour = GetComponent<IBallMovementBehaviour>();
        if (ballMovementBehaviour == null)
            Debug.LogWarning(gameObject.name + ": no IBallMovementBehaviour component found! Ball will behave improperly...");
    }

    void Start()
    {
        Input.multiTouchEnabled = false;
        Input.simulateMouseWithTouches = true;
    }

    private void FixedUpdate()
    {
        ballMovementBehaviour.ForwardMovement();
        ballMovementBehaviour.PushBall();
    }

    public void SetLevelSpeed(float speed)
    {
        ballMovementBehaviour.SetLevelSpeed(speed);
    }
}
