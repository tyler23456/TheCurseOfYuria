using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Nullify : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Ground_Hit_2_blue";

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
