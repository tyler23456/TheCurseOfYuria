using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDeactivation", menuName = "StatusEffects/Deactivation")]
public class Deactivation : StatusEffectBase, IStatusEffect
{
    [SerializeField] bool disableMovement = false;
    [SerializeField] bool disableATBGauge = false;
    [SerializeField] float minimumDuration = 0f;
    [SerializeField] float maximumDuration = 0f;

    public override void Activate(IActor target, float accumulator = 0f)
    {
        base.Activate(target, Random.Range(minimumDuration, maximumDuration));
    }

    public override void OnAdd(IActor target)
    {
        base.OnAdd(target);

        Animator animator = target.obj.GetComponent<Animator>();

        if (disableMovement)
            animator?.SetInteger("MovePriority", animator.GetInteger("MovePriority") - 1);

        if (disableATBGauge)
            target.getATBGuage.LowerPriority();
    }

    public override void OnRemove(IActor target)
    {
        base.OnRemove(target);

        Animator animator = target.obj.GetComponent<Animator>();

        if (disableMovement)
            animator?.SetInteger("MovePriority", animator.GetInteger("MovePriority") + 1);
            

        if (disableATBGauge)
            target.getATBGuage.LowerPriority();
    }
}
