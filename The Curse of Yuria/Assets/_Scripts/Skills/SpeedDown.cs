using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class SpeedDown : SkillBase, ISkill
    {
        protected override string particleSystemName => "Teleport_yellow_reverse";

        private void Reset()
        {
            power = 0;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.None;
            element = ISkill.Element.None;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}