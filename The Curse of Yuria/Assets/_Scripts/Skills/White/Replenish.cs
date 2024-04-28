using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Replenish : SkillBase, ISkill
    {
        protected override string particleSystemName => "health_up";


        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.Recovery;
            element = ISkill.Element.None;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}