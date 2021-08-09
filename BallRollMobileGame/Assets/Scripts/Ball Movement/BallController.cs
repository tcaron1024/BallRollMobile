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


    Rigidbody rb;

    private void OnEnable()
    {
        EventManager.OnPlayerDeath += KillBall;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= KillBall;
    }

    private void Awake()
    {
        ballMovementBehaviour = GetComponent<IBallMovementBehaviour>();
        rb = GetComponent<Rigidbody>();
        if (ballMovementBehaviour == null)
        {
            Debug.LogWarning("No IBallMovementBehaviour found. Force adding BallSwipeMovement.");
            ballMovementBehaviour = gameObject.AddComponent<BallSwipeMovement>();
        }
    }

    private void FixedUpdate()
    {
        ballMovementBehaviour.ForwardMovement();
    }

    private void Update()
    {
        ballMovementBehaviour.PushBall();
    }

    public void SetLevelSpeed(float speed)
    {
        Debug.Log("Set speed = " + speed);
        ballMovementBehaviour.SetLevelSpeed(speed);

    }

    // Kills player when they die
    private void KillBall()
    {
        Destroy(gameObject);
    }
}
