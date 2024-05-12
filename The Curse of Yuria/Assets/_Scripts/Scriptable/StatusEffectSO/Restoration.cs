using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restoration : StatusEffectBase, IStatusEffect
{
    [SerializeField] List<StatusEffectBase> StatusEffectsToRemove;

    public override void Activate(IActor target, float duration)
    {
        foreach (StatusEffectBase statusEffect in StatusEffectsToRemove)
            target.getStatusEffects.Remove(statusEffect.name);
    }
}
