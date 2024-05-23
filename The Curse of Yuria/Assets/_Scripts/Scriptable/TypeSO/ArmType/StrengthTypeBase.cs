using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthTypeBase : ArmTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator * (IStats.Sensitivity / (IStats.Sensitivity + user.getStats.GetAttribute(IStats.Attribute.Strength)))
                           * (IStats.Sensitivity / (IStats.Sensitivity - target.getStats.GetAttribute(IStats.Attribute.Defense)));
    }
}
