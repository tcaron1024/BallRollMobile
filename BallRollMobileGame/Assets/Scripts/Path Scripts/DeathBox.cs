using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            EventManager.PlayerDeath();
            //GameObject.Find("GameController").GetComponent<GameController>().EndLevel();
        }
    }
}
