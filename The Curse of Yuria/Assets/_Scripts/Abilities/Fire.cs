using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Fire : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Flame_Center_1";

        private void OnDestroy()
        {

        }
    }
}