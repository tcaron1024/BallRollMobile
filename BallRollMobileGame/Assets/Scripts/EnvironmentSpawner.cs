using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [Tooltip("% Chance of spawning an environment object at each spawn point")]
    [SerializeField] private int spawnChance = 50;

    [Tooltip("List of Environment Objects used in the desert level")]
    [SerializeField] private List<GameObject> defaultEnvironmentObjs = new List<GameObject>();

    [Tooltip("List of possible spawn positions in the default environment - positions are offsets from path objects")]
    [SerializeField] private List<Vector3> defaultSpawnPos = new List<Vector3>();

    [Tooltip("List of Environment Objects used in the desert level")]
    [SerializeField] private List<GameObject> desertEnvironmentObjs = new List<GameObject>();

    [Tooltip("List of possible spawn positions in the desert environment - positions are offsets from path objects")]
    [SerializeField] private List<Vector3> desertSpawnPos = new List<Vector3>();

    [Tooltip("List of Environment Objects used in the desert level")]
    [SerializeField] private List<GameObject> arcticEnvironmentObjs = new List<GameObject>();

    [Tooltip("List of possible spawn positions in the arctic environment - positions are offsets from path objects")]
    [SerializeField] private List<Vector3> arcticSpawnPos = new List<Vector3>();

    /// <summary>
    /// Jagged list used to hold environment objects for all levels
    /// </summary>
    private List<List<GameObject>> environmentObjs = new List<List<GameObject>>();

    /// <summary>
    /// Jagged list used to hold spawn positions for all levels
    /// </summary>
    private List<List<Vector3>> environmentSpawnPos = new List<List<Vector3>>();

    /// <summary>
    /// Parent to spawn the environment objects under - taken from the path given in SpawnEnvironment function
    /// </summary>
    private Transform parent;

    
    private void Awake()
    {
        // Initializing Object list
        environmentObjs.Add(defaultEnvironmentObjs);
        environmentObjs.Add(desertEnvironmentObjs);
        environmentObjs.Add(arcticEnvironmentObjs);

        // Initializing Spawn Pos list
        environmentSpawnPos.Add(defaultSpawnPos);
        environmentSpawnPos.Add(desertSpawnPos);
        environmentSpawnPos.Add(arcticSpawnPos);
        Debug.Log("start");
    }

    public void SpawnEnvironment(GameObject path, int index)
    {
        // only implemented for default level for now
        if (index < 1)
        {
            // Get parent from path
            parent = path.GetComponent<PathBehavior>().environmentParent;

            // For every possible spawn position, randomly choose whether or not to spawn and object and which to spawn
            foreach (Vector3 pos in environmentSpawnPos[index])
            {
                int rand = Random.Range(0, 99);

                // If an object should be spawned
                if (rand < spawnChance)
                {
                    int objRand = Random.Range(0, environmentObjs[index].Count - 1);
                    Instantiate(environmentObjs[index][objRand], parent.position + pos, Quaternion.identity, parent);
                }
            }
        }

    }
}
