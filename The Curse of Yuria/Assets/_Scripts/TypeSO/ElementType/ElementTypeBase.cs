using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementTypeBase : TypeBase
{
    public virtual int weaknessIndex => 0;

    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        int weakness = target.getStats.GetWeakness(weaknessIndex);
        
        return weakness >= 0? accumulator * (IStats.weaknessSensitivity / (IStats.weaknessSensitivity + weakness)) :
                              accumulator * ((-weakness + IStats.weaknessSensitivity) / IStats.weaknessSensitivity);
    }
}
