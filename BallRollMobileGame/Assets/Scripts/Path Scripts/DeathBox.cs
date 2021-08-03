using UnityEngine;

public class DeathBox : MonoBehaviour
{
    /// <summary>
    /// Kills player when they collide with this object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            EventManager.PlayerDeath();
        }
    }
}
