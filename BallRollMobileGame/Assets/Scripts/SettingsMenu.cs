using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    /// <summary>
    /// Background game object
    /// </summary>
    public GameObject bg;

    /// <summary>
    /// Slider for roll sound 
    /// </summary>
    public Slider rollSlider;

    /// <summary>
    /// Mixer for Roll Sound
    /// </summary>
    public AudioMixer roll;

    /// <summary>
    /// Volume Multipler
    /// </summary>
    private float multiplier = 30.0f; 



    // Start is called before the first frame update
    void Start()
    {
        bg.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        rollSlider.onValueChanged.AddListener(HandleRollSliderValueChanged);
    }

    private void HandleRollSliderValueChanged(float value)
    {
        roll.SetFloat("MasterVolume", Mathf.Log10(value) * multiplier);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
