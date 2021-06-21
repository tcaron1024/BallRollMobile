/*
 * Created by: Kyle Grenier
 * Date: June 11th, 2021
 * Summary: A contract that each ball movement behaviour must implement.
 */
using UnityEngine;
using System.Collections;

public abstract class IBallMovementBehaviour : MonoBehaviour
{
    private Rigidbody rb;

    #region -- Movement Fields --
    [Header("Marble Movement")]
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

    [Header("Height Clamping")]
    [Tooltip("How far a linecast should extend from the marble downward.")]
    [SerializeField] private float groundCheckDistance;

    [Tooltip("How many units above the marble should its y-position be clamped.")]
    [SerializeField] private float heightOffset;

    // The max y-position the marble can be at.
    private float maxYPos;




    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        rb.AddForce(Vector3.forward * 8, ForceMode.VelocityChange);
    }

    protected virtual void FixedUpdate()
    {
        ClampYPos();
    }

    private void ClampYPos()
    {
        // Continually update the maxYPos as long as the marble is grounded.
        if (Physics.Linecast(rb.position, rb.position + (Vector3.down * groundCheckDistance)))
        {
            maxYPos = rb.position.y + heightOffset;
        }
        else
        {
            Vector3 pos = rb.position;

            // Clamp the marble's position between an arbitrarily low negative number and 
            // the maxYPos we calculated.
            pos.y = Mathf.Clamp(pos.y, -999, maxYPos);

            // If we hit our peak, make sure to stop our y-velocity or else
            // our momentum will keep us pinned to the ceiling.
            if (pos.y >= maxYPos)
            {
                StopAllCoroutines();
                StartCoroutine(DecreaseYVelocity());
                //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }

            rb.position = pos;
        }

    }

    private IEnumerator DecreaseYVelocity()
    {
        // The time is takes to lerp the y-velocity to 0.
        const float DECREASE_TIME = 0.3f;
        float currentTime = 0f;
        float startingVel = rb.velocity.y;

        while (currentTime < DECREASE_TIME)
        {
            currentTime += Time.deltaTime;

            // Lerp the y-vel.
            Vector3 vel = rb.velocity;
            vel.y = Mathf.Lerp(startingVel, 0f, currentTime / DECREASE_TIME);

            rb.velocity = vel;

            yield return null;
        }
        print("DONE");
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