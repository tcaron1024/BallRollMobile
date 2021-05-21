using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static float gameSpeed = 1f;

    public float speedIncreaseRate = 5f;
    public float speedIncreaseAmount = .1f;

    private float timer = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= speedIncreaseRate)
        {
            gameSpeed += speedIncreaseAmount;
            timer = 0;
        }
    }
}
