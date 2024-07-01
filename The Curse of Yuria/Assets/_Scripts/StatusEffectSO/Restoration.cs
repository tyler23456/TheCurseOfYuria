using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

[CreateAssetMenu(fileName = "NewRestoration", menuName = "StatusEffects/Restortation")]
public class Restoration : StatusEffectBase, IStatusEffect, IRestoration
{
    [SerializeField] List<StatusEffectBase> StatusEffectsToRemove;

    public override void Activate(IActor target, float duration)
    {
        foreach (StatusEffectBase statusEffect in StatusEffectsToRemove)
            target.getStatusEffects.Remove(statusEffect.name);
    }

    public bool ContainsStatusEffectToRemove(string statusEffectToRemoveName)
    {
        
        return StatusEffectsToRemove.Find(i => i.name == statusEffectToRemoveName) != null;
    }
}
