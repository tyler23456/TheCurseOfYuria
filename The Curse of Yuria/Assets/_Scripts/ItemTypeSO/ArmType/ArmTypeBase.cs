using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ArmTypeBase : ArmType
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator;
    }
}
