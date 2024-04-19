using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Dark : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Lazer_purple";

        private void OnDestroy()
        {

        }
    }
}