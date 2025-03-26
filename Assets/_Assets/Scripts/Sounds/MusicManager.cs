using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string MUSIC_VOLUME = "MusicVolume";
    public static MusicManager Instance;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(MUSIC_VOLUME,audioSource.volume);
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOLUME,audioSource.volume);
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME) * 100f;
    }
}
