using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    /// <summary>
    /// Background game object
    /// </summary>
    public GameObject bg;

    [Tooltip("Volume Slider")]
    public Slider volumeSlider;

    [Tooltip("Music mute toggle")]
    public Toggle musicToggle;

    [Tooltip("SFX mute toggle")]
    public Toggle sfxToggle;

    [Tooltip("AudioMixers for music, SFX, and roll sound (in that order)")]
    [SerializeField] private AudioMixer[] mixers = new AudioMixer[3];

    /// <summary>
    /// Holds whether or not music, SFX, and roll sound audio mixers (in that order) are muted
    /// </summary>
    private static bool[] mixerIsMuted = new bool[3];

    /// <summary>
    /// Current volume
    /// </summary>
    private static float currentVol = 0;

    /// <summary>
    /// Minimum audio mixer volume - setting it to this mutes the mixer
    /// </summary>
    private const float MIN_VOLUME = -80f;

    
    
    void Start()
    {
        bg.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

        // Sets volume slider to correct value
        volumeSlider.value = currentVol;

        // Sets toggles to off if their corresponding mixer is muted
        musicToggle.isOn = !mixerIsMuted[0];
        sfxToggle.isOn = !mixerIsMuted[1];

        musicToggle.onValueChanged.AddListener(MusicMuteUnmute);
        sfxToggle.onValueChanged.AddListener(SFXMuteUnmute);
    }

    public void HandleVolumeSliderValueChanged()
    {
        Debug.Log("Value = " + volumeSlider.value);
        currentVol = volumeSlider.value;

        for(int i = 0; i < mixers.Length; i++)
        {
            if (mixerIsMuted[i])
            {
                mixers[i].SetFloat("MasterVolume", MIN_VOLUME);
                Debug.Log("Muted mixer " + i);
            }
            else
            {
                mixers[i].SetFloat("MasterVolume", volumeSlider.value);
                Debug.Log("Mixer " + i + " set to " + volumeSlider.value);
            }
        }
    }

    public void GoBack()
    {
        SceneManager.UnloadSceneAsync("Settings");
    }

    /// <summary>
    /// Mutes/Unmutes music, depending on whether or not it is currently muted
    /// </summary>
    public void MusicMuteUnmute(bool unmuted)
    {
       
        Debug.Log("Starting unmute/mute - music is currently muted = " + mixerIsMuted[0]);

        // Switches whether mixer is muted or not
        mixerIsMuted[0] = !unmuted;

        // Unmutes music
        if (unmuted)
        {
            mixers[0].SetFloat("MasterVolume", currentVol);

            Debug.Log("Mixer volume set to - " + currentVol);
        }
        // Mutes music
        else
        {
            mixers[0].SetFloat("MasterVolume", MIN_VOLUME);

            Debug.Log("Mixer volume set to - " + MIN_VOLUME);
        }

        
    }

    /// <summary>
    /// Mutes/Unmutes SFX and roll sound, depending on whether or not it is currently muted
    /// </summary>
    public void SFXMuteUnmute(bool unmuted)
    {
       // Mutes/Unmutes roll and SFX mixers
        for (int i = 1; i < mixers.Length; i++)
        {
            // Switches whether mixer is muted or not
            mixerIsMuted[i] = !unmuted;

            // Unmutes mixer
            if (unmuted)
            {
                mixers[i].SetFloat("MasterVolume", currentVol);
            }
            // Mutes mixer
            else
            {
                mixers[i].SetFloat("MasterVolume", MIN_VOLUME);
            }    
        }
    }

    public void muteRoll()
    {
        
    }
}
