using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class SpeedUp : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Teleport_yellow";

        private void OnDestroy()
        {

        }
    }
}