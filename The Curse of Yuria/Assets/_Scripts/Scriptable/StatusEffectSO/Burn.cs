using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBurn", menuName = "StatusEffects/Burn")]
public class Burn : StatusEffectBase, IStatusAilment
{
    [SerializeField] int power = 2;
    [SerializeField] float tickDuration = 5f;

    public override void Activate(IActor target, float duration)
    {
        StatusEffect statusEffect = target.getGameObject.AddComponent<StatusEffect>();
        statusEffect.effectDuration = duration;
        statusEffect.tickDuration = tickDuration;
        statusEffect.OnTickUpdate = () => target.getStats.ApplyCalculation(power, IItem.Element.Fire);
    }
}
