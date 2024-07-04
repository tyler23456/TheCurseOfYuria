using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CalculationTypeBase : ScriptableObject, ICalculationType
{
    public virtual float Calculate(IActor user, IActor target, float accumulator)
    {
        return 0f;
    }
}
