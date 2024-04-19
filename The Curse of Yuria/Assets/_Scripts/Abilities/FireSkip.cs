using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class FireSkip : AbilityBase, IAbility
    {
        protected override string particleSystemName => "ready_attack";

        private void OnDestroy()
        {

        }
    }
}