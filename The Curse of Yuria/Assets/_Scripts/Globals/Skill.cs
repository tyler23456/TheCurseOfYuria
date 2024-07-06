using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public abstract class Skill : ScriptableObject
{
    public abstract string type { get; }
    public abstract string description { get; }
    public abstract int marketValue { get; }

    public abstract ElementType elementType { get; }

    public abstract IEnumerator Use(IActor user, params IActor[] targets);
    public abstract IEnumerator Use(IActor target);
    public abstract void Equip(IActor target);
    public abstract void Unequip(IActor target);

    public abstract bool TrueForAnyStatusEffect(Func<StatusEffect, bool> predicate);
}
