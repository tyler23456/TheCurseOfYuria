using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementType : TypeSO
{
    public abstract int weaknessIndex { get; }
    public abstract float Calculate(IActor user, IActor target, float accumulator);
}
