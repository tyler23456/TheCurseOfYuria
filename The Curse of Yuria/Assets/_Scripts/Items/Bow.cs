using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

namespace TCOY.Items
{
    public class Bow : WeaponBase
    {
        public override string type => "Bow";
        public override EquipmentPart part => EquipmentPart.Bow;
    }
}
