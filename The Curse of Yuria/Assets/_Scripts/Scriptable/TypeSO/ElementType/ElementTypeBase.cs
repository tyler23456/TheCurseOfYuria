using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementTypeBase : TypeBase
{
    public abstract int weaknessIndex { get; protected set; }

    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator * (20f / (20f + user.getStats.GetWeakness(weaknessIndex)));
    }
}
