using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class MagicUp : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Teleport_green";

        private void OnDestroy()
        {

        }
    }
}