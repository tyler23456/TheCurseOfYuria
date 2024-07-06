using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusTypeBase
{
    public virtual float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator;
    }
}
