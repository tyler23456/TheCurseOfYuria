using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class ThunderSkip : AbilityBase, IAbility
    {
        protected override string particleSystemName => "ready_attack_yellow";

        private void OnDestroy()
        {

        }
    }
}