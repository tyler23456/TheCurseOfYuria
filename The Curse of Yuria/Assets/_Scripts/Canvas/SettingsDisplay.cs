using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TCOY.Canvas
{
    public class SettingsDisplay : MonoBehaviour
    {
        [SerializeField] GameObject leftPanel;
        [SerializeField] GameObject rightPanel;
        [SerializeField] Text heading;
        [SerializeField] Text description;

        [SerializeField] Slider musicVolume;
        [SerializeField] Slider ambienceVolume;
        [SerializeField] Slider SFXVolume;
        [SerializeField] Slider UIVolume;
        [SerializeField] Slider StepSFXVolume;

        void OnEnable()
        {
            leftPanel.SetActive(false);
            rightPanel.SetActive(false);

            musicVolume.onValueChanged.RemoveAllListeners();
            ambienceVolume.onValueChanged.RemoveAllListeners();
            SFXVolume.onValueChanged.RemoveAllListeners();
            UIVolume.onValueChanged.RemoveAllListeners();
            StepSFXVolume.onValueChanged.RemoveAllListeners();

            musicVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
            ambienceVolume.onValueChanged.AddListener(OnAmbienceVolumeChanged);
            SFXVolume.onValueChanged.AddListener(OnSFXVolumeChanged);
            UIVolume.onValueChanged.AddListener(OnUIVolumeChanged);
            StepSFXVolume.onValueChanged.AddListener(OnStepSFXVolumeChanged);

            musicVolume.value = IAudioOptions.musicVolume;
            ambienceVolume.value = IAudioOptions.ambienceVolume;
            SFXVolume.value = IAudioOptions.SFXVolume;
            UIVolume.value = IAudioOptions.UIVolume;
            StepSFXVolume.value = IAudioOptions.StepSFXVolume;

            heading.text = "";
            description.text = "";
        }

        void OnMusicVolumeChanged(float volume)
        {
            IAudioOptions.musicVolume = volume;
        }

        void OnAmbienceVolumeChanged(float volume)
        {
            IAudioOptions.ambienceVolume = volume;
        }

        void OnSFXVolumeChanged(float volume)
        {
            IAudioOptions.SFXVolume = volume;
        }

        void OnUIVolumeChanged(float volume)
        {
            IAudioOptions.UIVolume = volume;
        }

        void OnStepSFXVolumeChanged(float volume)
        {
            IAudioOptions.StepSFXVolume = volume;
        }
    }
}