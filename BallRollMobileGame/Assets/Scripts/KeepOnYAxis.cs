using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnYAxis : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject plane;
    public GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(0, 0, mainCamera.transform.position.z);
    }
}
