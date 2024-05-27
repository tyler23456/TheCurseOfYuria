using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemTypeBase : TypeBase
{
    public virtual EquipmentPart part => EquipmentPart.Armor;
}
