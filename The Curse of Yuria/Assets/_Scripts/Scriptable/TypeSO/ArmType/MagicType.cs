using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magic", menuName = "ArmType/Magic")]
public class MagicType : ArmTypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator * (IStats.Sensitivity / (IStats.Sensitivity + user.getStats.GetAttribute(IStats.Attribute.Magic))) 
                           * (IStats.Sensitivity / (IStats.Sensitivity - target.getStats.GetAttribute(IStats.Attribute.Aura)));
    }
}
