using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "ArmType/Melee")]
public class MeleeType : StrengthTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return base.Calculate(user, target, accumulator);
    }
}
