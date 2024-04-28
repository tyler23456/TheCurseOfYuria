using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class AutoReplenish : SkillBase, ISkill
    {
        protected override string particleSystemName => "Health_Up_white2";

        private void Reset()
        {
            power = 5;
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