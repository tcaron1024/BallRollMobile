using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItself : MonoBehaviour
{
    // This is used for animation events
    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
