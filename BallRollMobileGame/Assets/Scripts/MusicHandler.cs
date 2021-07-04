using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip[] gameplayMusic = new AudioClip[3];
    [SerializeField] private AudioClip shopMusic;

    private List<AudioClip> availableGameplayMusic = new List<AudioClip>();


    private AudioSource source1;
    private AudioSource source2;

    public enum MusicType
    {
        MENU, GAMEPLAY, SHOP
    };

    private MusicType currentMusic = MusicType.MENU;
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        source1 = transform.GetChild(0).GetComponent<AudioSource>();
        source2 = transform.GetChild(1).GetComponent<AudioSource>();

        foreach (AudioClip clip in gameplayMusic)
        {
            availableGameplayMusic.Add(clip);
        }
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
        PlayMusic();
    }


    private void PlayMusic()
    {
        // Finds which type of music should be played
        switch (currentMusic)
        {
            // Menu music should be played
            case MusicType.MENU:
                source1.clip = menuMusic;
                break;

            // Gameplay music should be played
            case MusicType.GAMEPLAY:

                // Fills list of available music if it is empty
                if (availableGameplayMusic.Count == 0)
                {
                    foreach (AudioClip clip in gameplayMusic)
                    {
                        availableGameplayMusic.Add(clip);
                    }
                }

                // Plays random music from available list
                int rand = Random.Range(0, availableGameplayMusic.Count);
                source1.clip = availableGameplayMusic[rand];
                availableGameplayMusic.RemoveAt(rand);
                break;

            // Shop music should be played
            case MusicType.SHOP:
                source1.clip = shopMusic;
                break;

            // default means gameplay music should be played -- should never reach here
            default:
                if (availableGameplayMusic.Count == 0)
                {
                    foreach (AudioClip clip in gameplayMusic)
                    {
                        availableGameplayMusic.Add(clip);
                    }
                }
                int rand2 = Random.Range(0, availableGameplayMusic.Count);
                source1.clip = availableGameplayMusic[rand2];
                availableGameplayMusic.RemoveAt(rand2);
                break;
        }

        source1.PlayScheduled(0);
        source2.Stop();
    }
}
