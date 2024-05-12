using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggeration : StatusEffectBase, IStatusEffect
{
    [SerializeField] List<StatusEffectBase> triggers;
    [SerializeField] Scroll skill;

    public override void Activate(IActor target, float accumulator = 0)
    {
        base.Activate(target, accumulator);
        target.StartCoroutine(enumerator(target));
    }

    public IEnumerator enumerator(IActor target)
    {
        while (target.getStatusEffects.Contains(name))
        {
            foreach (StatusEffectBase trigger in triggers)
                if (target.getStatusEffects.Contains(trigger.name))
                {
                    skill.Use(new IActor[] { target });
                    target.getStatusEffects.Remove(name);
                }

            yield return new WaitForEndOfFrame();
        }
    }
}
