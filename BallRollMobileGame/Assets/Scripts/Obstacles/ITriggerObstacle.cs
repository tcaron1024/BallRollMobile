/*****************************************************************************
// File Name :         ITriggerObstacle.cs
// Author :            Kyle Grenier
// Creation Date :     06/27/2021
//
// Brief Description : An Obstacle that makes use of triggers instead of colliders.
                       (i.e. OnTriggerEnter(), OnTriggerExit()).
*****************************************************************************/
using UnityEngine;

public abstract class ITriggerObstacle : MonoBehaviour, IObstacle
{
    /// <summary>
    /// Called when the Player collides with this Obstacle.
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PerformAction(col.gameObject);
        }
    }

    /// <summary>
    /// Called when the Player leaves the Obstacle's collider.
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StopAction(col.gameObject);
        }
    }

    public abstract void PerformAction(GameObject player);
    public abstract void StopAction(GameObject player);

}
