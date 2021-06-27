using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBehavior : MonoBehaviour
{
    #region -- Coin Spawning Variables --
    [Header("Coins")]

    [Tooltip("Coin Prefab")]
    [SerializeField] private GameObject coin;

    [Tooltip("List of possible coin spawn locations for this path")]
    [SerializeField] private List<Transform> possibleCoinPositions;

    [Tooltip("Gameobject to spawn coins under")]
    [SerializeField] private Transform coinParent;

    #endregion

    #region -- Obstacle Spawning Variables --
    [Header("Obstacles")]

    [Tooltip("Gameobjects to spawn obstacles under")]
    [SerializeField] private List<Transform> obstacleParents;

    [Tooltip("Obstacle objects to spawn for run 1")]
    [SerializeField] private List<GameObject> run1ObstacleObjects;

    [Tooltip("Obstacle spawn locations for run 1")]
    [SerializeField] private List<Transform> run1ObstaclePositions;

    [Tooltip("Obstacle objects to spawn for run 2")]
    [SerializeField] private List<GameObject> run2ObstacleObjects;

    [Tooltip("Obstacle spawn locations for run 2")]
    [SerializeField] private List<Transform> run2ObstaclePositions;

    [Tooltip("Obstacle objects to spawn for run 3")]
    [SerializeField] private List<GameObject> run3ObstacleObjects;

    [Tooltip("Obstacle spawn locations for run 3")]
    [SerializeField] private List<Transform> run3ObstaclePositions;

    /// <summary>
    /// Jagged array used to hold the Transforms to spawn obstacle objects at depending on run number
    /// </summary>
    private List<List<Transform>> obstaclePositions = new List<List<Transform>>();

    /// <summary>
    /// Jagged array used to hold the GameObjects needed to be spawned depending on run number
    /// </summary>
    private List<List<GameObject>> obstacleObjects = new List<List<GameObject>>();

    #endregion

    private void Start()
    {
        // Create jagged arrays for obstacle spawning
        obstacleObjects.Add(run1ObstacleObjects);
        obstacleObjects.Add(run2ObstacleObjects);
        obstacleObjects.Add(run3ObstacleObjects);
        obstaclePositions.Add(run1ObstaclePositions);
        obstaclePositions.Add(run2ObstaclePositions);
        obstaclePositions.Add(run3ObstaclePositions);

        // Spawns all interactables for this path (coins and obstacles)
        SpawnCoins(Random.Range(0, possibleCoinPositions.Count + 1));
        SpawnObstacles(GameController.environmentRuns[GameController.scenerySettings]);
    }

    /// <summary>
    /// Spawns given number of coins on this path
    /// </summary>
    /// <param name="numToSpawn"></param>
    private void SpawnCoins(int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            int rand = Random.Range(0, possibleCoinPositions.Count);
            Instantiate(coin, possibleCoinPositions[rand].position, possibleCoinPositions[rand].rotation, coinParent);
            possibleCoinPositions.RemoveAt(rand);
        }
    }

    private void SpawnObstacles(int runNum)
    {
        // Spawns 3rd run obstacles even on later runs
        if (runNum > 2)
        {
            runNum = 2;
        }

        if (obstacleObjects[runNum].Count > 0)
        {
            for (int i = 0; i < obstacleObjects[runNum].Count; i++)
            {
                Debug.Log("spawned a thing");
            }
        }

    }
}
