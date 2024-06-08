using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MPDamage", menuName = "CalculationType/MPDamage")]
public class MPDamageType : CalculationTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        target.getStats.ApplyMPDamage(accumulator);
        return accumulator;
    }
}
