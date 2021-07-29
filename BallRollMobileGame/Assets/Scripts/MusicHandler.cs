using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicHandler : MonoBehaviour
{
    [Tooltip("Amount of time it takes for volume to fade to desired volume")]
    public float volumeFadeTime = 1f;

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip[] gameplayMusic = new AudioClip[3];
    [SerializeField] private AudioClip shopMusic;

    private List<AudioClip> availableGameplayMusic = new List<AudioClip>();

    /// <summary>
    /// Desired volume for music
    /// </summary>
    private float desiredVolume;

    /// <summary>
    /// Audio source that plays the music
    /// </summary>
    private AudioSource musicSource;

    public enum MusicType
    {
        MENU, GAMEPLAY, SHOP
    };
    [Tooltip("Current music type being played")]
    [SerializeField] private MusicType currentMusic = MusicType.MENU;
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        foreach (AudioClip clip in gameplayMusic)
        {
            availableGameplayMusic.Add(clip);
        }

        desiredVolume = musicSource.volume;
    }

    public void ChangeMusic(string newMusic)
    {
        if (newMusic == "Menu")
        {
            currentMusic = MusicType.MENU;
        }
        else if (newMusic == "Shop")
        {
            currentMusic = MusicType.SHOP;
        }
        else
        {
            currentMusic = MusicType.GAMEPLAY;
        }
        FindMusic();
    }

    /// <summary>
    /// Determines which music to play
    /// </summary>
    private void FindMusic()
    {
        CancelInvoke("FindMusic");

        // Finds which type of music should be played
        switch (currentMusic)
        {
            // Menu music should be played
            case MusicType.MENU:
                StartCoroutine(FadeOutMusic(menuMusic));
                break;

            // Shop music should be played
            case MusicType.SHOP:
                StartCoroutine(FadeOutMusic(shopMusic)); ;
                break;

            // Default means gameplay music should be played 
            default:
                // Fills list of available music if it is empty
                if (availableGameplayMusic.Count < 1)
                {
                    foreach (AudioClip clip in gameplayMusic)
                    {
                        availableGameplayMusic.Add(clip);
                    }
                }

                // Plays random music from available list
                int rand = Random.Range(0, availableGameplayMusic.Count);
                StartCoroutine(FadeOutMusic(availableGameplayMusic[rand]));
                availableGameplayMusic.RemoveAt(rand);
                break;
        }

    }

    /// <summary>
    /// Fades out current music and fades in new music
    /// </summary>
    /// <param name="newMusic">New music to fade in</param>
    /// <returns></returns>
    private IEnumerator FadeOutMusic(AudioClip newMusic)
    {
        Debug.Log("new clip = " + newMusic.name);

        float t = 0;

        while (t < volumeFadeTime)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(desiredVolume, 0, t / volumeFadeTime);
            yield return null;
        }

        musicSource.clip = newMusic;
        musicSource.PlayScheduled(0);

        t = 0;
        if (currentMusic == MusicType.GAMEPLAY)
        {
            Invoke("FindMusic", musicSource.clip.length);
        }


        while (t < volumeFadeTime)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, desiredVolume, t / volumeFadeTime);
            yield return null;
        }

        musicSource.volume = desiredVolume;



    }
}
