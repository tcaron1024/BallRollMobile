/*****************************************************************************
// File Name :         IObstacle.cs
// Author :            Kyle Grenier
// Creation Date :     06/27/2021
//
// Brief Description : An interface all concrete Obstacles must derive from.
*****************************************************************************/
using UnityEngine;

public interface IObstacle
{
    /// <summary>
    /// Performs the concrete Obstacle's action.
    /// </summary>
    /// <param name="player">The player game object.</param>
    void PerformAction(GameObject player);

    /// <summary>
    /// Stops the concrete Obstacle's action.
    /// </summary>
    /// <param name="player">The player game object.</param>
    void StopAction(GameObject player);
}