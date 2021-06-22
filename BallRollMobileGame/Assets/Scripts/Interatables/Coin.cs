using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<UIController>().CollectCoin();
            Destroy(gameObject);
            // TODO: play sound on pickup
        }
    }
}
