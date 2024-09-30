using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAndAmbienceManager : MonoBehaviour
{
    public static MusicAndAmbienceManager Instance { get; private set; }

    [SerializeField] AudioSource music;
    [SerializeField] AudioSource ambience;

    private void Awake()
    {
        Instance = this;

        IAudioOptions.OnMusicVolumeChanged = (volume) => { music.volume = volume; };
        IAudioOptions.OnAmbienceVolumeChanged = (volume) => { ambience.volume = volume; };
    }

    public void SetAndPlayMusic(AudioClip clip)
    {
        if (music.clip != null && music.clip.name == clip.name)
            return;

        music.clip = clip;
        music.Play();
    }

    public void SetAndPlayAmbience(AudioClip clip)
    {
        if (ambience.clip != null && ambience.clip.name == clip.name)
            return;

        ambience.clip = clip;
        ambience.Play();
    }

    public void SetMusicVolume(float volume)
    {
        music.volume = volume;  
    }

    public void SetAmbienceVolume(float volume)
    {
        ambience.volume = volume;
    }
}
