using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class LightSkip : AbilityBase, IAbility
    {
        protected override string particleSystemName => "ready_attack_white";

        private void OnDestroy()
        {

        }
    }
}