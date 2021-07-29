using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickUpSound;
    [SerializeField] private AudioClip coinAddUpSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip portalSound;
    [SerializeField] private AudioClip icicleSound;
    [SerializeField] private AudioClip quicksandSound;
    [SerializeField] private AudioClip spikeSound;
    [SerializeField] private AudioClip pyramidSound;

    AudioSource sfxSource;
    private void OnEnable()
    {
        EventManager.OnPlayerDeath += PlayLoss;
        EventManager.OnCoinPickup += PlayCoinPickup;
        EventManager.OnObstacleCollision += PlayObstacle;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerDeath -= PlayLoss;
        EventManager.OnCoinPickup -= PlayCoinPickup;
        EventManager.OnObstacleCollision -= PlayObstacle;
    }

    private void Awake()
    {
        sfxSource = GetComponent<AudioSource>();
        if (GameController.currentLevelNum > 1)
        {
            PlayPortal();
        }
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

    public void PlayObstacle(string name)
    {
        Debug.Log("playing obstacle sound");
        switch (name)
        {
            case "Quick Sand":
                PlaySound(quicksandSound);
                break;
            case "Spikes":
                PlaySound(spikeSound);
                break;
            case "Icicles":
                PlaySound(icicleSound);
                break;
            default:
                PlaySound(pyramidSound);
                break;
        }


    }

    public void PlaySound(AudioClip sound)
    {
        sfxSource.clip = sound;
        sfxSource.PlayScheduled(0);
    }

}
