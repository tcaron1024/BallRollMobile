using System.Collections;
using UnityEngine;

public class SnowballSpawner : MonoBehaviour
{
    [Tooltip("How often the snowballs spawn")]
    [SerializeField] private float spawnInterval;

    [Tooltip("Force at which the snowball is spawned")]
    [SerializeField] private Vector3 spawnForce;

    [Tooltip("Snowball object to spawn")]
    [SerializeField] private GameObject snowball;

    /// <summary>
    /// Transform to spawn the snowballs under
    /// </summary>
    private Transform spawnParent;
    void Start()
    {
        StartCoroutine(SpawnSnowballs());
    }


    private IEnumerator SpawnSnowballs()
    {
       
        while (true)
        {
            // Spawn ball, add spawn force, destroy after spawn interval
            GameObject ball = Instantiate(snowball, transform.position, transform.rotation, spawnParent);
            ball.GetComponent<Rigidbody>().AddForce(spawnForce);
            Destroy(ball, spawnInterval);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
