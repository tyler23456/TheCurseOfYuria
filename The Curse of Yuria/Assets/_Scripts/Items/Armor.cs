using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

namespace TCOY.Items
{
    public class Armor : EquipableBase
    {
        public override string type => "Armor";
        public override EquipmentPart part => EquipmentPart.Armor;
    }
}
