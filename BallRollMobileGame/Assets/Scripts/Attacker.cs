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

    public void Attack(Health target)
    {
        target.TakeDamage(this, damage);
    }

    public string GetName()
    {
        return attackerName;
    }

    public ATTACKER_TYPE GetAttackerType()
    {
        return attackerType;
    }
}