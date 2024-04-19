using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Enthrall : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Flame_green";




        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = IAbility.Group.Magic;
            type = IAbility.Type.None;
            element = IAbility.Element.Enthrall;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}