using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Abilities
{
    public class Deflect : AbilityBase, IAbility
    {
        protected override string particleSystemName => "Magic_Sphere_blue";

        //Hit_1_blue

        private void OnDestroy()
        {

        }
    }
}