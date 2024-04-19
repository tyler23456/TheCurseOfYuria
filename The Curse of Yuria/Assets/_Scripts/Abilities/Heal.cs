using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Heal : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Level_Up_blue";

        private void OnDestroy()
        {

        }
    }
}