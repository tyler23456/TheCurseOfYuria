using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public abstract void Activate(IActor actor, float accumulator = 0f);
    public abstract Effect OnAttack(IActor user, IActor target,  IItem item);
    public abstract Effect OnHit( IActor user,  IActor target,  IItem item);
    public abstract void OnAdd( IActor actor);
    public abstract void OnRemove( IActor actor);
}
