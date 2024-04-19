using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Burn : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Flame_burn";

        private void OnDestroy()
        {

        }
    }
}