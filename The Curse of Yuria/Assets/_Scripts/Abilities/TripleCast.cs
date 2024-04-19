using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class TripleCast : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Default";

        private void OnDestroy()
        {

        }
    }
}