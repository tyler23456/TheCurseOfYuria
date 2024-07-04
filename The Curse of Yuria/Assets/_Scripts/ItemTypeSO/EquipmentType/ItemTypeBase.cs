using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemTypeBase : ScriptableObject, IItemType
{
    public virtual EquipmentPart part => EquipmentPart.Armor;
}
