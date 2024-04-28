using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Shock : SkillBase, ISkill
    {
        protected override string particleSystemName => "electricity";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.Damage;
            element = ISkill.Element.Shock;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}