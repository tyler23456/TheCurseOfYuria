using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRedirection", menuName = "StatusEffects/Redirection")]
public class Redirection : StatusEffectBase, IStatusEffect
{
    [SerializeField] ParticleSystem particleSystem;

    enum Type { Deflection, Reflection }

    [SerializeField] Type type;

    public override void Activate(IActor target, float duration)
    {
        base.Activate(target, duration);
    }

    public override Effect OnHit(IActor user, IActor target, IItem item)
    {
        bool isSuccessful = false;
        if (type == Type.Deflection && item is IMelee || type == Type.Reflection && item is IScroll)
        {
            Destroy(Instantiate(particleSystem.gameObject, target.obj.transform), 10f);
            target = user;
            isSuccessful = true;
        }

        return new Effect(user, target, item, false, isSuccessful);
    }
}
