using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

public abstract class ItemBase : SerializedScriptableObject
{
    [SerializeField] Sprite _icon;
    [SerializeField] GameObject _prefab;
    [SerializeField] [TextArea(3, 10)] string info;
    [SerializeField] ItemSprite _itemSprite;
    [SerializeField] int worth = 4;

    public virtual string type => "";

    public Sprite icon { get { return _icon; } set { _icon = value; } }
    public GameObject prefab { get { return _prefab; } set { _prefab = value; } }
    public ItemSprite itemSprite { get { return _itemSprite; } set { _itemSprite = value; } }

    public string getInfo => info;
    public int getWorth => worth;

    public virtual IEnumerator Use(IActor user, IActor[] targets) { yield return null; }
    public virtual IEnumerator Use(IActor target) { yield return null; }
    public virtual void Equip(IActor user) { }
    public virtual void Unequip(IActor user) { }
}
