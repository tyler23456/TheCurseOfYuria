using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee2H", menuName = "ItemType/Melee2H")]
public class Melee2HType : ItemTypeBase, IType
{
    public override EquipmentPart part { get; protected set; } = EquipmentPart.MeleeWeapon2H;
}
