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

            attribute = IStats.Attributes.Magic;
            group = IAbility.Group.Magic;
            type = IAbility.Type.Light;
        }

        private void OnDestroy()
        {
            getTarget.getStats.ApplyDamage(getUser.getStats.GetAttribute(attribute) + power, group, type);
        }
    }
}