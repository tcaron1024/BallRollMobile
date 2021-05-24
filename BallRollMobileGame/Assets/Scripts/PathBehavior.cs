using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBehavior : MonoBehaviour
{
    private PathSpawner ps;
    private void Start()
    {
        ps = GameObject.Find("GameController").GetComponent<PathSpawner>();
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ps.SpawnPath();
            Destroy(gameObject, 1.5f);
        }
    }
}
