using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuBehavior : MonoBehaviour
{
    [Tooltip("AudioMixers for music, SFX, and roll sound (in that order)")]
    [SerializeField] private AudioMixer[] mixers = new AudioMixer[3];

    void Start()
    {
        // Checks if player has a current high score and sets it to 0 if not
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        // Checks if player has a current shop balance and sets it to 0 if not
        if (!PlayerPrefs.HasKey("ShopBalance"))
        {
            PlayerPrefs.SetInt("ShopBalance", 0);
        }

        

        // Sets volume on audio mixers to saved volume value
        foreach(AudioMixer am in mixers)
        {
            am.SetFloat("MasterVolume", PlayerPrefs.GetFloat("Volume", 0));
        }

        // Mutes music audio if player had it muted before
        if (PlayerPrefs.GetInt("MusicMuted", 0) == 1)
        {
            mixers[0].SetFloat("MasterVolume", -80f);
        }

        // Mutes SFX audio if player had it muted before
        if (PlayerPrefs.GetInt("SFXMuted", 0) == 1)
        {
            mixers[1].SetFloat("MasterVolume", -80f);
            mixers[2].SetFloat("MasterVolume", -80f);
        }
    }

    private IEnumerator ChangeScene(string name)
    {
        yield return new WaitForSeconds(.15f);
        LevelLoader.instance.LoadLevel(name);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine("ChangeScene", sceneName);
    }

    // Loads a scene on top of those already loaded.
    public void LoadSceneAdditive(string sceneName)
    {
        LevelLoader.instance.LoadLevelAdditive(sceneName);

        // Change music to appropriate for scene being loaded
        GameObject.FindObjectOfType<MusicHandler>().ChangeMusic(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
