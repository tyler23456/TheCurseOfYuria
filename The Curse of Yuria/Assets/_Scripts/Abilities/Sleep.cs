using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Sleep : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Water_Splash_13_air_2";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = IAbility.Group.Magic;
            type = IAbility.Type.Damage;
            element = IAbility.Element.Sleep ;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}