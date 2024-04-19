using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Reflect : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Magic_Sphere_purple";

        //Hit_3_purple

        private void Reset()
        {
            power = 5;
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