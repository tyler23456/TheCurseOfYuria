using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Nullify : SkillBase, ISkill
    {
        protected override string particleSystemName => "Ground_Hit_2_blue";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.Damage;
            element = ISkill.Element.Light;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}
