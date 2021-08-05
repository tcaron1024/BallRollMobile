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

    [Tooltip("Audio clip to play when player toggles music/sfx on/off")]
    public AudioClip toggleAudio;


    void Start()
    {
        thisSource = GetComponent<AudioSource>();

        musicToggle.onValueChanged.AddListener(AddToggleSounds);
        sfxToggle.onValueChanged.AddListener(AddToggleSounds);
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

    public void AddToggleSounds(bool unUsed)
    {
        PlaySound(toggleAudio);
    }
}
