using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ItemTypes
{
    public abstract class ArmTypeBase : ArmType
    {
        public override float Calculate(IActor user, IActor target, float accumulator)
        {
            return accumulator;
        }
    }
}
