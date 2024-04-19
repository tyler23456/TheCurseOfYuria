using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Wings : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Hit_2_normal";

        private void OnDestroy()
        {

        }
    }
}