using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Bleed : SkillBase, ISkill
    {
        protected override string particleSystemName => "blood_01";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.Damage;
            element = ISkill.Element.Bleed;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}