using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class AuraUp : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Teleport_purple";

        private void OnDestroy()
        {

        }
    }
}