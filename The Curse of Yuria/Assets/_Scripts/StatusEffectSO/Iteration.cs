using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIteration", menuName = "StatusEffects/Iteration")]
public class Iteration : StatusEffectIcon, IStatusEffect
{
    enum ActivationType { OnKnockOut, TickDuration  }

    [SerializeField] Scroll action;
    [SerializeField] int power = 2;
    [SerializeField] float tickDuration = 5f;

    public override void OnAdd(IActor target)
    {
        base.OnAdd(target);
        target.StartCoroutine(enumerator(target));
    }

    public IEnumerator enumerator(IActor target)
    {
        while (target.getStatusEffects.Contains(name))
        {
            target.StartCoroutine(action.Use(target));
            yield return new WaitForSeconds(tickDuration);
        }   
    }
}
