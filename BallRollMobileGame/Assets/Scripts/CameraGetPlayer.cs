/*****************************************************************************
// File Name :         CameraGetPlayer.cs
// Author :            Kyle Grenier
// Creation Date :     07/25/2021
//
// Brief Description : Assigns the target to follow to the Player Transform.
*****************************************************************************/
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraGetPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera cam;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        cam.Follow = player;
    }
}
