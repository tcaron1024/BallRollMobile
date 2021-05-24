using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{
    public Vector3 offset;
    public Transform ball;

    private float camSpeed = 2;
    private Vector3 target;
    
    // Update is called once per frame
    void Update()
    {
        camSpeed = GameController.gameSpeed;
        target = ball.position + offset;
        float interpolation = camSpeed * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, target, interpolation);

    }


}
