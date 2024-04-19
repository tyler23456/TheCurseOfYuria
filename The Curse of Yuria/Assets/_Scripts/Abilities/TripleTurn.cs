using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class TripleTurn : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Default";

        private void Reset()
        {
            power = 0;
            duration = float.PositiveInfinity;
            cost = 5;
            group = IAbility.Group.None;
            type = IAbility.Type.None;
            element = IAbility.Element.None;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}