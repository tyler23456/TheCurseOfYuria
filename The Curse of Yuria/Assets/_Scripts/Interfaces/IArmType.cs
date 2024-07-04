using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IArmType
{
    float Calculate(IActor user, IActor target, float accumulator);
}
