using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TCOY.Interactables
{
    public class InteractableSFXManager : MonoBehaviour
    {
        public static InteractableSFXManager Instance { get; private set; }

        [SerializeField] AssetLabelReference grabItemReference;
        [SerializeField] AssetLabelReference openCrateReference;
        [SerializeField] AssetLabelReference openChestReference;
        [SerializeField] AssetLabelReference openSackReference;
        [SerializeField] AssetLabelReference searchEnemyReference;

        [SerializeField] AudioSourceManager audioSourceManager;

        List<AudioClip> grabItemSFX = new List<AudioClip>();
        List<AudioClip> openCrateSFX = new List<AudioClip>();
        List<AudioClip> openChestSFX = new List<AudioClip>();
        List<AudioClip> openSackSFX = new List<AudioClip>();
        List<AudioClip> searchEnemySFX = new List<AudioClip>();

        void Awake()
        {
            Instance = this;

            Addressables.LoadAssetsAsync<AudioClip>(grabItemReference, (i) =>
            {
                grabItemSFX.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(openCrateReference, (i) =>
            {
                openCrateSFX.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(openChestReference, (i) =>
            {
                openChestSFX.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(openSackReference, (i) =>
            {
                openSackSFX.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(searchEnemyReference, (i) =>
            {
                searchEnemySFX.Add(i);

            }).WaitForCompletion();
        }

        public void PlayGrabItemSFX(AudioSource audioSource)
        {
            audioSourceManager.Play(audioSource, grabItemSFX);
        }

        public void PlayOpenCrateSFX(AudioSource audioSource)
        {
            audioSourceManager.Play(audioSource, openCrateSFX);
        }

        public void PlayOpenChestSFX(AudioSource audioSource)
        {
            audioSourceManager.Play(audioSource, openChestSFX);
        }

        public void PlayOpenSackSFX(AudioSource audioSource)
        {
            audioSourceManager.Play(audioSource, openSackSFX);
        }

        public void PlaySearchEnemySFX(AudioSource audioSource)
        {
            audioSourceManager.Play(audioSource, searchEnemySFX);
        }
    }
}