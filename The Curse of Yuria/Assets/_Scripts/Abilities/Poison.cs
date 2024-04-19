using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Poison : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Ground_Splash_green";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = IAbility.Group.Magic;
            type = IAbility.Type.Damage;
            element = IAbility.Element.Poison;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}