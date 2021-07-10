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


    // Event for taking damage.
    public delegate void OnDamageHandler(int remainingLives);
    public event OnDamageHandler OnDamageTaken;

    // A separate event for getting the damage and attacker.
    // Both the previous damage event and this one are invoked simultaneously.
    public delegate void OnDamageHandlerAttacker(int remainingLives, Attacker attacker);
    public event OnDamageHandlerAttacker OnDamageTakenAttacker;

    // Event for death.
    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;


    private void Awake()
    {
        currentLives = maxLives;
        _invincible = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            TakeDamage(null, 1);
    }

    // If the OnDisable() method in the GameController is causing errors with a missing Health
    // component on the Player game object, uncomment the below lines and comment out the GameController OnDisable() method.

    private void OnDestroy()
    {
        // Setting our events to null so we don't run into trouble
        // with OnDisable() conflicts in the GameManager (in regards to the player's health).
        OnDamageTaken = null;
        OnDeath = null;
    }




    public int GetCurrentLives()
    {
        return currentLives;
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
            OnDamageTakenAttacker?.Invoke(currentLives, attacker);
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