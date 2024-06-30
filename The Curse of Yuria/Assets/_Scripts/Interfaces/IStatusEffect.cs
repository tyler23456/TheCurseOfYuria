using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffect
{
    string name { get; }
    void Activate(IActor actor, float accumulator = 0f);
    bool OnAttack(IActor user, IActor target, IItem item);
    bool OnHit(IActor user, IActor target, IItem item);
    void OnAdd(IActor actor);
    void OnRemove(IActor actor);
}
