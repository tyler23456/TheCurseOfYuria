using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    AudioSource music;
    AudioSource atmosphere;
    AudioSource SFX;

    void Awake()
    {
        Instance = this;
        music = transform.GetChild(0).GetComponent<AudioSource>();
        atmosphere = transform.GetChild(1).GetComponent<AudioSource>();
        SFX = transform.GetChild(2).GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        music.clip = clip;
        music.volume = volume;
        music.pitch = pitch;
        music.Play();
    }

    public void PlayAtmosphere(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        atmosphere.clip = clip;
        atmosphere.volume = volume;
        atmosphere.pitch = pitch;
        atmosphere.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        SFX.pitch = pitch;
        SFX.PlayOneShot(clip, volume);
    }
}
