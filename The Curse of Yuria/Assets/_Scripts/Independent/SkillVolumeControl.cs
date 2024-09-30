using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Independent
{
    public class SkillVolumeControl : MonoBehaviour
    {
        
        void Awake()
        {
            GetComponent<AudioSource>().volume = IAudioOptions.SFXVolume;
        }
    }
}