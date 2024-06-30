using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffectSO : ScriptableObject
{
    public new string name { get; }
    public abstract void Activate(IActor actor, float accumulator = 0);
    public abstract bool OnAttack(IActor user, IActor target, IItem item);
    public abstract bool OnHit(IActor user, IActor target, IItem item);
    public abstract void OnAdd(IActor actor);
    public abstract void OnRemove(IActor actor);
}
