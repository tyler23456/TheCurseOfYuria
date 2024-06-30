using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemTypeBase : ItemTypeSO
{
    public override EquipmentPart part => EquipmentPart.Armor;
}
