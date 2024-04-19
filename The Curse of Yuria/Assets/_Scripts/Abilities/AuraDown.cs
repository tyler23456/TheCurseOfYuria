using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class AuraDown : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Teleport_purple_reverse";

        private void OnDestroy()
        {

        }
    }
}