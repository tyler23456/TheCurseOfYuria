using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Ice : SkillBase, ISkill
    {
        protected override string particleSystemName => "Rocket_blue";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.Damage;
            element = ISkill.Element.Ice;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}