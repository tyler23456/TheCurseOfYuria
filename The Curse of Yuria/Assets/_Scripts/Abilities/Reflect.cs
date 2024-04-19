using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Reflect : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Magic_Sphere_purple";

        //Hit_3_purple

        private void OnDestroy()
        {

        }
    }
}