using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magic", menuName = "ArmType/Magic")]
public class MagicType : ArmTypeBase
{
    [Range(0f, 1f)][SerializeField] float defenseNormal = 1f;

    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator * ((user.getStats.GetAttribute(IStats.Attribute.Magic) + IStats.OffenseSensitivity) / (IStats.OffenseSensitivity)) 
                           * (IStats.DefenseSensitivity / (IStats.DefenseSensitivity + target.getStats.GetAttribute(IStats.Attribute.Aura) * defenseNormal));
    }
}
