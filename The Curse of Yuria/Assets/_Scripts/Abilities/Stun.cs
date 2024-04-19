using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Stun : AbilityBase, IAbility
    {
        protected override string particleSystemName => "explosion_1";

        private void OnDestroy()
        {

        }
    }
}