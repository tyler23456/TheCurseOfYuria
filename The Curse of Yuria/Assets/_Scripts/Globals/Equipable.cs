using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using HeroEditor.Common.Enums;

public abstract class Equipable : ScriptableObject
{
    public abstract string type { get; }
    public abstract string description { get; }
    public abstract int marketValue { get; }

    public abstract EquipmentPart part { get; }

    public abstract ReadOnlyCollection<Modifier> getModifiers { get; }
    public abstract ReadOnlyCollection<Ward> getWards { get; }
    public abstract ReadOnlyCollection<Reactor> getCounters { get; }
    public abstract ReadOnlyCollection<Reactor> getInterrupts { get; }

    public abstract IEnumerator Use(IActor user, params IActor[] targets);
    public abstract IEnumerator Use(IActor target);
    public abstract void Equip(IActor target);
    public abstract void Unequip(IActor target);
}
