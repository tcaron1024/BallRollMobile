/*****************************************************************************
// File Name :         BallHealthManager.cs
// Author :            Kyle Grenier
// Creation Date :     06/29/2021
//
// Brief Description : Class to manage the ball's (player's) health.
*****************************************************************************/
using UnityEngine;

public class BallHealthManager : MonoBehaviour, IDamageable
{
    /// <summary>
    /// Destroys the marble instantly, ending the game.
    /// </summary>
    public void TakeDamage(GameObject attacker)
    {
        if (attacker.CompareTag("Spikes"))
            print("POP"); // TODO: Popping anim and SFX here

        EventManager.PlayerDeath();
        Destroy(gameObject);
    }
}