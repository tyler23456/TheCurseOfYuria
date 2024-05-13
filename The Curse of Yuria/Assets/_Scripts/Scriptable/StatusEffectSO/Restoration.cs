using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRestoration", menuName = "StatusEffects/Restortation")]
public class Restoration : StatusEffectBase, IStatusEffect
{
    [SerializeField] List<StatusEffectBase> StatusEffectsToRemove;

    public override void Activate(IActor target, float duration)
    {
        foreach (StatusEffectBase statusEffect in StatusEffectsToRemove)
            target.getStatusEffects.Remove(statusEffect.name);
    }
}
