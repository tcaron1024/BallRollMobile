using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    Text fpsdisplay;
    private void Start()
    {
        fpsdisplay = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        fpsdisplay.text = (1.0 / Time.deltaTime).ToString();
    }
}
