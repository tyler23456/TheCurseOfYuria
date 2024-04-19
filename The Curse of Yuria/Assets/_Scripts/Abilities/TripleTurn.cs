using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class TripleTurn : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Default";

        private void OnDestroy()
        {

        }
    }
}