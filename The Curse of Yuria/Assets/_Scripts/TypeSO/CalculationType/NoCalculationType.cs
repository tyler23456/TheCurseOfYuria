using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoCalculation", menuName = "CalculationType/NoCalculation")]
public class NoCalculationType : CalculationTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator;
    }
}
