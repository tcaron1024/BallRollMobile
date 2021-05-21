using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{
    public Vector3 offset;
    public Transform ball;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = ball.position + offset;
    }
}
