using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    Rigidbody rb;

    Touch t1;
    private Vector2 startPos;

    public float startForce;
    public float ballSpeed = 7f;
    public float amountDivisor = 500f;

    [SerializeField] private float minMaxSpeedDiff = 4f;
    private float maxSpeed = 50.0f;
    private float minSpeed = 0.0f;
    private float yTarget = -.2f;

    private Vector2[] touchStartPositions = new Vector2[10];

    //Shows the player how fast they are going
    public Slider speedReference;

    public 


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Input.multiTouchEnabled = false;
        Input.simulateMouseWithTouches = true;
        print(Vector3.forward * startForce);
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
        rb.AddForce(GameController.pathSlope * GameController.gameSpeed * ballSpeed * Time.deltaTime);   
        //print("adding force - " + GameController.pathSlope * GameController.gameSpeed * ballSpeed * Time.deltaTime);
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

                /*If ball is going under max speed, and the player has swiped
               upwards, increase ball speed*/
                if(ballSpeed < maxSpeed)
                {
                    ballSpeed += (direction.y % 100) / 10;
                    Debug.Log("ball speed = " + ballSpeed);
                }
                else
                {
                    //Only if the input is negative, add ball speed
                    if(direction.y < minSpeed)
                    {
                        ballSpeed += (direction.y % 100) / 10;
                    }
                }
                speedReference.value = ballSpeed;
                MoveBall(direction.x, direction.magnitude);

            }
        }


        //if (Input.touchCount != 0)
        //{
        //    t1 = Input.touches[0];
        //    if (t1.phase == TouchPhase.Began)
        //    {
        //        startPos = t1.position;
        //    }
        //    else if (t1.phase == TouchPhase.Ended || t1.phase == TouchPhase.Canceled)
        //    {
        //        MoveBall(t1.deltaPosition.x, (t1.position - startPos).magnitude);
        //    }
        //}
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

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Path"))
    //    {
    //        collidingWithPath = false;
    //        if (!collidingWithPath)
    //        {
    //            print("go down");
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Path"))
    //    {
    //        collidingWithPath = true;

    //    }
    //}

    //void GetVelocityNormalized()
    //{
    //    yTarget = rb.velocity.normalized.y;
    //    ySet = true;
    //    print("yset is true - " + rb.velocity.normalized);
    //}
    //private void Update()
    //{
            //if (rb.velocity.magnitude > maxSpeed)
             //{
             //    print("ball high");
             //    rb.velocity = rb.velocity.normalized * GameController.gameSpeed;
             //}
             //else if (rb.velocity.magnitude < minSpeed)
             //{
             //    print("ball low");
             //    rb.velocity = rb.velocity.normalized * GameController.gameSpeed;
             //}
             //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
             //print("max = " + maxSpeed);

        //if (rb.velocity.normalized.y > yTarget)
        //{
        //    float speed = rb.velocity.magnitude;
        //    //print("here - " + rb.velocity.normalized + " > " + yTarget);
        //    rb.velocity = new Vector3(rb.velocity.normalized.x * speed, yTarget * speed, rb.velocity.normalized.z * speed);
        //}
        //maxSpeed = GameController.gameSpeed + minMaxSpeedDiff; 
        //if (GameController.gameSpeed - minMaxSpeedDiff > 0)
        //{
        //    minSpeed = GameController.gameSpeed - minMaxSpeedDiff;
        //}
        //else
        //{
        //    minSpeed = 1;
        //}
    //}
}
