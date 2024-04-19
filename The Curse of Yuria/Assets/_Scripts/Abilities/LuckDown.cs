using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class LuckDown : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Teleport_yellow_reverse";

        private void OnDestroy()
        {

        }
    }
}