using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Fire : SkillBase, ISkill
    {
        protected override string particleSystemName => "Flame_Center_1";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.Damage;
            element = ISkill.Element.Fire;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}