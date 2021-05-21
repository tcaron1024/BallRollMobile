using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    public GameObject[] paths;
    private GameObject lastPath;
    [SerializeField] private Transform pathParent;

    private Vector3 offset;

    public GameObject[] startPaths;

    public float spawnTime = 1;
    private float timer;

    private void Start()
    {
        timer = spawnTime;
        offset = startPaths[1].transform.position - startPaths[0].transform.position;
        lastPath = startPaths[startPaths.Length - 1];
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime * GameController.gameSpeed;
        if (timer <= 0)
        {
            SpawnPath();
            timer = spawnTime;
        }
    }

    private void SpawnPath()
    {
        int rand = Random.Range(0, paths.Length);
        lastPath = Instantiate(paths[rand], lastPath.transform.position + offset, lastPath.transform.rotation, pathParent);
    }
}
