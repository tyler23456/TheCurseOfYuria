using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Light : SkillBase, ISkill
    {
        protected override string particleSystemName => "Lazer_blue";

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