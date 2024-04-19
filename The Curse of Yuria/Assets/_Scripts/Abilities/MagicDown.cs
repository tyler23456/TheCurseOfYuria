using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class MagicDown : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Teleport_green_reverse";

        private void OnDestroy()
        {

        }
    }
}