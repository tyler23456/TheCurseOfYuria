using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;

public abstract class ItemSO : ScriptableObject
{
    public abstract ulong getGuid { get; }
    public new string name { get; }
    public abstract Sprite icon { get; }
    public abstract GameObject prefab { get; }
    public abstract ItemTypeBase itemType { get; }
    public abstract string getInfo { get; }
    public abstract ItemSprite itemSprite { get; }
    public abstract int getValue { get; }

    public abstract IEnumerator Use(IActor user, List<IActor> targets);
    public abstract IEnumerator Use(IActor user, IActor target);
    public abstract void Equip(IActor user);
    public abstract void Unequip(IActor user);
}
