/*****************************************************************************
// File Name :         HealthUI.cs
// Author :            Kyle Grenier
// Creation Date :     07/10/2021
//
// Brief Description : A component added to a TextMeshPro Text component that displays the stats of the
                       Health component provided.
*****************************************************************************/
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthUI : MonoBehaviour
{
    [Tooltip("The Health component to display the stats of. If null, will" +
        " find the Player's health component at runtime.")]
    [SerializeField] private Health healthComponent;

    private TextMeshProUGUI livesText;

    private void Awake()
    {
        livesText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (healthComponent == null)
            healthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        // Putting these in Start() so they don't run before we've found our Player's Health component.
        healthComponent.OnDamageTaken += UpdateLivesText;
        healthComponent.OnDeath += () => UpdateLivesText(0);

        UpdateLivesText(healthComponent.GetCurrentLives());
    }

    private void OnDisable()
    {
        healthComponent.OnDamageTaken -= UpdateLivesText;
        healthComponent.OnDeath -= () => UpdateLivesText(0);
    }



    private void UpdateLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
}
