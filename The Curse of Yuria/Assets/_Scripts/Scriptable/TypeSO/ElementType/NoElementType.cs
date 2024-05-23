using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoElement", menuName = "ElementType/NoElement")]
public class NoElementType : ElementTypeBase
{
    public override int weaknessIndex { get; protected set; } = -1;

    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator;
    }
}
