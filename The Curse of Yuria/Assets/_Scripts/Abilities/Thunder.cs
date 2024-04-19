using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Thunder : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Electricity_Splash_Center";

        private void OnDestroy()
        {

        }
    }
}