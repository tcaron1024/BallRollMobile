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
    [Tooltip("The Health component to display the stats of.")]
    [SerializeField] private Health healthComponent;

    private TextMeshProUGUI livesText;

    private void Awake()
    {
        livesText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateLivesText(healthComponent.GetCurrentLives());
    }

    private void OnEnable()
    {
        healthComponent.OnDamageTaken += UpdateLivesText;
        healthComponent.OnDeath += () => UpdateLivesText(0);
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
