using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class StatusEffectBase : StatusEffectSO, IStatusEffect
{
    [SerializeField] protected float duration = float.PositiveInfinity;

    protected float getDuration => duration;

    public override void Activate(IActor target, float accumulator = 0f)
    {
        OnAdd(target);
        target.getStatusEffects.Add(name, accumulator);
        target.StartCoroutine(UpdateEffect(target));
    }

    IEnumerator UpdateEffect(IActor target)
    {
        while (target.getStatusEffects.Elapse(name, duration))
            yield return null;

        OnRemove(target);
        target.getStatusEffects.Remove(name);

    }

    public override bool OnAttack(IActor user, IActor target, IItem item)
    {
        bool itemCancellationFlag = false;
        return itemCancellationFlag;
    }

    public override bool OnHit(IActor user, IActor target, IItem item)
    {
        bool itemCancellationFlag = false;
        return itemCancellationFlag;
    }

    public override void OnAdd(IActor target)
    {
          
    }

    public override void OnRemove(IActor target)
    {
        
    }
}
