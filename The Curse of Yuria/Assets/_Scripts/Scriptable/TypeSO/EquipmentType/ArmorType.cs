using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "ItemType/Armor")]
public class ArmorType : ItemTypeBase, IType
{
    public override EquipmentPart part => EquipmentPart.Armor;
}
