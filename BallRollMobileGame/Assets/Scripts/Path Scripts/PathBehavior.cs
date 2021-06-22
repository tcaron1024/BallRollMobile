using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBehavior : MonoBehaviour
{

    [Tooltip("Coin Prefab")]
    [SerializeField] private GameObject coin;

    [Tooltip("Array of possible coin spawn locations for this path")]
    [SerializeField] private Transform[] possibleCoinPositions;

    [Tooltip("Gameobject to spawn coins under")]
    [SerializeField] private Transform coinParent;


    /// <summary>
    /// Spawns given number of coins on this path
    /// </summary>
    /// <param name="numToSpawn"></param>
    public void SpawnCoins(int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            Instantiate(coin, possibleCoinPositions[i].position, possibleCoinPositions[i].rotation, coinParent);
        }
    }
}
