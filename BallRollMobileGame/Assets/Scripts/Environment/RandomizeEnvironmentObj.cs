using UnityEngine;

public class RandomizeEnvironmentObj : MonoBehaviour
{
    [Tooltip("Minimum possible scale for environment object")]
    [SerializeField] private float minScale = .8f;

    [Tooltip("Maximum possible scale for environment object")]
    [SerializeField] private float maxScale = 1.2f;

    /// <summary>
    /// Randomize scale and Y rotation of the environment object
    /// </summary>
    void Start()
    {
        // Randomize scale
        float randScale = Random.Range(minScale, maxScale);
        transform.localScale *= randScale;

        // Randomize rotation
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, Random.Range(0, 360), transform.rotation.eulerAngles.z));
    }

}
