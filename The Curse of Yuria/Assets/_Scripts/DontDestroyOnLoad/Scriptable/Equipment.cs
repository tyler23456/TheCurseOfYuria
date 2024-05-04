using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeroEditor.Common.Enums;
using HeroEditor.Common.Data;

namespace TCOY.DontDestroyOnLoad
{
    public class Equipment : ItemBase, IItem
    {
        public override void Use(IActor user, IActor[] targets)
        {
            if (targets == null || targets.Length == 0)
                return;

            IGlobal global = GameObject.Find("/DontDestroyOnLoad").GetComponent<IGlobal>();
            global.StartCoroutine(performAnimation(user, targets));
        }

        protected virtual IEnumerator performAnimation(IActor user, IActor[] targets)
        {
            user.getAnimator.Attack();

            yield return new WaitForSeconds(0.5f);

            foreach (IActor target in targets)
                yield return PerformEffect(user, target);  //may need to start a new coroutine for this? 
        }

        protected virtual IEnumerator PerformEffect(IActor user, IActor target)
        {
            target.getStats.ApplySkillCalculation(power, user.getStats, group, type, element);
            Debug.Log("Calculation Applied");
            yield return null;
        }

        public override void Equip(IActor target)
        {
            base.Equip(target);
        }

        public override void Unequip(IActor target)
        {
            base.Unequip(target);
        }
    }
}