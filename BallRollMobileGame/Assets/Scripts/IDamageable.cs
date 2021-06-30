/*****************************************************************************
// File Name :         IDamageable.cs
// Author :            Kyle Grenier
// Creation Date :     06/29/2021
//
// Brief Description : Interface that any game object can utilize that takes damage.
*****************************************************************************/
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(GameObject attacker);
}
