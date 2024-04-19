using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Paralyze : AbilityBase, IAbility
    {
        protected override string particleSystemName => "plazma";

        private void OnDestroy()
        {

        }
    }
}