using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFreeze", menuName = "StatusEffects/Freeze")]
public class Freeze : StatusEffectBase, IStatusAilment
{
    [SerializeField] int power = 2;
    [SerializeField] float tickDuration = 5f;

    public override void Activate(IActor target, float duration)
    {
        StatusEffect statusEffect = target.getGameObject.AddComponent<StatusEffect>();
        statusEffect.effectDuration = duration;
        statusEffect.tickDuration = tickDuration;
        statusEffect.OnTickUpdate = () => target.getStats.ApplyCalculation(power, IItem.Element.Ice);
    }
}