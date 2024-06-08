using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;
using UnityEditor;

public abstract class ItemBase : TypeBase
{
    [SerializeField] [HideInInspector] protected ulong guid;

    [SerializeField] protected Sprite _icon;
    [SerializeField] protected GameObject _prefab;
    [SerializeField] protected ItemTypeBase _equipmentType;
    [SerializeField] [TextArea(3, 10)] protected string info;
    [SerializeField] protected ItemSprite _itemSprite;

    public ulong getGuid => guid;

    public Sprite icon { get { return _icon; } set { _icon = value; } }
    public GameObject prefab { get { return _prefab; } set { _prefab = value; } }
    public ItemTypeBase itemType { get { return _equipmentType; } set { _equipmentType = value; } }
    public ItemSprite itemSprite { get { return _itemSprite; } set { _itemSprite = value; } }
    public string getInfo => info;

    
    public virtual IEnumerator Use(IActor user, List<IActor> targets)
    {
        yield return null;
    }   

    public virtual IEnumerator Use(IActor target)
    {
        yield return null;
    }

    public virtual void Equip(IActor target)
    {     
    }

    public virtual void Unequip(IActor target)
    {   
    }

    protected void SetDirection(IActor user, List<IActor> targets)
    {
        if (targets.Count == 0)
            return;

        Vector2 direction = (targets[0].obj.transform.position - user.obj.transform.position).normalized;

        if (direction.x >= 0)
            user.obj.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        else
            user.obj.transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }
}

