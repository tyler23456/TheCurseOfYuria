using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class StrengthUp : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Teleport_normal";

        private void OnDestroy()
        {

        }
    }
}