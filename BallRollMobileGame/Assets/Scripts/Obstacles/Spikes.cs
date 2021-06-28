/*****************************************************************************
// File Name :         Spikes.cs
// Author :            Kyle Grenier
// Creation Date :     06/27/2021
//
// Brief Description : The Spikes obstacle 'pops' the player on impact, ending the game.
*****************************************************************************/
using UnityEngine;

public class Spikes : IColliderObstacle
{
    /// <summary>
    /// 'Pops' the player on impact, ending the game.
    /// </summary>
    /// <param name="player">The player gameobject.</param>
    public override void PerformAction(GameObject player)
    {
        print("POP");
        // TODO: Play 'pop' anim and end the game.
    }
}