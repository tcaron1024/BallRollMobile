﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;

    Touch t1;
    private Vector2 startPos;

    public float amountDivisor = 500f;

    [SerializeField] private float minMaxSpeedDiff = 4f;
    private float maxSpeed;
    private float minSpeed;
    private float yTarget = -.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Input.multiTouchEnabled = false;
        Input.simulateMouseWithTouches = true;
        //Invoke("GetVelocityNormalized", 2f);
    }

    void Update()
    {    
        maxSpeed = GameController.gameSpeed + minMaxSpeedDiff; 
        if (GameController.gameSpeed - minMaxSpeedDiff > 0)
        {
            minSpeed = GameController.gameSpeed - minMaxSpeedDiff;
        }
        else
        {
            minSpeed = 1;
        }

        if (Input.touchCount != 0)
        {
            t1 = Input.touches[0];
            if (t1.phase == TouchPhase.Began)
            {
                startPos = t1.position;
            }
            else if (t1.phase == TouchPhase.Ended || t1.phase == TouchPhase.Canceled)
            {
                MoveBall(t1.deltaPosition.x, (t1.position - startPos).magnitude);
            }
        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            print("ball high");
            rb.velocity = rb.velocity.normalized * GameController.gameSpeed;
        }
        else if (rb.velocity.magnitude < minSpeed)
        {
            print("ball low");
            rb.velocity = rb.velocity.normalized * GameController.gameSpeed;
        }
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        //print("max = " + maxSpeed);

        //if (rb.velocity.normalized.y > yTarget)
        //{
        //    float speed = rb.velocity.magnitude;
        //    //print("here - " + rb.velocity.normalized + " > " + yTarget);
        //    rb.velocity = new Vector3(rb.velocity.normalized.x * speed, yTarget * speed, rb.velocity.normalized.z * speed);
        //}


    }
    //void GetVelocityNormalized()
    //{
    //    yTarget = rb.velocity.normalized.y;
    //    ySet = true;
    //    print("yset is true - " + rb.velocity.normalized);
    //}
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
}