using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee1H", menuName = "ItemType/Melee1H")]
public class Melee1HType : ItemTypeBase, IType
{
    public override EquipmentPart part => EquipmentPart.MeleeWeapon1H;
}
