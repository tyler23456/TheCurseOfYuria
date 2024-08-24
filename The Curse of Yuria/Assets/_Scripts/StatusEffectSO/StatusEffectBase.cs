using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class StatusEffectBase : StatusEffect
{
    [SerializeField] protected float duration = float.PositiveInfinity;

    protected float getDuration => duration;

    public override void Activate(IActor target, float accumulator = 0f)
    {
        target.getStatusEffects.Add(name, accumulator);
        OnAdd(target);
        target.StartCoroutine(UpdateEffect(target));
    }

    IEnumerator UpdateEffect(IActor target)
    {
        while (target.getStatusEffects.Elapse(name, duration))
            yield return null;

        OnRemove(target);    
    }

    public override Effect OnAttack(IActor user, IActor target, IItem item)
    {
        return new Effect(user, target, item);
    }

    public override Effect OnHit(IActor user, IActor target, IItem item)
    {
        return new Effect(user, target, item);
    }

    public override void OnAdd(IActor target)
    {
          
    }

    public override void OnRemove(IActor target)
    {
        target.getStatusEffects.Remove(name);
    }
}
