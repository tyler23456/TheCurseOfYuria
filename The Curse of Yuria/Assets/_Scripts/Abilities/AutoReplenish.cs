using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class AutoReplenish : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Health_Up_white2";

        private void OnDestroy()
        {

        }
    }
}