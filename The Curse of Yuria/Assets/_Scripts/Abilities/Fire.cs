using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Fire : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Flame_Center_1";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = IAbility.Group.Magic;
            type = IAbility.Type.Damage;
            element = IAbility.Element.Fire;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}