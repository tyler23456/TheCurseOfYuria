using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Dark : SkillBase, ISkill
    {
        protected override string particleSystemName => "Lazer_purple";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.Damage;
            element = ISkill.Element.Dark;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}