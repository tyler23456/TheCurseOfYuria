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
    string getDescription { get; }
    int getWorth { get; }


    IEnumerator Use(IActor user, params IActor[] targets);
    IEnumerator Use(IActor target);
    void Equip(IActor target);
    void Unequip(IActor target);
}
