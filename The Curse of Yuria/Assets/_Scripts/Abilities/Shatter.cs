using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Shatter : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Shotgun_hit_purple";

        private void OnDestroy()
        {

        }
    }
}