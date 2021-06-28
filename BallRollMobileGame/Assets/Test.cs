/*****************************************************************************
// File Name :         Test.cs
// Author :            Kyle Grenier
// Creation Date :     #CREATIONDATE#
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float forceMult;

    Vector3 force;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float x = Mathf.Sin(Time.time);

        force = new Vector3(x, 0, x);
        print(force);
    }

    private void FixedUpdate()
    {
        rb.AddForce(force * forceMult, ForceMode.Force);
    }
}
