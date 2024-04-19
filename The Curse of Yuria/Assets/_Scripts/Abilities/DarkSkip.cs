using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class DarkSkip : AbilityBase, IAbility
    {
        protected override string particleSystemName => "ready_attack_black";

        private void OnDestroy()
        {

        }
    }
}