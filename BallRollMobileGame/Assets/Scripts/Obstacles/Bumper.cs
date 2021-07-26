/*****************************************************************************
// File Name :         Bumper.cs
// Author :            Kyle Grenier
// Creation Date :     07/25/2021
//
// Brief Description : The Bumper obstacle knocks the player back.
*****************************************************************************/
using UnityEngine;

public class Bumper : IColliderObstacle
{
    protected override void PerformAction(GameObject player, Collision col)
    {
        throw new System.NotImplementedException();
    }
}