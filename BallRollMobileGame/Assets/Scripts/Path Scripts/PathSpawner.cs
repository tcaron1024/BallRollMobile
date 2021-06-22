using UnityEngine;

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

    [Tooltip("Array of the tiles that can be used for this level")]
    public GameObject[] currentPathPrefabs;

    [Tooltip("GameObject that holds the paths")]
    [SerializeField] private Transform pathParent = null;

    [Tooltip("Array of the pre-created starting paths")]
    [SerializeField] private GameObject[] startPaths;

    [Tooltip("Last created path")]
    [SerializeField] private Path lastPath;

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
        lastPath = new Path(startPaths.Length - 1, startPaths[startPaths.Length - 1]);

        // Spawns (number given - number of starting paths - 1) paths
        for (int i = startPaths.Length; i < numPaths - 1; i++)
        {
            // Keep searching for a suitable random number if the previous path spawned == the current path.
            int rand = -1;
            
            do
            {
                #region -- Debugging Path Spawning --
                //if (rand != -1)
                //{
                //    Debug.Log("PATH SPAWNER: Path num " + i + " equals the previous. Getting a new random num.");
                //    Debug.Log("PATH SPAWNER: " + rand + " == " + lastPath.index);
                //    Debug.Log("PATH SPAWNER: Previous path is type " + lastPath.gameObject.name);
                //}
                #endregion
                rand = Random.Range(0, currentPathPrefabs.Length);
                
            }
            while (lastPath.index == rand);

            // For now spawns a random number of coins from 0-3, need to see what people want for this
            int coinRand = Random.Range(0, 4);

            lastPath = new Path(rand, Instantiate(currentPathPrefabs[rand], lastPath.gameObject.transform.position + offset, lastPath.gameObject.transform.rotation, pathParent));
            lastPath.gameObject.GetComponent<PathBehavior>().SpawnCoins(coinRand);
            //Debug.Log("PATH SPAWNER: Selected path type " + lastPath.gameObject.name + " for path num " + i);
        }

        // Spawn portal (ending) path here
        lastPath = new Path(-1, Instantiate(portalPath, lastPath.gameObject.transform.position + offset, lastPath.gameObject.transform.rotation, pathParent));
    }
}
