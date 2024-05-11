using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAutoAction", menuName = "StatusEffects/AutoAction")]
public class Iteration : StatusEffectBase, IStatusAilment
{
    enum ActivationType { OnKnockOut, TickDuration  }

    [SerializeField] Scroll action;
    [SerializeField] int power = 2;
    [SerializeField] float tickDuration = 5f;
    

    public override void Activate(IActor target, float duration)
    {
        StatusEffect statusEffect = target.getGameObject.AddComponent<StatusEffect>();
        statusEffect.effectDuration = duration;
        statusEffect.tickDuration = tickDuration;
        statusEffect.OnTickUpdate = () => action.Use(new IActor[] { target });
    }
}
