using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    AudioSource music;
    AudioSource atmosphere;
    AudioSource SFX;

    void Awake()
    {
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

    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

    public void PlaySFX(List<AudioClip> clips)
    {
        Play(SFX, clips);
    }

    public void Play(AudioSource audioSource, List<AudioClip> clips, float minVolume = 1f, float maxVolume = 1f, float minPitch = 1f, float maxPitch = 1f)
    {
        float volume = Random.Range(minVolume, maxVolume);
        float pitch = Random.Range(minPitch, maxPitch);

        audioSource.volume = volume;
        audioSource.pitch = pitch;

        int index = Random.Range(0, clips.Count);

        audioSource.PlayOneShot(clips[index], SFX.volume);
    }
}
