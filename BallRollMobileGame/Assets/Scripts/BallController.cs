using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;

    #region -- Ball Movement Fields --
    [Header("Ball Movement")]

    public float startForce;
    public float ballSpeed = 7f;

    [Tooltip("A multiplier that affects how fast the ball is travelling.")]
    [SerializeField] private float speedMultiplier;

    [SerializeField] private float minMaxSpeedDiff = 4f;
    private float maxSpeed = 50.0f;
    private float minSpeed = 0.0f;
    #endregion

    #region -- Swipe Controls --
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    #endregion

    //Shows the player how fast they are going
    public Slider speedReference;

    private float levelSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Input.multiTouchEnabled = false;
        Input.simulateMouseWithTouches = true;
        //print(Vector3.forward * startForce);
        rb.AddForce(transform.forward * startForce);
        //Invoke("GetVelocityNormalized", 2f);

        //Setting up values for speed refernce
        speedReference.value = ballSpeed;
        speedReference.maxValue = maxSpeed;
        speedReference.minValue = minSpeed;

    }

    void Update()
    {
        SwipeMovement();
    }

    private void FixedUpdate()
    {
        ForwardMovement();
    }


    // Applies a forward force to the ball over time.
    private void ForwardMovement()
    {
        rb.AddForce(Vector3.forward * levelSpeed * ballSpeed * Time.fixedDeltaTime);
    }

    // Handle retrieving swipe input from the player.
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
            ApplyForceToBall(forceMagnitude);
        }
    }

    /// <summary>
    /// Applies a force to the ball. Used to control the ball via swipe controls.
    /// </summary>
    /// <param name="forceMagnitude">The magnitude of the force to apply to the ball.
    /// Force will be applied over the X-axis.</param>
    private void ApplyForceToBall(float forceMagnitude)
    {
        rb.AddForce(Vector3.right * forceMagnitude * speedMultiplier, ForceMode.Force);
        print(Vector3.right * forceMagnitude * speedMultiplier);
    }

    public void SetLevelSpeed(float speed)
    {
        levelSpeed = speed;
        print("level speeed set to " + speed);
    }
}
