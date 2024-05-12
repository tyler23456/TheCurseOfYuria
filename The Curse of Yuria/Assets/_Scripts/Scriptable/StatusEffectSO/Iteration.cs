using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAutoAction", menuName = "StatusEffects/AutoAction")]
public class Iteration : StatusEffectBase, IStatusEffect
{
    enum ActivationType { OnKnockOut, TickDuration  }

    [SerializeField] Scroll action;
    [SerializeField] int power = 2;
    [SerializeField] float tickDuration = 5f;

    public override void Activate(IActor target, float accumulator = 0f)
    {
        base.Activate(target, accumulator);
        target.StartCoroutine(enumerator(target));
    }

    public IEnumerator enumerator(IActor target)
    {
        while (target.getStatusEffects.Contains(name))
        {
            action.Use(new IActor[] { target });
            yield return new WaitForSeconds(tickDuration);
        }   
    }
}
