using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAudioOptions
{
    private static float _musicVolume = 1f;
    private static float _ambienceVolume = 1f;

    static float musicVolume { get { return _musicVolume; } set { _musicVolume = value; OnMusicVolumeChanged.Invoke(_musicVolume); } }
    static float ambienceVolume { get { return _ambienceVolume; } set { _ambienceVolume = value; OnAmbienceVolumeChanged.Invoke(_ambienceVolume); } }

    static float SFXVolume { get; set; } = 1f;
    static float UIVolume { get; set; } = 1f;
    static float StepSFXVolume { get; set; } = 1f;

    static Action<float> OnMusicVolumeChanged = (volume) => { };
    static Action<float> OnAmbienceVolumeChanged = (volume) => { };
}
