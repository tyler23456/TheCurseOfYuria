using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magic", menuName = "ArmType/Magic")]
public class MagicType : ArmTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator * ((user.getStats.GetAttribute(IStats.Attribute.Magic) + IStats.OffenseSensitivity) / (IStats.OffenseSensitivity)) 
                           * (IStats.DefenseSensitivity / (IStats.DefenseSensitivity + target.getStats.GetAttribute(IStats.Attribute.Aura)));
    }
}
