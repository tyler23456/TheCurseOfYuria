using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;

public abstract class ItemBase : ScriptableObject
{
    [SerializeField] BasicInfo basicInfo;

    public virtual string type => "";

    public Sprite icon { get { return basicInfo.icon; } set { basicInfo.icon = value; } }
    public GameObject prefab { get { return basicInfo.prefab; } set { basicInfo.prefab = value; } }
    public ItemSprite itemSprite { get { return basicInfo.itemSprite; } set { basicInfo.itemSprite = value; } }

    public string description => basicInfo.description;
    public int marketValue => basicInfo.marketValue;

    public virtual IEnumerator Use(IActor user, IActor[] targets) { yield return null; }
    public virtual IEnumerator Use(IActor target) { yield return null; }
    public virtual void Equip(IActor user) { }
    public virtual void Unequip(IActor user) { }
}
