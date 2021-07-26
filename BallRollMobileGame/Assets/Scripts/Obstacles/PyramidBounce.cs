/*****************************************************************************
// File Name :         PyramidBounce.cs
// Author :            Kyle Grenier
// Creation Date :     07/19/2021
//
// Brief Description : Adds force to the player in the opposite direction they're travelling in 
                       on impact with the game object.
*****************************************************************************/
using UnityEngine;

// OLD: Now implemented in Pyramid.cs

public class PyramidBounce : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = col.gameObject.GetComponent<Rigidbody>();

            Vector3 force = col.GetContact(0).normal * playerRb.velocity.z;
            
            playerRb.AddForce(force, ForceMode.Impulse);

            //print(Vector3.Reflect(col.gameObject.GetComponent<Rigidbody>().velocity,col.GetContact(0).normal));
        }
    }
}
