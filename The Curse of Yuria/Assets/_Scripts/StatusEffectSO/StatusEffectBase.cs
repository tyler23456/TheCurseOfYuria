using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class StatusEffectBase : ScriptableObject, IStatusEffect
{
    [SerializeField] protected float duration = float.PositiveInfinity;

    protected float getDuration => duration;

    public virtual void Activate(IActor target, float accumulator = 0f)
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

    public virtual bool OnAttack(IActor user, IActor target, IItem item)
    {
        bool itemCancellationFlag = false;
        return itemCancellationFlag;
    }

    public virtual bool OnHit(IActor user, IActor target, IItem item)
    {
        bool itemCancellationFlag = false;
        return itemCancellationFlag;
    }

    public virtual void OnAdd(IActor target)
    {
          
    }

    public virtual void OnRemove(IActor target)
    {
        
    }
}
