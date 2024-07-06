using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Data;
using HeroEditor.Common.Enums;

public abstract class ItemBase : ScriptableObject
{
    [HideInInspector][SerializeField] Sprite _icon;
    [HideInInspector][SerializeField] GameObject _prefab;

    [Space(5)]
    [SerializeField] [TextArea(3, 10)] string description;
    [HideInInspector][SerializeField] ItemSprite _itemSprite;

    [Space(5)]
    [SerializeField] int marketValue = 4;

    public virtual string type => "";

    public Sprite icon { get { return _icon; } set { _icon = value; } }
    public GameObject prefab { get { return _prefab; } set { _prefab = value; } }
    public ItemSprite itemSprite { get { return _itemSprite; } set { _itemSprite = value; } }

    public string getDescription => description;
    public int getWorth => marketValue;

    public virtual IEnumerator Use(IActor user, IActor[] targets) { yield return null; }
    public virtual IEnumerator Use(IActor target) { yield return null; }
    public virtual void Equip(IActor user) { }
    public virtual void Unequip(IActor user) { }
}
