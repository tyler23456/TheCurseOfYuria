using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TCOY.ItemTypes
{
    public class ArmTypeSFXManager : MonoBehaviour
    {
        public static ArmTypeSFXManager Instance { get; private set; }

        [SerializeField] AssetLabelReference swingReference;
        [SerializeField] AssetLabelReference releaseBowReference;

        [SerializeField] AudioSourceManager audioSourceManager;

        List<AudioClip> swingSFX = new List<AudioClip>();
        List<AudioClip> releaseBowSFX = new List<AudioClip>();

        void Awake()
        {
            Instance = this;

            Addressables.LoadAssetsAsync<AudioClip>(swingReference, (i) =>
            {
                swingSFX.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(releaseBowReference, (i) =>
            {
                releaseBowSFX.Add(i);

            }).WaitForCompletion();
        }

        public void PlaySwingSFX(AudioSource audioSource)
        {
            audioSourceManager.PlaySFX(audioSource, swingSFX);
        }

        public void PlayReleaseBowSFX(AudioSource audioSource)
        {
            audioSourceManager.PlaySFX(audioSource, releaseBowSFX);
        }
    }
}
