using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;

    Touch t1;
    private Vector2 startPos;

    public float amountDivisor = 500f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Input.multiTouchEnabled = false;
        Input.simulateMouseWithTouches = true;
    }

    void Update()
    {
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
    }

    private void MoveBall(float pos, float amount)
    {
        amount /= amountDivisor;
        print("x pos change = " + pos + " amount = " + amount);
        rb.AddForce((pos > 0 ? Vector3.right * amount : Vector3.left * amount));
    }
}
