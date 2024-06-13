using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKnockOut", menuName = "StatusEffects/KnockOut")]
public class KnockOut : StatusEffectBase, IStatusEffect
{
    public override void OnAdd(IActor target)
    {
        base.OnAdd(target);

        target.getStatusEffects.RemoveWhere(e => e != name);    
        Animator animator = target.obj.GetComponent<Animator>();
        animator?.SetInteger("MovePriority", animator.GetInteger("MovePriority") - 1);
        target.getATBGuage.LowerPriority();
        animator?.SetInteger("State", 6);
    }

    public override void OnRemove(IActor target)
    {
        base.OnRemove(target);

        Animator animator = target.obj.GetComponent<Animator>();
        animator?.SetInteger("MovePriority", animator.GetInteger("MovePriority") + 1);
        target.getATBGuage.LowerPriority();
        animator?.SetInteger("State", 0);
    }
}
