using UnityEngine;
using TMPro;
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
        fpsdisplay.text = (1.0 / Time.deltaTime).ToString();
    }
}
