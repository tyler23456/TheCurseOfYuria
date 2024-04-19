using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Ice : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Rocket_blue";

        private void OnDestroy()
        {

        }
    }
}