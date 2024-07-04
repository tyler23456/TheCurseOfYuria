using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBonusType
{
    float Calculate(IActor user, IActor target, float accumulator);
}
