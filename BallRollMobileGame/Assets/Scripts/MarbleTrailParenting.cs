using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleTrailParenting : MonoBehaviour
{
    public GameObject marble;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = marble.transform.position;
    }
}
