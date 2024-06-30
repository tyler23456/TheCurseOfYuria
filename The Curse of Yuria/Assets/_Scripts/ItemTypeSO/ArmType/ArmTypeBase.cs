using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ArmTypeBase : ArmTypeSO
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator;
    }
}
