using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ArmTypeBase : IArmType
{
    public virtual float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator;
    }
}
