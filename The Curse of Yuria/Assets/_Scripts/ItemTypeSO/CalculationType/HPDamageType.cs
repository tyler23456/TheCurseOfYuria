using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HPDamage", menuName = "CalculationType/HPDamage")]
public class HPDamageType : CalculationTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        target.getStats.ApplyHPDamage(accumulator);
        return accumulator;
    }
}
