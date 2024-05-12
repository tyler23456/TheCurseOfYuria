using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewImmobility", menuName = "StatusEffects/Immobility")]
public class Deactivation : StatusEffectBase, IStatusEffect
{
    [SerializeField] bool disableMovement = false;
    [SerializeField] bool disableATBGauge = false;
    [SerializeField] float minimumDuration = 0f;
    [SerializeField] float maximumDuration = 0f;

    //we need to figure out a way to ensure deactivation effect duration is loaded properly after saving and loading.
    public override void Activate(IActor target, float accumulator = 0f)
    {
        base.Activate(target, Random.Range(minimumDuration, maximumDuration));
    }

    public override void OnAdd(IActor target)
    {
        base.OnAdd(target);

        if (disableMovement)
            target.getPosition.Deactivate();

        if (disableATBGauge)
            target.getATBGuage.Deactivate();
    }


    public override void OnRemove(IActor target)
    {
        base.OnRemove(target);

        if (disableMovement)
            target.getPosition.Deactivate();

        if (disableATBGauge)
            target.getATBGuage.Deactivate();
    }
}
