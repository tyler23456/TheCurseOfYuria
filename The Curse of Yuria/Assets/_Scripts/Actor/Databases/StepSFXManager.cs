using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TCOY.AStar
{
    public class StepSFXManager : MonoBehaviour
    {
        public static StepSFXManager Instance { get; private set; }


        [SerializeField] AssetLabelReference StepFXGrassReference;
        [SerializeField] AssetLabelReference StepFXDirtReference;
        [SerializeField] AssetLabelReference StepFXWoodReference;
        [SerializeField] AssetLabelReference StepFXSnowReference;

        [SerializeField] float minVolume = 0.6f;
        [SerializeField] float maxVolume = 1.4f;
        [SerializeField] float minPitch = 0.6f;
        [SerializeField] float maxPitch = 1.4f;

        List<AudioClip> StepFXGrass = new List<AudioClip>();
        List<AudioClip> StepFXDirt = new List<AudioClip>();
        List<AudioClip> StepFXWood = new List<AudioClip>();
        List<AudioClip> StepFXSnow = new List<AudioClip>();

        Dictionary<string, List<AudioClip>> stepFXs = new Dictionary<string, List<AudioClip>>();


        public void Awake()
        {
            Instance = this;

            Addressables.LoadAssetsAsync<AudioClip>(StepFXGrassReference, (i) =>
            {
                StepFXGrass.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(StepFXDirtReference, (i) =>
            {
                StepFXDirt.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(StepFXWoodReference, (i) =>
            {
                StepFXWood.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(StepFXSnowReference, (i) =>
            {
                StepFXSnow.Add(i);

            }).WaitForCompletion();


            stepFXs.Add("GrassStepSFX", StepFXGrass);
            stepFXs.Add("DirtStepSFX", StepFXDirt);
            stepFXs.Add("WoodStepSFX", StepFXWood);
            stepFXs.Add("SnowStepSFX", StepFXSnow);
        }


        public void Play(string groundType, AudioSource audioSource)
        {
            float volume = Random.Range(minVolume, maxVolume);
            float pitch = Random.Range(minPitch, maxPitch);

            audioSource.volume = volume;
            audioSource.pitch = pitch;

            int index = Random.Range(0, stepFXs[groundType].Count);

            audioSource.PlayOneShot(stepFXs[groundType][index]);
        }
    }
}