/*****************************************************************************
// File Name :         IPowerUp.cs
// Author :            Kyle Grenier
// Creation Date :     07/31/2021
//
// Brief Description : Abstract powerup class that defines base functionality for all powerups the
                       player can possess.
*****************************************************************************/
using UnityEngine;
using System.Collections;

public abstract class IPowerUp : MonoBehaviour
{
    protected virtual void Start()
    {
        Activate();
        StartCoroutine(StartDeactivation());
    }

    protected abstract void Activate();
    protected abstract float GetDeactivationTime();
    protected abstract void Deactivate();

    /// <summary>
    /// Removes the component after a time.
    /// </summary>
    protected IEnumerator StartDeactivation()
    {
        yield return new WaitForSeconds(GetDeactivationTime());
        Deactivate();
    }
}
