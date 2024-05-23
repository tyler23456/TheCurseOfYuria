using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Earring", menuName = "ItemType/Earring")]
public class EarringType : ItemTypeBase, IType
{
    public override EquipmentPart part { get; protected set; } = EquipmentPart.Earrings;
}
