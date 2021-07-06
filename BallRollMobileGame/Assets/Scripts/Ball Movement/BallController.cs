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


    [SerializeField] private AudioSource rollSource;
    Rigidbody rb;
    private float maxSpeed = 10f;

    private void Awake()
    {
        ballMovementBehaviour = GetComponent<IBallMovementBehaviour>();
        rb = GetComponent<Rigidbody>();
        if (ballMovementBehaviour == null)
            Debug.LogWarning(gameObject.name + ": no IBallMovementBehaviour component found! Ball will behave improperly...");
    }

    private void FixedUpdate()
    {
        ballMovementBehaviour.ForwardMovement();
    }

    private void Update()
    {
        ballMovementBehaviour.PushBall();


        float speed = rb.velocity.magnitude;
        float scaled = speed / maxSpeed;

        scaled = Mathf.Clamp(scaled, 0, 1);
        rollSource.pitch = scaled;

        rollSource.outputAudioMixerGroup.audioMixer.SetFloat("pitchShift", 1 / scaled);
    }

    public void SetLevelSpeed(float speed)
    {
        Debug.Log("Set speed = " + speed);
        ballMovementBehaviour.SetLevelSpeed(speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Path") && !rollSource.isPlaying)
        {
            Debug.Log("Playing roll sound");
            rollSource.Play();

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Path") && rollSource.isPlaying)
        {
            Debug.Log("Stopping roll sound");
            rollSource.Stop();
        }
    }
}
