using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Glasses", menuName = "ItemType/Glasses")]
public class GlassesType : ItemTypeBase, IType
{
    public override EquipmentPart part => EquipmentPart.Glasses;
}
