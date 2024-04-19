using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Sleep : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Water_Splash_13_air_2";

        private void OnDestroy()
        {

        }
    }
}