using UnityEngine;
using TMPro;
using System;
public class FPSDisplay : MonoBehaviour
{
    TextMeshProUGUI fpsdisplay;
    private void Start()
    {
        fpsdisplay = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        fpsdisplay.text = Math.Round((1.0 / Time.deltaTime), 3).ToString();
    }
}
