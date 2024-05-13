using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRedirection", menuName = "StatusEffects/Redirection")]
public class Redirection : StatusEffectBase, IStatusEffect
{
    enum Type { Deflection, Reflection }

    [SerializeField] Type type;

    public override void Activate(IActor target, float duration)
    {
        base.Activate(target, duration);
    }
}
