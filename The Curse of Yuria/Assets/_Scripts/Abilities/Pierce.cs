using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Pierce : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Slash_Angled_04_Unique";

        private void OnDestroy()
        {

        }
    }
}