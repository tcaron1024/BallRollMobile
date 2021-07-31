/*****************************************************************************
// File Name :         Fan.cs
// Author :            Kyle Grenier
// Creation Date :     07/31/2021
//
// Brief Description : The fan pushes the player in the direction the wind is blowing.
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class Fan : ITriggerObstacle
{
    [SerializeField] private float pushForce;
    private bool playerInRange;

    private Rigidbody playerRb;

    protected override void Awake()
    {
        base.Awake();

        playerInRange = false;
    }

    public override void PerformAction(GameObject player)
    {
        playerRb = player.GetComponent<Rigidbody>();
        playerInRange = true;
    }

    /// <summary>
    /// Stops pushing the player.
    /// </summary>
    /// <param name="player">The player GameObject.</param>
    public override void StopAction(GameObject player)
    {
        playerRb = null;
        playerInRange = false;
    }

    private void FixedUpdate()
    {
        if (playerInRange)
            playerRb.AddForce(-transform.forward * pushForce);
    }
}
