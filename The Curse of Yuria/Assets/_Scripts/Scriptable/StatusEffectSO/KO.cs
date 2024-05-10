using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKO", menuName = "StatusEffects/KO")]
public class KO : StatusEffectBase, IKockOut
{

    public override void Activate(IActor target, float duration)
    {
        StatusEffect statusEffect = target.getGameObject.AddComponent<StatusEffect>();
        statusEffect.effectDuration = duration;

        statusEffect.OnStart = () => target.getATBGuage.isActive = false;
        statusEffect.OnStart += () => target.getAnimator.KO();

        statusEffect.OnStop = () => target.getATBGuage.isActive = true;
        statusEffect.OnStop += () => target.getAnimator.Stand();
    }
}
