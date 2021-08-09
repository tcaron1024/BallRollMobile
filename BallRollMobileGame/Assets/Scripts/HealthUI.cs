/*****************************************************************************
// File Name :         HealthUI.cs
// Author :            Kyle Grenier && Connor Riley (handle heart animation)
// Creation Date :     07/10/2021
//
// Brief Description : A component added to a TextMeshPro Text component that displays the stats of the
                       Health component provided.
*****************************************************************************/
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [Tooltip("The Health component to display the stats of. If null, will" +
        " find the Player's health component at runtime.")]
    [SerializeField] private Health healthComponent;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3; 


    private void Start()
    {
        if (healthComponent == null)
            healthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        // Putting these in Start() so they don't run before we've found our Player's Health component.
        healthComponent.OnDamageTaken += UpdateLivesText;
        healthComponent.OnDeath += () => UpdateLivesText(0);

        UpdateLivesText(healthComponent.GetCurrentLives());
        Debug.Log("start " + healthComponent.GetCurrentLives() + " lives");
   
    }

    private void OnDisable()
    {
        healthComponent.OnDamageTaken -= UpdateLivesText;
        healthComponent.OnDeath -= () => UpdateLivesText(0);
    }



    private void UpdateLivesText(int currentLives)
    {
        if(currentLives == 2 )
        {
            heart1.GetComponent<Animator>().SetBool("break", true);
        }
        if(currentLives == 1)
        {
            heart2.GetComponent<Animator>().SetBool("break", true);
        }
        if(currentLives == 0)
        {
            heart3.GetComponent<Animator>().SetBool("break", true);
        }
    }
}
