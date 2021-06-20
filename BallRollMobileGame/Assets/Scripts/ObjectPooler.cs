// Object Pooling Example
// Author: Owen Schaffer
// Code for this example based on https://www.youtube.com/watch?v=tdSmKaJvCoA
//
// Changes: 
// -FillsPoolWithInactiveObjects() encapsulated from Start() into its own method
// -Added ReturnObjectToPool() to return objects to the pool (This is not 
//  currently used because the spawn method adds the objects back to the pool, 
//  but could be used as well as or instead of adding them to the queue on spawn.)

using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // A list of Pool objects based on the Pool class
    public List<Pool> pools;

    // A Dictionary with a string as the Key and a Queue of GameObjects as the values
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPooler instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    void Start()
    {
        FillPoolsWithInactiveObjects();
    }

    // Fill the pools with inactive objects on Start()
    private void FillPoolsWithInactiveObjects()
    {
        foreach (Pool pool in pools)
        {
            // Create a queue of game objects to hold the prefabs in the pool
            Queue<GameObject> objectPool = new Queue<GameObject>();

            // For each prefab up to the size of the pool (set in inspector)...
            for (int i = 0; i < pool.numToSpawn; i++)
            {
                // Instantiate the prefab (also set in inspector) and assign it to obj
                int index = Random.Range(0, pool.prefabs.Length);
                GameObject obj = Instantiate(pool.prefabs[index]);
                // Set obj as inactive
                obj.SetActive(false);
                // Enqueue or add obj to the queue of objects (to the back of the line)
                objectPool.Enqueue(obj);
            }
            // Add the queue of objects to the dictionary of pools with that pool's 
            // string tag (set in inspector) as a label
            poolDictionary.Add(pool.tag, objectPool);
        }
    }


    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        // Check to make sure the Dictionary contains the pool with the tag passed into SpawnFromPool
        if (!poolDictionary.ContainsKey(tag))
        {
            // If you get this error, be sure you set the Pool's tag correctly in the inspector
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        // Dequeue or remove the object to spawn (from the front of the line)
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        // Set object to spawn to active
        objectToSpawn.SetActive(true);
        // Set the position and rotation of the object to what was passed into SpawnFromPool()
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // Add the object back to the queue of objects (to the back of the line)
        poolDictionary[tag].Enqueue(objectToSpawn);

        // If we spawned in a pooled object, call its OnSpawn method.
        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        pooledObject?.OnSpawn();

        // Return the object to spawn
        return objectToSpawn;
    }

    public void ReturnObjectToPool(string tag, GameObject objectToReturn)
    {
        // Set obj as inactive
        objectToReturn.SetActive(false);
    }

    public string GetRandomTag()
    {
        string[] keyArr = new string[poolDictionary.Keys.Count];
        poolDictionary.Keys.CopyTo(keyArr, 0);

        int index = Random.Range(0, poolDictionary.Keys.Count);
        return keyArr[index];
    }

}