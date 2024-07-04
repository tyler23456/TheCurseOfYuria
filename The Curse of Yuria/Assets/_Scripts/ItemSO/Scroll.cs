using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Items
{
    public class Scroll : SkillBase, IItem, IScroll
    {
        [SerializeField] protected int cost;

        public override string type => "Scroll";
        public int getCost => cost;

        public override IEnumerator Use(IActor user, params IActor[] targets)
        {
            LightManager.instance.FadeOut();

            skillInfo.SetDirection(user, targets);

            user.getStats.ApplyCost(cost);
            user.obj.GetComponent<Animator>()?.SetTrigger("Cast");

            foreach (IActor target in targets)
                target.StartCoroutine(skillInfo.PerformAnimation(user, target, this, statusEffectsInfo));

            LightManager.instance.FadeIn();

            yield return null;
        }
     
        public override void Equip(IActor target)
        {
            base.Equip(target);

            if (target.getScrolls.Contains(name))
                return;

            target.getScrolls.Add(name);
        }

        public override void Unequip(IActor target)
        {
            base.Unequip(target);
            target.getScrolls.Remove(name);
        }
    }
}
