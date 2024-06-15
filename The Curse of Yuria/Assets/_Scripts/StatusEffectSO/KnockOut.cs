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
        animator?.SetInteger("State", 9);

        IEnabler[] objectsToEnable = target.obj.GetComponents<IEnabler>();
        foreach (IEnabler objectToEnable in objectsToEnable)
            objectToEnable.enabled = true;

        BattleManager.Instance.CancelCommandsFrom(target);

        target.enabled = false;
    }

    public override void OnRemove(IActor target)
    {
        base.OnRemove(target);

        Animator animator = target.obj.GetComponent<Animator>();
        animator?.SetInteger("MovePriority", animator.GetInteger("MovePriority") + 1);
        target.getATBGuage.RaisePriority();

        IEnabler[] objectsToEnable = target.obj.GetComponents<IEnabler>();
        foreach (IEnabler objectToEnable in objectsToEnable)
            objectToEnable.enabled = false;

        animator?.SetInteger("State", 0);
        target.enabled = true;
        
    }
}
