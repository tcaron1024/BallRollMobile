using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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

    
    
    void Awake()
    {
        bg.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

        currentVol = PlayerPrefs.GetFloat("Volume", 0);

        // Sets volume slider to correct value
        volumeSlider.value = currentVol;

        // Sets whether or not mixers should be muted
        mixerIsMuted[0] = PlayerPrefs.GetInt("MusicMuted") == 0 ? false : true;
        mixerIsMuted[1] = PlayerPrefs.GetInt("SFXMuted") == 0 ? false : true;
        mixerIsMuted[2] = PlayerPrefs.GetInt("SFXMuted") == 0 ? false : true;


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
        PlayerPrefs.SetFloat("Volume", currentVol);


        for(int i = 0; i < mixers.Length; i++)
        {
            // Mutes mixer or sets it to correct volume
            mixers[i].SetFloat("MasterVolume", mixerIsMuted[i] ? MIN_VOLUME : currentVol);
        }
    }

    public void GoBack()
    {
        StartCoroutine(CloseScene());
    }

    private IEnumerator CloseScene()
    {
        yield return new WaitForSecondsRealtime(.15f);
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

        // Mutes mixer or sets it to correct volume
        mixers[0].SetFloat("MasterVolume", unmuted ? currentVol : MIN_VOLUME);

        // Holds whether or not music is muted in PlayerPrefs for later startups
        PlayerPrefs.SetInt("MusicMuted", unmuted ? 0 : 1);
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

            // Mutes mixer or sets it to correct volume
            mixers[i].SetFloat("MasterVolume", unmuted ? currentVol : MIN_VOLUME);
        }

        // Holds whether or not SFX is muted in PlayerPrefs for later startups
        PlayerPrefs.SetInt("SFXMuted", unmuted ? 0 : 1);
    }

}
