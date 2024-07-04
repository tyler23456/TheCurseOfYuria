using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using HeroEditor.Common.Enums;

public interface IEquipment : IItem
{
    EquipmentPart part { get; }

    ReadOnlyCollection<Modifier> getModifiers { get; }
    ReadOnlyCollection<Ward> getWards { get; }
    ReadOnlyCollection<Reactor> getCounters { get; }
    ReadOnlyCollection<Reactor> getInterrupts { get; }
}
