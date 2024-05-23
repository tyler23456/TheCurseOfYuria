using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bow", menuName = "ItemType/Bow")]
public class BowType : ItemTypeBase, IType
{
    public override EquipmentPart part => EquipmentPart.Bow;
}
