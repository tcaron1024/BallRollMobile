using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            GameObject.Find("GameController").GetComponent<UIController>().ShowLoseScreen();
            //GameObject.Find("GameController").GetComponent<GameController>().EndLevel();
        }
    }
}
