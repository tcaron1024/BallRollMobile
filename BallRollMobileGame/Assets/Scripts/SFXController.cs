using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickUpSound;
    [SerializeField] private AudioClip coinAddUpSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip portalSound;


    AudioSource sfxSource;
    private void OnEnable()
    {
        // Subscribe to the OnPlayerDeath event so we 
        // display the loss screen on death.
        EventManager.OnPlayerDeath += PlayLoss;
        EventManager.OnCoinPickup += PlayCoinPickup;
        EventManager.OnLevelComplete += PlayPortal;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= PlayLoss;
        EventManager.OnCoinPickup -= PlayCoinPickup;
        EventManager.OnLevelComplete -= PlayPortal;
    }

    private void Awake()
    {
        sfxSource = GetComponent<AudioSource>();
    }

    public void PlayCoinPickup()
    {
        PlaySound(coinPickUpSound);
    }

    public void PlayLoss()
    {
        PlaySound(deathSound);
    }

    public void PlayAddUp()
    {
        PlaySound(coinAddUpSound);
    }

    public void PlayPortal()
    {
        PlaySound(portalSound);
    }

    private void PlaySound(AudioClip sound)
    {
        sfxSource.clip = sound;
        sfxSource.PlayScheduled(0);
    }

}
