/*****************************************************************************
// File Name :         Spikes.cs
// Author :            Kyle Grenier
// Creation Date :     06/27/2021
//
// Brief Description : The Spikes obstacle 'pops' the player on impact, dealing damage.
*****************************************************************************/
using UnityEngine;

public class Spikes : IColliderObstacle
{
    // The component that allows this obstacle to inflict damage.
    private Attacker attackingComponent;

    private void Awake()
    {
        attackingComponent = GetComponent<Attacker>();
    }

    public override void PerformAction(GameObject player)
    {
        // Attack the player.
        Health playerHealth = player.GetComponent<Health>();
        attackingComponent.Attack(playerHealth);

        // TODO: Particle effects?? Sounds???
    }
}