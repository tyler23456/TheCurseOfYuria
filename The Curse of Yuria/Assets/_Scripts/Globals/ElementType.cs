using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementType : ScriptableObject
{
    public abstract int weaknessIndex { get; }
    public abstract float Calculate(IActor user, IActor target, float accumulator);
}
