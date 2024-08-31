using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using System.Collections.ObjectModel;

public interface IItem
{
    string name { get; }
    Sprite icon { get; }
    GameObject prefab { get; }
    ItemSprite itemSprite { get; }
    string type { get; }
    string description { get; }
    int marketValue { get; }


    IEnumerator Use(IActor user, List<IActor> targets);
    IEnumerator Use(IActor target);
    List<string> Equip(IActor target);
    void Unequip(IActor target);
    List<string> GetRequiredRemovalsFor(IActor target);
}
