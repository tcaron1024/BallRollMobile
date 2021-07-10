/*****************************************************************************
// File Name :         Health.cs
// Author :            Kyle Grenier
// Creation Date :     06/29/2021
//
// Brief Description : Class to manage a game object's health if it takes damage.
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Tooltip("The maximum (starting) lives this object has.")]
    [SerializeField] private int maxLives;
    private int currentLives;

    [Tooltip("The time in seconds the object should remain invincible after receiving damage.")]
    [SerializeField] private float invincibilityTime;

    // True if the object is invincible (i.e. it cannot take damage).
    private bool _invincible;
    public bool invincible { get { return _invincible; } }


    // Events for taking damage and death.
    public delegate void OnDamageHandler(int remainingLives);
    public event OnDamageHandler OnDamageTaken;

    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;




    private void Start()
    {
        currentLives = maxLives;
        _invincible = false;
    }

    public void TakeDamage(Attacker attacker, int health)
    {
        if (_invincible)
            return;

        currentLives -= health;

        // If we're not out of lives yet, start invincibility counter
        // and invoke the OnDamageTaken event.
        if (currentLives > 0)
        {
            StartCoroutine(DamageCooldown());
            OnDamageTaken?.Invoke(currentLives);
        }
        // If we're out lives, invoke the event and destroy the object.
        else
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Activates the object's invinvibility for a set period of time.
    /// </summary>
    private IEnumerator DamageCooldown()
    {
        _invincible = true;

        float currentTime = 0f;
        while (currentTime < invincibilityTime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        _invincible = false;
    }
}