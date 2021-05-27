using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    public GameObject[] paths;
    [SerializeField] private GameObject lastPath;
    [SerializeField] private Transform pathParent = null;

    private Vector3 offset;

    private GameObject[] startPaths;

    //public float spawnTime = 1;
    //private float timer;

    private void Start()
    {
        //timer = spawnTime;
        startPaths = GameObject.FindGameObjectsWithTag("Path");
        offset = startPaths[1].transform.position - startPaths[0].transform.position;
        GameController.pathSlope = offset.normalized;
        print("pathslope = " + GameController.pathSlope);
        lastPath = startPaths[startPaths.Length - 1];
    }

    // Update is called once per frame
    //void Update()
    //{
    //    timer -= Time.deltaTime * GameController.gameSpeed;
    //    if (timer <= 0)
    //    {
    //        SpawnPath();
    //        timer = spawnTime;
    //    }
    //}

    public void SpawnPath()
    {
        int rand = Random.Range(0, paths.Length);
        lastPath = Instantiate(paths[rand], lastPath.transform.position + offset, lastPath.transform.rotation, pathParent);
    }
}
