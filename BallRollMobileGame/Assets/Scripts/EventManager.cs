/*****************************************************************************
// File Name :         EventManager.cs
// Author :            Kyle Grenier
// Creation Date :     06/29/2021
//
// Brief Description : Event manager class used to define and invoke global events.
*****************************************************************************/
using System;
using UnityEngine;

public static class EventManager
{
    // Invoked when the player dies.
    public static Action OnPlayerDeath;

    // Invoked when player picks up a coin
    public static Action OnCoinPickup;

    // Invoked when player goes through a portal (completes a level)
    public static Action OnLevelComplete;

    // Invoked when player collides with an obstacle
    public static Action<string> OnObstacleCollision;

    /// <summary>
    /// Invoked when the player dies.
    /// </summary>
    public static void PlayerDeath()
    {
        Debug.Log("Player died!");
        OnPlayerDeath?.Invoke();
    }

    /// <summary>
    /// Invoked when player picks up a coin
    /// </summary>
    public static void CoinPickup()
    {
        OnCoinPickup?.Invoke();
    }

    /// <summary>
    /// Invoked when player goes through a portal (completes a level)
    /// </summary>
    public static void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    /// <summary>
    /// Invoked when player collides with an obstacle
    /// </summary>
    public static void ObstacleCollision(string obstacleName)
    {
        OnObstacleCollision.Invoke(obstacleName);
    }
}