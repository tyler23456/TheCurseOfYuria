using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Thunder : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Electricity_Splash_Center";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = IAbility.Group.Magic;
            type = IAbility.Type.Damage;
            element = IAbility.Element.Thunder;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}