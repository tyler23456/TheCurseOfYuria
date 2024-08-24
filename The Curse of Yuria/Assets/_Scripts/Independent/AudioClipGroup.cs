using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Independent
{
    public class AudioClipGroup : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] List<AudioClip> audioClips;

        void Awake()
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
            audioSource.Play();
        }
    }
}