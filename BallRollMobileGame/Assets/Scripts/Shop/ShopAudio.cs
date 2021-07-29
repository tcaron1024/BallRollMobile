using UnityEngine;
using UnityEngine.Events;

public class ShopAudio : MonoBehaviour
{
    /// <summary>
    /// Audio source to play sounds in the shop
    /// </summary>
    AudioSource thisSource;

    private void Awake()
    {
        thisSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays given audio clip
    /// </summary>
    /// <param name="sound">Clip to play</param>
    public void PlaySound(AudioClip sound)
    {
        thisSource.clip = sound;
        thisSource.PlayScheduled(0);
    }
}
