using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class DarkSkip : AbilityBase, IAbility
    {
        protected override string particleSystemName => "ready_attack_black";

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