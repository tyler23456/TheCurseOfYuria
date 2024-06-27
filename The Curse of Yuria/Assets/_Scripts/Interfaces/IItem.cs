using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using System.Collections.ObjectModel;

public interface IItem
{
    ulong getGuid { get; }
    string name { get; }
    Sprite icon { get; }
    GameObject prefab { get; }
    ItemTypeBase itemType { get; }
    string getInfo { get; }
    ItemSprite itemSprite { get; }
    int getValue { get; }


    IEnumerator Use(IActor user, List<IActor> targets);
    IEnumerator Use(IActor target);
    void Equip(IActor target);
    void Unequip(IActor target);
}
