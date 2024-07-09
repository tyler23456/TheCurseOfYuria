using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusType : TypeSO
{
    public abstract float Calculate(IActor user, IActor target, float accumulator);
}
