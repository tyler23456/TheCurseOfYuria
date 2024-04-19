using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Replenish : AbilityBase, IAbility
    {
        protected override string particleSystemName => "health_up";


        private void OnDestroy()
        {

        }
    }
}