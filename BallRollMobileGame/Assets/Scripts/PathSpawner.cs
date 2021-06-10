using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    [Tooltip("Array of the tiles that can be used for this level")]
    public GameObject[] pathPrefabs;
    
    [Tooltip("GameObject that holds the paths")]
    [SerializeField] private Transform pathParent = null;

    [Tooltip("Array of the pre-created starting paths")]
    [SerializeField] private GameObject[] startPaths;

    /// <summary>
    /// Last created path
    /// </summary>
    [SerializeField] private GameObject lastPath;

    private Vector3 offset;

    /// <summary>
    /// Spawns given number of paths in random order connected to starting path
    /// </summary>
    /// <param name="numPaths"></param>
    public void SpawnPaths(int numPaths)
    {
        // Finds distance offset for paths and sets the last path to last object in startPaths array
        offset = startPaths[1].transform.position - startPaths[0].transform.position;
        lastPath = startPaths[startPaths.Length - 1];

        // Spawns (number given - number of starting paths - 1) paths
        for (int i = startPaths.Length; i < numPaths - 1; i++)
        {           
            // Change first number in random.range to 0 to include the basic rectangle path
            int rand = Random.Range(1, pathPrefabs.Length);
            lastPath = Instantiate(pathPrefabs[rand], lastPath.transform.position + offset, lastPath.transform.rotation, pathParent);
        }

        // Spawn portal (ending) path here
        lastPath = Instantiate(pathPrefabs[0], lastPath.transform.position + offset, lastPath.transform.rotation, pathParent);
    }
}
