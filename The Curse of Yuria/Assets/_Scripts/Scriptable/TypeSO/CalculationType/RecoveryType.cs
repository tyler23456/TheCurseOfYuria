using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recovery", menuName = "CalculationType/Recovery")]
public class RecoveryType : CalculationTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        target.getStats.ApplyRecovery(accumulator);
        return accumulator;
    }
}
