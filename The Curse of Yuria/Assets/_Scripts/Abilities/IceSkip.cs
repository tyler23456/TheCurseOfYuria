using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class IceSkip : AbilityBase, IAbility
    {
        protected override string particleSystemName => "ready_attack_blue";

        private void OnDestroy()
        {

        }
    }
}