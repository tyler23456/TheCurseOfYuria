using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Items
{
    public class Basic : SkillBase, IItem
    {
        public override string type => "Basic";

        public override IEnumerator Use(IActor user, params IActor[] targets)
        {
            InventoryManager.Instance.basic.Remove(name);

            user.obj.GetComponent<Animator>()?.SetTrigger("UseSupply");

            foreach (IActor target in targets)
                user.StartCoroutine(skillInfo.PerformAnimation(user, target, this, statusEffectsInfo));

            yield return null;
        }
    }
}
