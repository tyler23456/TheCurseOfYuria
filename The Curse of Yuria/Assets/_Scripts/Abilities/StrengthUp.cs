using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class StrengthUp : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Teleport_normal";

        private void Reset()
        {
            power = 0;
            duration = float.PositiveInfinity;
            cost = 5;
            group = IAbility.Group.Magic;
            type = IAbility.Type.None;
            element = IAbility.Element.None;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}