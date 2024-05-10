using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuffDebuff", menuName = "StatusEffects/BuffDebuff")]
public class BuffDebuff : StatusEffectBase, IStatusEffect
{
    [SerializeField] IStats.Attribute attribute;
    [SerializeField] int value;

    public override void Activate(IActor target, float duration)
    {
        StatusEffect statusEffect = target.getGameObject.AddComponent<StatusEffect>();
        statusEffect.effectDuration = duration;
        statusEffect.OnStart = () => target.getStats.OffsetAttribute(attribute, value);
        statusEffect.OnStop = () => target.getStats.OffsetAttribute(attribute, -value);
    }
}
