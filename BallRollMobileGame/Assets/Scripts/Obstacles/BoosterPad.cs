/*****************************************************************************
// File Name :         BoosterPad.cs
// Author :            Kyle Grenier
// Creation Date :     07/31/2021
//
// Brief Description : The booster pad increases the player's speed for a short duration.
*****************************************************************************/
using UnityEngine;

public class BoosterPad : ITriggerObstacle
{
    public override void PerformAction(GameObject player)
    {
        player.AddComponent<SpeedPowerup>();
        EventManager.ObstacleCollision(this.GetType().Name);
    }
}
