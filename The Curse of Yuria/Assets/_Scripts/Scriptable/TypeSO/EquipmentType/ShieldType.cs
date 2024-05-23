using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "ItemType/Shield")]
public class ShieldType : ItemTypeBase, IType
{
    public override EquipmentPart part { get; protected set; } = EquipmentPart.Shield;
}
