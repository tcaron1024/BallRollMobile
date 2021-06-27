using UnityEngine;
using System.Collections.Generic;

public class PathSpawner : MonoBehaviour
{
    private struct Path
    {
        public Path(int index, GameObject gameObject)
        {
            this.index = index;
            this.gameObject = gameObject;
        }

        public int index { get; private set; }
        public GameObject gameObject { get; private set; }
    }

    [Tooltip("Array of the tiles that can be used for the default scenery")]
    public GameObject[] defaultPathPrefabs;

    [Tooltip("Array of the tiles that can be used for the desert scenery")]
    public GameObject[] desertPathPrefabs;

    [Tooltip("Array of the tiles that can be used for the desert scenery")]
    public GameObject[] arcticPathPrefabs;

    [Tooltip("Array of the tiles that can be used for this level")]
    public GameObject[] currentPathPrefabs;

    [Tooltip("GameObject that holds the paths")]
    [SerializeField] private Transform pathParent = null;

    [Tooltip("Array of the pre-created starting paths")]
    [SerializeField] private GameObject[] startPaths;

    [Tooltip("Last created path")]
    [SerializeField] private Path lastPath;

    private List<GameObject> unusedPaths;
    private List<GameObject> usedPaths;

    [Tooltip("Portal Path prefab")]
    [SerializeField] private GameObject[] portalPaths;

    private Vector3 offset;

    /// <summary>
    /// Spawns given number of paths in random order connected to starting path
    /// </summary>
    /// <param name="numPaths"></param>
    public void SpawnPaths(int numPaths)
    {
        // Checks which paths to use for this level - 0 = default, 1 = desert
        switch (GameController.scenerySettings)
        {
            case 0:
                currentPathPrefabs = defaultPathPrefabs;
                break;               
            case 1:
                currentPathPrefabs = desertPathPrefabs;
                break;
            case 2:
                currentPathPrefabs = arcticPathPrefabs;
                break;
            default:
                currentPathPrefabs = defaultPathPrefabs;
                break;
        }

        // Finds distance offset for paths and sets the last path to last object in startPaths array
        offset = startPaths[1].transform.position - startPaths[0].transform.position;
        lastPath = new Path(startPaths.Length - 1, startPaths[startPaths.Length - 1]);

        // Initializing the unusedPaths and usedPaths lists.
        unusedPaths = new List<GameObject>(currentPathPrefabs);
        usedPaths = new List<GameObject>();

        // Spawns (number given - number of starting paths - 1) paths
        for (int i = startPaths.Length; i < numPaths - 1; i++)
        {
            if (unusedPaths.Count == 0)
            {
                unusedPaths = new List<GameObject>(usedPaths);
                usedPaths.Clear();
            }

            // Get a random index.
            int rand = Random.Range(0, unusedPaths.Count);

            lastPath = new Path(rand, Instantiate(unusedPaths[rand], lastPath.gameObject.transform.position + offset, lastPath.gameObject.transform.rotation, pathParent));

            // Move the used path to the unusedPaths list.
            GameObject temp = unusedPaths[rand];
            usedPaths.Add(temp);
            unusedPaths.RemoveAt(rand);

        }

        // Spawn portal (ending) path here
        lastPath = new Path(-1, Instantiate(portalPaths[GameController.scenerySettings], lastPath.gameObject.transform.position + offset, lastPath.gameObject.transform.rotation, pathParent));
    }
}
