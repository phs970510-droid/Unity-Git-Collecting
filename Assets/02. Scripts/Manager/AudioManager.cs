using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource audioSource;
    public Vector2 volume = new Vector2(0.5f, 0.9f);
    public Vector2 pitch = new Vector2(0.8f, 1.2f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (audioSource == null) return;

        audioSource.volume = Random.Range(volume.x, volume.y);
        audioSource.pitch = Random.Range(pitch.x, pitch.y);

        audioSource.clip = clip;

        audioSource.Stop();
        audioSource.Play();
    }
}
