using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class StrengthDown : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Teleport_normal_reverse";

        private void OnDestroy()
        {

        }
    }
}