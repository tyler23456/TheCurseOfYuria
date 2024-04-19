using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Nullify : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Ground_Hit_2_blue";

        private void OnDestroy()
        {

        }
    }
}
