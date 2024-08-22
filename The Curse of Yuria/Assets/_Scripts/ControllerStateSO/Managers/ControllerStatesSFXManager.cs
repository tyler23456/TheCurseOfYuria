using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TCOY.ControllerStates
{
    public class ControllerStatesSFXManager : MonoBehaviour
    {
        public static ControllerStatesSFXManager Instance { get; private set; }

        [SerializeField] AssetLabelReference StepFXGrassReference;
        [SerializeField] AssetLabelReference StepFXDirtReference;
        [SerializeField] AssetLabelReference StepFXWoodReference;
        [SerializeField] AssetLabelReference StepFXSnowReference;

        [SerializeField] AssetLabelReference StepFXGrassLandReference;
        [SerializeField] AssetLabelReference StepFXDirtLandReference;
        [SerializeField] AssetLabelReference StepFXWoodLandReference;
        [SerializeField] AssetLabelReference StepFXSnowLandReference;

        [SerializeField] AssetLabelReference jumpReference;

        [SerializeField] AssetLabelReference ladderLandReference;
        [SerializeField] AssetLabelReference ladderClimbReference;

        [SerializeField] AudioSourceManager audioSourceManager;

        List<AudioClip> StepSFXGrass = new List<AudioClip>();
        List<AudioClip> StepSFXDirt = new List<AudioClip>();
        List<AudioClip> StepSFXWood = new List<AudioClip>();
        List<AudioClip> StepSFXSnow = new List<AudioClip>();

        List<AudioClip> StepSFXGrassLand = new List<AudioClip>();
        List<AudioClip> StepSFXDirtLand = new List<AudioClip>();
        List<AudioClip> StepSFXWoodLand = new List<AudioClip>();
        List<AudioClip> StepSFXSnowLand = new List<AudioClip>();

        List<AudioClip> StepSFXJump = new List<AudioClip>();

        List<AudioClip> StepSFXLadderClimb = new List<AudioClip>();

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


            Addressables.LoadAssetsAsync<AudioClip>(StepFXGrassLandReference, (i) =>
            {
                StepSFXGrassLand.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(StepFXDirtLandReference, (i) =>
            {
                StepSFXDirtLand.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(StepFXWoodLandReference, (i) =>
            {
                StepSFXWoodLand.Add(i);

            }).WaitForCompletion();
            Addressables.LoadAssetsAsync<AudioClip>(StepFXSnowLandReference, (i) =>
            {
                StepSFXSnowLand.Add(i);

            }).WaitForCompletion();


            Addressables.LoadAssetsAsync<AudioClip>(jumpReference, (i) =>
            {
                StepSFXJump.Add(i);

            }).WaitForCompletion();

            Addressables.LoadAssetsAsync<AudioClip>(ladderClimbReference, (i) =>
            {
                StepSFXLadderClimb.Add(i);

            }).WaitForCompletion();

            stepSFXs.Add("GrassStepSFX", StepSFXGrass);
            stepSFXs.Add("DirtStepSFX", StepSFXDirt);
            stepSFXs.Add("WoodStepSFX", StepSFXWood);
            stepSFXs.Add("SnowStepSFX", StepSFXSnow);

            stepSFXs.Add("LandGrassStepSFX", StepSFXGrassLand);
            stepSFXs.Add("LandDirtStepSFX", StepSFXDirtLand);
            stepSFXs.Add("LandWoodStepSFX", StepSFXWoodLand);
            stepSFXs.Add("LandSnowStepSFX", StepSFXSnowLand);

            stepSFXs.Add("JumpStepSFX", StepSFXDirtLand);

            stepSFXs.Add("LadderClimbStepSFX", StepSFXLadderClimb);
        }

        public void PlayStepSFX(string groundType, AudioSource audioSource, float minVolume = 1f, float maxVolume = 1f, float minPitch = 1f, float maxPitch = 1f)
        {
            audioSourceManager.Play(audioSource, stepSFXs[groundType], minVolume, maxVolume, minPitch, maxPitch);
        }
    }
}