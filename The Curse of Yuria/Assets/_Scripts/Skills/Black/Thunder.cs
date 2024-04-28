using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Thunder : SkillBase, ISkill
    {
        protected override string particleSystemName => "Electricity_Splash_Center";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.Damage;
            element = ISkill.Element.Thunder;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}