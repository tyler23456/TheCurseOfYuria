using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MPRecovery", menuName = "CalculationType/MPRecovery")]
public class MPRecoveryType : CalculationTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        target.getStats.ApplyMPRecovery(accumulator);
        return accumulator;
    }
}
