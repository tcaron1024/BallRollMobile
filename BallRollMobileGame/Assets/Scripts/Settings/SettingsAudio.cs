using UnityEngine;
using UnityEngine.UI;

public class SettingsAudio : MonoBehaviour
{
    /// <summary>
    /// Audio source on this object
    /// </summary>
    private AudioSource thisSource;

    [Tooltip("Music mute toggle")]
    public Toggle musicToggle;

    [Tooltip("SFX mute toggle")]
    public Toggle sfxToggle;

    [Tooltip("Volume Slider")]
    public Slider volumeSlider;

    [Tooltip("Sensitivity Slider")]
    public Slider sensitivitySlider;


    [Tooltip("Audio clip to play when player toggles music/sfx on/off")]
    public AudioClip toggleAudio;

    [Tooltip("Audio clip to play when player toggles music/sfx on/off")]
    public AudioClip sliderAudio;


    void Start()
    {
        thisSource = GetComponent<AudioSource>();

        musicToggle.onValueChanged.AddListener(delegate { AddToggleSounds(); });
        sfxToggle.onValueChanged.AddListener(delegate { AddToggleSounds(); });

        volumeSlider.onValueChanged.AddListener(delegate { AddSliderSounds(); });
        sensitivitySlider.onValueChanged.AddListener(delegate { AddSliderSounds(); });
    }

    /// <summary>
    /// Plays given audio clip
    /// </summary>
    /// <param name="toPlay"></param>
    public void PlaySound(AudioClip toPlay)
    {
        thisSource.clip = toPlay;
        thisSource.PlayScheduled(0);
    }

    /// <summary>
    /// Plays toggle sound when toggles are pressed
    /// </summary>
    public void AddToggleSounds()
    {
        PlaySound(toggleAudio);
    }

    /// <summary>
    /// Player slider sound when slider value is changed
    /// </summary>
    public void AddSliderSounds()
    {
        PlaySound(sliderAudio);
    }
}
