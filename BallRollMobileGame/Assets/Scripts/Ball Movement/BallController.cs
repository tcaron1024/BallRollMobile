using UnityEngine;

[RequireComponent(typeof(IBallMovementBehaviour))]
public class BallController : MonoBehaviour
{
    #region -- Ball Movement Fields --
    [Header("Ball Movement")]

    // The unique ball movement behaviour component.
    // Determines how pushing the ball around will be handled.
    private IBallMovementBehaviour ballMovementBehaviour;

    #endregion


    private void Awake()
    {
        ballMovementBehaviour = GetComponent<IBallMovementBehaviour>();
        if (ballMovementBehaviour == null)
            Debug.LogWarning(gameObject.name + ": no IBallMovementBehaviour component found! Ball will behave improperly...");
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
