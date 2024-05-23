using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;

public interface IItem
{
    ulong getGuid { get; }
    string name { get; }
    Sprite icon { get; }
    GameObject prefab { get; }
    ItemTypeBase itemType { get; }
    string getInfo { get; }

    ArmTypeBase armType { get; }
    CalculationTypeBase calculationType { get; }
    ElementTypeBase elementType { get; }
    int getPower { get; }
    int getCost { get; }

    ItemSprite itemSprite { get; }
    public List<Modifier> getModifiers { get; }
    public List<Reactor> getCounters { get; }
    public List<Reactor> getInterrupts { get; }

    IEnumerator Use(IActor user, List<IActor> targets);
    IEnumerator Use(IActor target);
    void Equip(IActor target);
    void Unequip(IActor target);
}
