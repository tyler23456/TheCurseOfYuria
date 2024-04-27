using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Stun : SkillBase, ISkill
    {
        protected override string particleSystemName => "explosion_1";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.Damage;
            element = ISkill.Element.Stun;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}