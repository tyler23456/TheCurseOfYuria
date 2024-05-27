using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cape", menuName = "ItemType/Cape")]
public class CapeType : ItemTypeBase, IType
{
    public override EquipmentPart part => EquipmentPart.Cape;
}
