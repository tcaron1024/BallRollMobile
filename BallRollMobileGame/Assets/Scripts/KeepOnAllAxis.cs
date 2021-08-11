﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnAllAxis : MonoBehaviour
{
    public GameObject player;
    public float zAdjustment;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + zAdjustment);
        
    }
}
