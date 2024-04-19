using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class AutoLife : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Health_Up_white";

        private void OnDestroy()
        {

        }
    }
}