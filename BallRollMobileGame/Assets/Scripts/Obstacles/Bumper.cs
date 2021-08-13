/*****************************************************************************
// File Name :         Bumper.cs
// Author :            Kyle Grenier
// Creation Date :     07/25/2021
//
// Brief Description : The Bumper obstacle knocks the player back.
*****************************************************************************/
using UnityEngine;

public class Bumper : IColliderObstacle
{
    // Knocks the player back, similar to the pyramid.
    protected override void PerformAction(GameObject player, Collision col)
    {
        Rigidbody playerRb = player.GetComponent<Rigidbody>();

        //Vector3 force = col.GetContact(0).normal * 3f;

        Vector3 force = (player.transform.position - transform.position).normalized * playerRb.velocity.z;
        force.z *= 3f;
        force.y = Random.Range(1, 5);
        print(force);

        playerRb.AddForce(force, ForceMode.VelocityChange);

        EventManager.ObstacleCollision(this.name);
    }
}