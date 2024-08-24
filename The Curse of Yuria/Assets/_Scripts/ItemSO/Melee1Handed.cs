using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

namespace TCOY.Items
{
    public class Melee1Handed : WeaponBase, IMelee
    {
        public override string type => "Melee1Handed";
        public override EquipmentPart part => EquipmentPart.MeleeWeapon1H;
    }
}
