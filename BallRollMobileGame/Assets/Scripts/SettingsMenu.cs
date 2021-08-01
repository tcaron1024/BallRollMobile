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

    /// <summary>
    /// Slider for roll sound 
    /// </summary>
    public Slider rollSlider;

    [Tooltip("AudioMixers for music, SFX, and roll sound (in that order)")]
    [SerializeField] private AudioMixer[] mixers = new AudioMixer[3];

    [Tooltip("Holds whether or not music, SFX, and roll sound audio mixers (in that order) are muted")]
    [SerializeField] private bool[] mixerIsMuted = new bool[3];

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
        //rollSlider.onValueChanged.AddListener(HandleRollSliderValueChanged);
    }

    public void HandleVolumeSliderValueChanged(Slider thisSlider)
    {
        Debug.Log("Value = " + thisSlider.value);
        for(int i = 0; i < mixers.Length; i++)
        {
            if (mixerIsMuted[i])
            {
                mixers[i].SetFloat("MasterVolume", -80);
                Debug.Log("Muted mixer " + i);
            }
            else
            {
                mixers[i].SetFloat("MasterVolume", thisSlider.value);
                Debug.Log("Mixer " + i + " set to " + thisSlider.value);
            }
        }
    }

    public void GoBack()
    {
        SceneManager.UnloadSceneAsync("Settings");
    }

    public void muteVolume()
    {
        mixers[0].SetFloat("MasterVolume", -80);
    }

    public void muteSFX()
    {
        mixers[1].SetFloat("MasterVolume", -80);
    }

    public void muteRoll()
    {
        mixers[2].SetFloat("MasterVolume", -80);
    }
}
