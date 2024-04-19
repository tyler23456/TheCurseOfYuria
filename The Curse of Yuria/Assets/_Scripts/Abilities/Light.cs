using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Light : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Lazer_blue";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = IAbility.Group.Magic;
            type = IAbility.Type.Damage;      
            element = IAbility.Element.Light;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}