using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Sleep : SkillBase, ISkill
    {
        protected override string particleSystemName => "Water_Splash_13_air_2";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.Damage;
            element = ISkill.Element.Sleep ;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}