using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CalculationTypeSO : ScriptableObject
{
    public abstract float Calculate(IActor user, IActor target, float accumulator);
}
