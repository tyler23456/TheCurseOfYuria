using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Bleed : AbilityBase, IAbility
    {
        protected override string particleSystemName => "blood_01";

        private void OnDestroy()
        {

        }
    }
}