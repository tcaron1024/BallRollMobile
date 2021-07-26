/*****************************************************************************
// File Name :         IColliderObstacle.cs
// Author :            Kyle Grenier
// Creation Date :     06/27/2021
//
// Brief Description : An Obstacle that makes use of colliders, not triggers.
                       (i.e. OnCollisionEnter(), OnCollisionExit()).
*****************************************************************************/
using UnityEngine;

public abstract class IColliderObstacle : MonoBehaviour, IObstacle
{
    private Collision collision = null;

    protected virtual void Awake()
    {
        Collider col = GetComponent<Collider>();
        if (col.isTrigger)
            col.isTrigger = false;
    }

    /// <summary>
    /// Called when the Player collides with this Obstacle.
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            collision = col;
            PerformAction(col.gameObject);
        }
    }

    /// <summary>
    /// Called when the Player leaves the Obstacle's collider.
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            collision = col;
            StopAction(col.gameObject);
        }
    }

    // Below is the method we require to implement from our IObstacle interface.
    public void PerformAction(GameObject player) => PerformAction(player, collision);

    // Here is the abstract method that each concrete obstacle will need to implement since we need to make use of
    // the Collision object.
    protected abstract void PerformAction(GameObject player, Collision col);

    public void StopAction(GameObject player) => StopAction(player, collision);
    protected virtual void StopAction(GameObject player, Collision col) { }
}
