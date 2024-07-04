using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElementType
{
    int weaknessIndex { get; }
    float Calculate(IActor user, IActor target, float accumulator);
}
