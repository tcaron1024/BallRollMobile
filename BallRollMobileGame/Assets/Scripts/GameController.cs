using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static float gameSpeed = 2f;
    public static Vector3 pathSlope = new Vector3();

    public float GamerSpeeding = 2f;

    [Tooltip("Ball speed increase per second")]
    public float speedIncreaseRate = .1f;
    //public float speedIncreaseAmount = .1f;

    //private float timer = 0;
    // Update is called once per frame
    void Update()
    {
        gameSpeed += Time.deltaTime * speedIncreaseRate;
        GamerSpeeding = gameSpeed;
    }
}
