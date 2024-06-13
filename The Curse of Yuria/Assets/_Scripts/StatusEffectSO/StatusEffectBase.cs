using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class StatusEffectBase : ScriptableObject, IStatusEffect
{
    [SerializeField] protected float duration = float.PositiveInfinity;

    public float getDuration => duration;

    public virtual void Activate(IActor target, float accumulator = 0f)
    {
        target.getStatusEffects.Add(name, accumulator);
    }

    public virtual void ActivateCounter(IActor user, IActor target, IItem item)
    {
        
    }

    public virtual void OnAdd(IActor target)
    {
          
    }

    public virtual void OnRemove(IActor target)
    {
        
    }
}
