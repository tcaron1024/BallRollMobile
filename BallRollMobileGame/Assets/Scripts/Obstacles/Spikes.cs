/*****************************************************************************
// File Name :         Spikes.cs
// Author :            Kyle Grenier
// Creation Date :     06/27/2021
//
// Brief Description : The Spikes obstacle damage the player on impact.
*****************************************************************************/
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class Spikes : IColliderObstacle
{
    // The component that allows this obstacle to inflict damage.
    private Attacker attackingComponent;
    public GameObject explosionEffect;
    public GameObject explosionParticles;

    [Tooltip("True if the game object should be destroyed after hitting the player.")]
    [SerializeField] private bool destroyOnContact;

    protected override void Awake()
    {
        base.Awake();
        attackingComponent = GetComponent<Attacker>();
    }

    public override void PerformAction(GameObject player)
    {
        // Attack the player.
        Health playerHealth = player.GetComponent<Health>();
        attackingComponent.Attack(playerHealth);

        // TODO: Particle effects?? Sounds???
        Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity);
        Instantiate(explosionParticles, gameObject.transform.position, Quaternion.identity);
        if (destroyOnContact)
            Destroy(gameObject);
    }
}