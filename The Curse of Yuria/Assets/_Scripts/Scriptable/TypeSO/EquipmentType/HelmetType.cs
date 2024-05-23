using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Helmet", menuName = "ItemType/Helmet")]
public class HelmetType : ItemTypeBase, IType
{
    public override EquipmentPart part { get; protected set; } = EquipmentPart.Helmet;
}
