using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TCOY.AStar
{
    public class AStarSFXManager : MonoBehaviour
    {
        public static AStarSFXManager Instance { get; private set; }


        [SerializeField] AssetLabelReference StepFXGrassReference;
        [SerializeField] AssetLabelReference StepFXDirtReference;
        [SerializeField] AssetLabelReference StepFXWoodReference;
        [SerializeField] AssetLabelReference StepFXSnowReference;

        [SerializeField] AudioSourceManager audioSourceManager;

        [SerializeField] float minVolume = 0.6f;
        [SerializeField] float maxVolume = 1.4f;
        [SerializeField] float minPitch = 0.6f;
        [SerializeField] float maxPitch = 1.4f;

        List<AudioClip> StepSFXGrass = new List<AudioClip>();
        List<AudioClip> StepSFXDirt = new List<AudioClip>();
        List<AudioClip> StepSFXWood = new List<AudioClip>();
        List<AudioClip> StepSFXSnow = new List<AudioClip>();

        Dictionary<string, List<AudioClip>> stepSFXs = new Dictionary<string, List<AudioClip>>();


        public void Awake()
        {
            Instance = this;

            Addressables.LoadAssetsAsync<AudioClip>(StepFXGrassReference, (i) =>
            {
                StepSFXGrass.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(StepFXDirtReference, (i) =>
            {
                StepSFXDirt.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(StepFXWoodReference, (i) =>
            {
                StepSFXWood.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(StepFXSnowReference, (i) =>
            {
                StepSFXSnow.Add(i);

            }).WaitForCompletion();

            stepSFXs.Add("GrassStepSFX", StepSFXGrass);
            stepSFXs.Add("DirtStepSFX", StepSFXDirt);
            stepSFXs.Add("WoodStepSFX", StepSFXWood);
            stepSFXs.Add("SnowStepSFX", StepSFXSnow);
        }

        public void PlayStepSFX(string groundType, AudioSource audioSource)
        {
            audioSourceManager.PlaySFX(stepSFXs[groundType]);
        }
    }
}