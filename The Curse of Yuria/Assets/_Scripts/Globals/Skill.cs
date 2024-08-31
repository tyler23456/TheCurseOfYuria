using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public abstract class Skill : ItemSO, IItem
{
    public abstract ElementType elementType { get; }

    public abstract bool TrueForAnyStatusEffect(Func<StatusEffect, bool> predicate);
    public abstract bool ContainsStatusEffectThatCanRemoveKO();
    public abstract bool ContainsType(string typeName);
}
