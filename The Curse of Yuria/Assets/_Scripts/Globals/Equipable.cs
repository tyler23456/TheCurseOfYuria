using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using HeroEditor.Common.Enums;

public abstract class Equipable : ItemSO
{
    public abstract EquipmentPart part { get; }

    public abstract ReadOnlyCollection<Modifier> getModifiers { get; }
    public abstract ReadOnlyCollection<Ward> getWards { get; }
    public abstract ReadOnlyCollection<Reactor> getCounters { get; }
    public abstract ReadOnlyCollection<Reactor> getInterrupts { get; }
}
