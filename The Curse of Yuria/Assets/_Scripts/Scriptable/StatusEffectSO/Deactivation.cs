using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewImmobility", menuName = "StatusEffects/Immobility")]
public class Deactivation : StatusEffectBase, IStatusAilment
{
    [SerializeField] bool disableATBGauge = false;
    [SerializeField] float minimumDuration = 30f;
    [SerializeField] float maximumDuration = 30f;

    public override void Activate(IActor target, float duration)
    {
        StatusEffect statusEffect = target.getGameObject.AddComponent<StatusEffect>();
        statusEffect.effectDuration = Random.Range(minimumDuration, maximumDuration);
        statusEffect.OnStart = () => target.getPosition.isActive = false;
        statusEffect.OnStop = () => target.getPosition.isActive = true;

        if (!disableATBGauge)
            return;

        statusEffect.OnStart += () => target.getATBGuage.isActive = false;
        statusEffect.OnStop += () => target.getATBGuage.isActive = true;

        /*statusEffect.OnStart = () => target.getATBGuage.isActive = false;
        statusEffect.OnStart += () => target.getAnimator.KO();

        statusEffect.OnStop = () => target.getATBGuage.isActive = true;
        statusEffect.OnStop += () => target.getAnimator.Stand();*/
    }

    protected class KnockedOutEffect : EffectBase, IKockOut { }
}
