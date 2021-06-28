/*****************************************************************************
// File Name :         QuickSand.cs
// Author :            Kyle Grenier
// Creation Date :     06/27/2021
//
// Brief Description : The Quick Sand obstacle sucks the player in, with more
                       force being applied the closer the player is to the center.
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class QuickSand : ITriggerObstacle
{
    [Tooltip("The base amount of force to suck the player in with.")]
    [SerializeField] private float suckForce = 3f;

    // The player's Rigidbody component.
    private Rigidbody playerRb;

    // The player's initial scale on entering the trigger.
    private Vector3 initialScale;


    /// <summary>
    /// Begins sucking the player into the quicksand.
    /// </summary>
    /// <param name="player"></param>
    public override void PerformAction(GameObject player)
    {
        StopAllCoroutines();
        StartCoroutine(ActionCoroutine(player));
    }
    
    /// <summary>
    /// Stops sucking the player into the quicksand.
    /// </summary>
    /// <param name="player">The player gameobject.</param>
    public override void StopAction(GameObject player)
    {
        StopAllCoroutines();
        player.transform.localScale = initialScale;
    }

    private IEnumerator ActionCoroutine(GameObject player)
    {
        if (playerRb == null)
            playerRb = player.GetComponent<Rigidbody>();

        // Will be used to modify the player's scale.
        float initialDistance = Vector3.Distance(player.transform.position, transform.position);

        // Setting the player's initial scale so we can set it back once they leave
        // the trigger.
        initialScale = player.transform.localScale;

        // Run until StopAction() is called.
        while (true)
        {
            float dist = Vector3.Distance(transform.position, playerRb.position);

            PerformSuck(dist);
            ShrinkPlayer(player, dist, initialDistance);

            yield return null;
        }
    }

    /// <summary>
    /// Perform the action of sucking the player marble into the center.
    /// A stronger force will be used the closer the player is to the center.
    /// </summary>
    /// <param name="dist">The distance the player is from the quicksand.</param>
    private void PerformSuck(float dist)
    {
        float forceAmnt = 0f;

        if (dist > 0.0001f)
            forceAmnt = suckForce / dist;

        Vector3 force = (transform.position - playerRb.position).normalized * forceAmnt;
        playerRb.AddForce(force, ForceMode.Force);
    }

    /// <summary>
    /// Shrinks the player smaller and smaller the closer they are to the center
    /// of the quicksand.
    /// </summary>
    /// <param name="player">The player gameobject.</param>
    /// <param name="dist">The distance the player is from the quicksand.</param>
    private void ShrinkPlayer(GameObject player, float dist, float initialDistance)
    {
        float normalizedDistance = dist / initialDistance;
        normalizedDistance = Mathf.Clamp(normalizedDistance, 0.1f, 1f);
        print(normalizedDistance);

        Vector3 scale = Vector3.one * normalizedDistance;
        player.transform.localScale = scale;
    }
}