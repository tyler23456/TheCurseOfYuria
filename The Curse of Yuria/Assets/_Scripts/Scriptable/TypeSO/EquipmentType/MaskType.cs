using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mask", menuName = "ItemType/Mask")]
public class MaskType : ItemTypeBase, IType
{
    public override EquipmentPart part { get; protected set; } = EquipmentPart.Mask;
}
