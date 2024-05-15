using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKnockOut", menuName = "StatusEffects/KnockOut")]
public class KnockOut : Deactivation, IStatusEffect
{
    public override void OnAdd(IActor target)
    {
        base.OnAdd(target);

        target.getAnimator.KO();

    }


    public override void OnRemove(IActor target)
    {
        base.OnRemove(target);

        target.getAnimator.Stand();
    }
}
