using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    Rigidbody rb;

    public float startForce;
    public float ballSpeed = 7f;
    public float amountDivisor = 500f;

    [SerializeField] private float minMaxSpeedDiff = 4f;
    private float maxSpeed = 50.0f;
    private float minSpeed = 0.0f;

    private Vector2[] touchStartPositions = new Vector2[10];

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
        ForwardMovement();

        SwipeMovement();
    }


    private void ForwardMovement()
    {
        rb.AddForce(Vector3.forward * levelSpeed * ballSpeed * Time.deltaTime);   
    }

    private void SwipeMovement()
    {
        for (int i = 0; i < Input.touches.Length; i++)
        {
            if (Input.touches[i].phase == TouchPhase.Began)
            {
                touchStartPositions[i] = Input.touches[i].position;
            }
            else if (Input.touches[i].phase == TouchPhase.Ended || Input.touches[i].phase == TouchPhase.Canceled)
            {
                Vector2 direction = Input.touches[i].position - touchStartPositions[i];
                Debug.Log("direction.X = " + direction.x);


               // OLD CODE FROM SWIPING UP/DOWN TO GAIN/REDUCE SPEED

               // /*If ball is going under max speed, and the player has swiped
               //upwards, increase ball speed*/
               // if(ballSpeed < maxSpeed)
               // {
               //     ballSpeed += (direction.y % 100) / 10;
               //     Debug.Log("ball speed = " + ballSpeed);
               // }
               // else
               // {
               //     //Only if the input is negative, add ball speed
               //     if(direction.y < minSpeed)
               //     {
               //         ballSpeed += (direction.y % 100) / 10;
               //     }
               // }
               // speedReference.value = ballSpeed;

                MoveBall(direction.x, direction.magnitude);

            }
        }
    }
    private void MoveBall(float pos, float amount)
    {
        if (pos != 0)
        {
            amount /= amountDivisor;
            print("x pos change = " + pos + " amount = " + amount);
            rb.AddForce((pos > 0 ? Vector3.right * amount : Vector3.left * amount));
        }
        else
        {
            print("swipe didn't register movement");
        }

    }

    public void SetLevelSpeed(float speed)
    {
        levelSpeed = speed;
        print("level speeed set to " + speed);
    }
}
