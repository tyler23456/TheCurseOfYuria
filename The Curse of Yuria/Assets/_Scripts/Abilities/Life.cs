using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Life : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Health_Up_green";

        private void OnDestroy()
        {

        }
    }
}