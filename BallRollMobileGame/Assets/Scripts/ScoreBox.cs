using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("GameController").GetComponent<UIController>().IncreaseScore();
        }
    }
}
