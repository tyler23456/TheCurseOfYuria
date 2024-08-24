using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[ExecuteAlways]
public class StatFXDatabase : MonoBehaviour
{
    public static StatFXDatabase Instance { get; private set; }

    [SerializeField] StatusEffectBase knockOut;
    [SerializeField] AssetLabelReference statusEffectsReference;
    [SerializeField] bool populate = false;
    [SerializeField] List<StatusEffect> serializedEffects = new List<StatusEffect>();

    Dictionary<string, StatusEffect> statusEffects = new Dictionary<string, StatusEffect>();

    public StatusEffect getKnockOut => knockOut;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!populate)
            return;

        populate = false;

        Addressables.LoadAssetsAsync<StatusEffect>(statusEffectsReference, (i) =>
        {
            serializedEffects.Add(i);
        }).WaitForCompletion();
    }

    public StatusEffect Get(string name)
    {
        if (statusEffects.Count == 0)
        {
            foreach (StatusEffect effect in serializedEffects)
                statusEffects.Add(effect.name, effect);
            serializedEffects.Clear();
        }

        return statusEffects[name];
    }
}
