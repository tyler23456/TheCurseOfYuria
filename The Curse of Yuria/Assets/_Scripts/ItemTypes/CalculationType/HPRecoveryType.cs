using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HPRecovery", menuName = "CalculationType/HPRecovery")]
public class HPRecoveryType : CalculationTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        target.getStats.ApplyHPRecovery(accumulator);
        return accumulator;
    }
}
