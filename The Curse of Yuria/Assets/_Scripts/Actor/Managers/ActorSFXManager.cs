using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ActorSFXManager : MonoBehaviour
{
    public static ActorSFXManager Instance { get; private set; }

    [SerializeField] AssetLabelReference swingReference;
    [SerializeField] AssetLabelReference releaseBowReference;
    [SerializeField] AssetLabelReference hitReference;

    [SerializeField] AudioSourceManager audioSourceManager;

    List<AudioClip> swingSFX = new List<AudioClip>();
    List<AudioClip> releaseBowSFX = new List<AudioClip>();
    List<AudioClip> hitSFX = new List<AudioClip>();

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
        Addressables.LoadAssetsAsync<AudioClip>(hitReference, (i) =>
        {
            hitSFX.Add(i);

        }).WaitForCompletion();
    }

    public void PlaySwingSFX()
    {
        audioSourceManager.PlaySFX(swingSFX);
    }

    public void PlayReleaseBowSFX()
    {
        audioSourceManager.PlaySFX(releaseBowSFX);
    }

    public void PlayHitSFX()
    {
        audioSourceManager.PlaySFX(hitSFX);
    }
}
