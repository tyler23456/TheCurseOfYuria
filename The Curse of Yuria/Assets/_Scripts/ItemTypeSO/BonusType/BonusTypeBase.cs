using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTypeBase : TypeBase
{
    public override float Calculate(IActor user, IActor target, float accumulator)
    {
        return accumulator;
    }
}
