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

    public override bool OnHit(IActor user, IActor target, IItem item)
    {
        if (type == Type.Deflection && item is IScroll || type == Type.Reflection && item is IWeapon)
        {
            Destroy(Instantiate(particleSystem.gameObject, target.obj.transform), particleSystem.main.duration);
            target = user;
        }

        return false;
    }
}
