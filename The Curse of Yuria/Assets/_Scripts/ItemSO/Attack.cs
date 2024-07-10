using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Items
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Attacks/Attack")]
    public class Attack : SkillBase
    {
        public override IEnumerator Use(IActor user, params IActor[] targets)
        {
            skillInfo.SetDirection(user, targets);

            user.obj.GetComponent<Animator>()?.SetTrigger("Slash");

            foreach (IActor target in targets)
                target.StartCoroutine(skillInfo.PerformAnimation(user, target, this, statusEffectsInfo));

            yield return null;
        }
    }
}