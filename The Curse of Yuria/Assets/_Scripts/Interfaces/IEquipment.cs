using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

public interface IEquipment : IItem
{
    ReadOnlyCollection<Modifier> getModifiers { get; }
    ReadOnlyCollection<Reactor> getCounters { get; }
    ReadOnlyCollection<Reactor> getInterrupts { get; }
}
