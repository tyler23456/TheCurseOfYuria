using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class DefenseDown : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Teleport_blue_reverse";

        private void OnDestroy()
        {

        }
    }
}