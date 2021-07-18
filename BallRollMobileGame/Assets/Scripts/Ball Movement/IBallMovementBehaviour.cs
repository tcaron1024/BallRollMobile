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
    [Tooltip("The ball's speed increase per-second. Will be applied to move the ball forward.")]
    [SerializeField] private float ballSpeed = 1f;
    public float BallSpeed { get { return ballSpeed; } }

    // How fast the ball is travelling in the forward direction.
    private float currentForwardSpeed;
    public float CurrentForwardSpeed { get { return currentForwardSpeed; } }

    [Tooltip("A multiplier that affects how fast the ball is travelling in the forward direction.")]
    [SerializeField] private float speedMultiplier = 1;

    [Tooltip("A multiplier that affects how much force is applied to the ball when pushing it.")]
    [SerializeField] private float pushForceMultiplier = 1;


    // The initial force applied to the ball; the ball's starting speed.
    private float levelSpeed = 1;
    // TODO: Min and max speed?
    #endregion

    #region -- Height Clamping Fields --
    [Header("Height Clamping")]
    [Tooltip("How far a linecast should extend from the marble downward.")]
    [SerializeField] private float groundCheckDistance;

    [Tooltip("How many units above the marble should its y-position be clamped.")]
    [SerializeField] private float heightOffset;

    // The max y-position the marble can be at.
    private float maxYPos;
    #endregion

    private bool grounded = true;
    [SerializeField] private AudioSource rollSource;
    private float maxSpeed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        currentForwardSpeed = levelSpeed;
        rollSource = GameObject.FindGameObjectWithTag("Roll").GetComponent<AudioSource>();
        //rb.AddForce(Vector3.forward * currentForwardSpeed, ForceMode.VelocityChange);
    }

    protected virtual void FixedUpdate()
    {
        PerformGroundCheck();

        // Continually clamp the Y-pos.
        ClampYPos();

        // Change roll pitch/speed based on ball's speed
        float speed = rb.velocity.magnitude;
        float scaled = speed / maxSpeed;

        scaled = Mathf.Clamp(scaled, 0, 1);
        rollSource.pitch = scaled;

        rollSource.outputAudioMixerGroup.audioMixer.SetFloat("pitchShift", 1 / scaled);

        // Plays roll sound if ball is on ground, stops it if ball isnt on ground
        if (grounded)
        {
            if (!rollSource.isPlaying)
            {
                rollSource.Play();
            }
        }
        else
        {
            if (rollSource.isPlaying)
            {
                rollSource.Stop();
            }
        }
    }

    private void PerformGroundCheck()
    {
        grounded = Physics.Linecast(rb.position, rb.position + (Vector3.down * groundCheckDistance));
    }

    private void ClampYPos()
    {
        // Continually update the maxYPos as long as the marble is grounded.
        if (grounded)
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
        // We need a speed that is NOT influenced by friction and other level hinderences
        // if we want the ball to accelerate consistently.
        currentForwardSpeed += ballSpeed * speedMultiplier * Time.fixedDeltaTime;

        Vector3 vel = rb.velocity;
        vel.z = currentForwardSpeed;
        rb.velocity = vel;

        //rb.AddForce(Vector3.forward * currentForwardSpeed, ForceMode.Force);
    }

    /// <summary>
    /// Applies a force over the X-axis with the given signed magnitude.
    /// </summary>
    /// <param name="forceMagnitude">The signed magnitude of the force to apply to the ball over the X-axis.</param>
    protected void MoveBall(float forceMagnitude)
    {
        rb.AddForce(Vector3.right * forceMagnitude * pushForceMultiplier * Time.deltaTime, ForceMode.Force);
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


    private void OnDestroy()
    {
        if (rollSource != null)
        {
            // Stops roll sound from playing when ball is destroyed
            rollSource.Stop();
        }

    }
}