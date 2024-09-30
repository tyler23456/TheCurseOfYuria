using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class CalculationTypeSFXManager : MonoBehaviour
{
    public static CalculationTypeSFXManager Instance { get; private set; }

    [SerializeField] AssetLabelReference hitReference;

    [SerializeField] AudioSourceManager audioSourceManager;

    List<AudioClip> hitSFX = new List<AudioClip>();

    void Awake()
    {
        Instance = this;

        Addressables.LoadAssetsAsync<AudioClip>(hitReference, (i) =>
        {
            hitSFX.Add(i);

        }).WaitForCompletion();
    }

    public void PlayHitSFX(AudioSource audioSource)
    {
        audioSourceManager.PlaySFX(audioSource, hitSFX);
    }
}

