using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Deflect : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Magic_Sphere_blue";

        //Hit_1_blue

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