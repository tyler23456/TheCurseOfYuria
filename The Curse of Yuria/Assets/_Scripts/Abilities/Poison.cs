using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Poison : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Ground_Splash_green";

        private void OnDestroy()
        {

        }
    }
}