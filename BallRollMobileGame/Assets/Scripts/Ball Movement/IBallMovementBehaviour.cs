/*
 * Created by: Kyle Grenier
 * Date: June 11th, 2021
 * Summary: A contract that each ball movement behaviour must implement.
 */
using UnityEngine;

public abstract class IBallMovementBehaviour : MonoBehaviour
{
    private Rigidbody rb;

    #region -- Movement Fields --

    [Tooltip("The ball's base speed. Will be applied to move the ball forward.")]
    [SerializeField] private float ballSpeed = 7f;
    public float BallSpeed { get { return ballSpeed; } }

    [Tooltip("A multiplier that affects how fast the ball is travelling in the forward direction.")]
    [SerializeField] private float speedMultiplier = 1;

    [Tooltip("A multiplier that affects how much force is applied to the ball when pushing it.")]
    [SerializeField] private float pushForceMultiplier = 1;


    private float levelSpeed = 1;
    // TODO: Min and max speed?
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.AddForce(Vector3.forward * 8, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Pushes the ball based on a unique method of input.
    /// </summary>
    public abstract void PushBall();

    /// <summary>
    /// Moves the ball forward.
    /// </summary>
    public void ForwardMovement()
    {
        rb.AddForce(Vector3.forward * ballSpeed * levelSpeed * speedMultiplier * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Applies a force over the X-axis with the given signed magnitude.
    /// </summary>
    /// <param name="forceMagnitude">The signed magnitude of the force to apply to the ball over the X-axis.</param>
    protected void MoveBall(float forceMagnitude)
    {
        rb.AddForce(Vector3.right * forceMagnitude * pushForceMultiplier, ForceMode.Force);
    }

    /// <summary>
    /// Updates the level speed to the given value.
    /// </summary>
    /// <param name="speed">The value to set the level speed to.</param>
    public void SetLevelSpeed(float speed)
    {
        print("BALL MOVEMENT: levelSpeed set to " + speed);
        levelSpeed = speed;
    }
}