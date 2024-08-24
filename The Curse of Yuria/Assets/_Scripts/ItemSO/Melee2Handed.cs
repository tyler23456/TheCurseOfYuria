using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

namespace TCOY.Items
{
    public class Melee2Handed : WeaponBase, IMelee
    {
        public override string type => "Melee2Handed";
        public override EquipmentPart part => EquipmentPart.MeleeWeapon2H;
    }
}
