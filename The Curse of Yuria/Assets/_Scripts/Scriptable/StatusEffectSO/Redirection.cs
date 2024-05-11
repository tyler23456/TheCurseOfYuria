using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirection : StatusEffectBase
{
    enum Type { Deflection, Reflection }

    [SerializeField] Type type;

    public override void Activate(IActor target, float duration)
    {
        switch (type)
        {
            case Type.Deflection:
                target.getGameObject.AddComponent<Deflection>();
                break;
            case Type.Reflection:
                target.getGameObject.AddComponent<Reflection>();
                break;
        }
    }
    protected class Deflection : EffectBase, IDeflection, IStatusEffect { }
    protected class Reflection : EffectBase, IReflection,  IStatusEffect { }
}
