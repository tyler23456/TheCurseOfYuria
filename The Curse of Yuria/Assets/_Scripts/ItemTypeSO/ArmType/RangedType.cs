using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranged", menuName = "ArmType/Ranged")]
public class RangedType : StrengthTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return base.Calculate(user, target, accumulator);
    }
}
