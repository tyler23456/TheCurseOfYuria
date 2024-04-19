using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Freeze : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Default";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = IAbility.Group.Magic;
            type = IAbility.Type.Damage;
            element = IAbility.Element.Freeze;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}