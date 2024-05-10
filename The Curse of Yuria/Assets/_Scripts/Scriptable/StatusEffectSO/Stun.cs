using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStun", menuName = "StatusEffects/Stun")]
public class Stun : StatusEffectBase, IStatusAilment
{
    public override void Activate(IActor target, float duration)
    {
        StatusEffect statusEffect = target.getGameObject.AddComponent<StatusEffect>();
        statusEffect.effectDuration = duration;
        statusEffect.OnStart = () => target.getATBGuage.isActive = false;
        statusEffect.OnStop = () => target.getATBGuage.isActive = true;
    }
}
