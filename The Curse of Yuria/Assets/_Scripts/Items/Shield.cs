using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

namespace TCOY.Items
{
    public class Shield : EquipableBase
    {
        public override string type => "Shield";
        public override EquipmentPart part => EquipmentPart.Shield;
    }
}