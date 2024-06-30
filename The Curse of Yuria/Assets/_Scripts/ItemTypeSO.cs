using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

public abstract class ItemTypeSO : ScriptableObject
{
    public virtual EquipmentPart part => EquipmentPart.Armor;
}
