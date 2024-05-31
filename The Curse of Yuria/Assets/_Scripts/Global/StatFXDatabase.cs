using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[ExecuteAlways]
public class StatFXDatabase : MonoBehaviour
{
    public static StatFXDatabase Instance { get; private set; }

    [SerializeField] AssetLabelReference statusEffectsReference;
    [SerializeField] bool populate = false;
    [SerializeField] List<StatusEffectBase> serializedEffects = new List<StatusEffectBase>();

    Dictionary<string, IStatusEffect> statusEffects = new Dictionary<string, IStatusEffect>();

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!populate)
            return;

        populate = false;

        Addressables.LoadAssetsAsync<StatusEffectBase>(statusEffectsReference, (i) =>
        {
            serializedEffects.Add(i);
        }).WaitForCompletion();
    }

    public IStatusEffect Get(string name)
    {
        if (statusEffects.Count == 0)
        {
            foreach (IStatusEffect effect in serializedEffects)
                statusEffects.Add(effect.name, effect);
            serializedEffects.Clear();
        }

        return statusEffects[name];
    }
}
