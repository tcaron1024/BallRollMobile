/*****************************************************************************
// File Name :         Snowball.cs
// Author :            Kyle Grenier
// Creation Date :     07/18/2021
//
// Brief Description : The Snowball slides down the slope it is spawned on, and if it hits the player
                       it deals damage and is destroyed.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class Snowball : IColliderObstacle
{
    public GameObject explosionEffect;
    public GameObject particleExplosion;
    private Attacker attackingComponent;

    protected override void Awake()
    {
        base.Awake();
        attackingComponent = GetComponent<Attacker>();
    }

    protected override void PerformAction(GameObject player, Collision col)
    {
        Health playerHealth = player.GetComponent<Health>();
        attackingComponent.Attack(playerHealth);

        // TODO: Particle effects
        Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity);
        Instantiate(particleExplosion, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}