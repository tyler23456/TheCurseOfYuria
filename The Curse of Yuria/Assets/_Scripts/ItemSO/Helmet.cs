using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;

namespace TCOY.Items
{
    public class Helmet : EquipableBase
    {
        public override string type => "Helmet";
        public override EquipmentPart part => EquipmentPart.Helmet;
    }
}