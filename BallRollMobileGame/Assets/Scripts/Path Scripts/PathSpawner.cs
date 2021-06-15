using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    [Tooltip("Array of the tiles that can be used for the default scenery")]
    public GameObject[] defaultPathPrefabs;

    [Tooltip("Array of the tiles that can be used for the desert scenery")]
    public GameObject[] desertPathPrefabs;

    [Tooltip("Array of the tiles that can be used for this level")]
    public GameObject[] currentPathPrefabs;

    [Tooltip("GameObject that holds the paths")]
    [SerializeField] private Transform pathParent = null;

    [Tooltip("Array of the pre-created starting paths")]
    [SerializeField] private GameObject[] startPaths;

    [Tooltip("Last created path")]
    [SerializeField] private GameObject lastPath;

    [Tooltip("Portal Path prefab")]
    [SerializeField] private GameObject portalPath;

    private Vector3 offset;

    /// <summary>
    /// Spawns given number of paths in random order connected to starting path
    /// </summary>
    /// <param name="numPaths"></param>
    public void SpawnPaths(int numPaths)
    {
        // Checks which paths to use for this level - 1 = default, 2 = desert
        switch (GameController.scenerySettings)
        {
            case 1:
                currentPathPrefabs = defaultPathPrefabs;
                break;               
            case 2:
                currentPathPrefabs = desertPathPrefabs;
                break;
            default:
                currentPathPrefabs = defaultPathPrefabs;
                break;
        }

        // Finds distance offset for paths and sets the last path to last object in startPaths array
        offset = startPaths[1].transform.position - startPaths[0].transform.position;
        lastPath = startPaths[startPaths.Length - 1];

        // Spawns (number given - number of starting paths - 1) paths
        for (int i = startPaths.Length; i < numPaths - 1; i++)
        {           
            // Change first number in random.range to 0 to include the basic rectangle path
            int rand = Random.Range(0, currentPathPrefabs.Length);
            lastPath = Instantiate(currentPathPrefabs[rand], lastPath.transform.position + offset, lastPath.transform.rotation, pathParent);
        }

        // Spawn portal (ending) path here
        lastPath = Instantiate(portalPath, lastPath.transform.position + offset, lastPath.transform.rotation, pathParent);
    }
}
