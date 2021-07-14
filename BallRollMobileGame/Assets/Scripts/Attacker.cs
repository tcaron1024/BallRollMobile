/*****************************************************************************
// File Name :         Attacker.cs
// Author :            Kyle Grenier
// Creation Date :     07/09/2021
//
// Brief Description : A class that game objects can be composed of to attack IDamageables.
*****************************************************************************/
using UnityEngine;


/// <summary>
/// A class that game objects can be composed of to attack IDamageables.
/// </summary>
public class Attacker : MonoBehaviour
{
    [Tooltip("The attacker's name.")]
    [SerializeField] private string attackerName;

    [Tooltip("The damage to inflict on the target.")]
    [SerializeField] private int damage;

    public enum ATTACKER_TYPE {UNASSIGNED, SPIKES, SNOWBALL, QUICKSAND };
    [SerializeField] private ATTACKER_TYPE attackerType;

    /// <summary>
    /// Attacks the target for a pre-determined amount of damage.
    /// </summary>
    /// <param name="target">The target to attack.</param>
    public void Attack(Health target)
    {
        target.TakeDamage(this, damage);
        EventManager.ObstacleCollision(attackerName);
    }

    /// <summary>
    /// Returns the attacker's name.
    /// </summary>
    /// <returns>The attacker's name.</returns>
    public string GetName()
    {
        return attackerName;
    }

    /// <summary>
    /// Returns the attacker's type.
    /// </summary>
    /// <returns>The attacker's type.</returns>
    public ATTACKER_TYPE GetAttackerType()
    {
        return attackerType;
    }
}