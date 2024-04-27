using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Freeze : SkillBase, ISkill
    {
        protected override string particleSystemName => "Default";

        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.Damage;
            element = ISkill.Element.Freeze;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}