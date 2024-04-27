using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Skills
{
    public class Enthrall : SkillBase, ISkill
    {
        protected override string particleSystemName => "Flame_green";




        private void Reset()
        {
            power = 5;
            duration = float.PositiveInfinity;
            cost = 5;
            group = ISkill.Group.Magic;
            type = ISkill.Type.None;
            element = ISkill.Element.Enthrall;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();


        }
    }
}