using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthTypeBase : ArmTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator * ((user.getStats.GetAttribute(IStats.Attribute.Strength) + IStats.OffenseSensitivity) / (IStats.OffenseSensitivity))
                           * (IStats.DefenseSensitivity / (IStats.DefenseSensitivity + target.getStats.GetAttribute(IStats.Attribute.Defense)));
    }
}
