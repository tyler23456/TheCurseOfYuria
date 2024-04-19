using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class DefenseUp : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Teleport_blue";

        private void OnDestroy()
        {

        }
    }
}