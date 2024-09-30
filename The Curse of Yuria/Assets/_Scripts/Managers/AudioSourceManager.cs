using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    AudioSource music;
    AudioSource ambience;
    AudioSource SFX;
    AudioSource dialogue;

    void Awake()
    {
        music = transform.GetChild(0).GetComponent<AudioSource>();
        ambience = transform.GetChild(1).GetComponent<AudioSource>();
        SFX = transform.GetChild(2).GetComponent<AudioSource>();
        dialogue = transform.GetChild(3).GetComponent<AudioSource>();

        //set up on changed methods here
    }

    public void PlayMusic(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        music.clip = clip;
        music.volume = volume * IAudioOptions.musicVolume; ;
        music.pitch = pitch;
        music.Play();
    }

    public void PlayAmbience(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        ambience.clip = clip;
        ambience.volume = volume * IAudioOptions.ambienceVolume; ;
        ambience.pitch = pitch;
        ambience.Play();
    }

    public void PlayUI(AudioClip clip)
    {
        SFX.PlayOneShot(clip, IAudioOptions.UIVolume);
    }

    public void PlayUI(List<AudioClip> clips)
    {
        Play(SFX, clips, IAudioOptions.UIVolume, IAudioOptions.UIVolume);
    }

    public void PlayStepSFX(AudioSource audioSource, List<AudioClip> clips, float minVolume = 1f, float maxVolume = 1f, float minPitch = 1f, float maxPitch = 1f)
    {
        Play(audioSource, clips, minVolume * IAudioOptions.StepSFXVolume, maxVolume * IAudioOptions.StepSFXVolume, minPitch, maxPitch);
    }

    public void PlaySFX(AudioSource audioSource, List<AudioClip> clips, float minVolume = 1f, float maxVolume = 1f, float minPitch = 1f, float maxPitch = 1f)
    {
        Play(audioSource, clips, minVolume * IAudioOptions.SFXVolume, maxVolume * IAudioOptions.SFXVolume, minPitch, maxPitch);
    }

    void Play(AudioSource audioSource, List<AudioClip> clips, float minVolume = 1f, float maxVolume = 1f, float minPitch = 1f, float maxPitch = 1f)
    {
        float volume = Random.Range(minVolume, maxVolume);
        float pitch = Random.Range(minPitch, maxPitch);

        audioSource.volume = volume;
        audioSource.pitch = pitch;

        int index = Random.Range(0, clips.Count);

        audioSource.PlayOneShot(clips[index], SFX.volume);
    }

    public void PlayDialogue(List<AudioClip> clips, float minVolume = 1f, float maxVolume = 1f, float minPitch = 1f, float maxPitch = 1f)
    {
        if (dialogue.isPlaying)
            return;

        float volume = Random.Range(minVolume, maxVolume);
        float pitch = Random.Range(minPitch, maxPitch);

        dialogue.volume = volume * IAudioOptions.UIVolume;
        dialogue.pitch = pitch;

        int index = Random.Range(0, clips.Count);

        dialogue.clip = clips[index];
        dialogue.Play();
    }

    public void StopDialogue()
    {  
        dialogue.Stop();
    }
}
