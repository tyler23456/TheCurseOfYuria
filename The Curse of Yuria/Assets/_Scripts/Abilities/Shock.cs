using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Shock : AbilityBase, IAbility
    {
        protected override string particleSystemName => "electricity";

        private void OnDestroy()
        {

        }
    }
}