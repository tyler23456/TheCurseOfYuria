using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ItemTypes
{
    [CreateAssetMenu(fileName = "NoElement", menuName = "ElementType/NoElement")]
    public class NoElementType : ElementBase
    {
        public override int weaknessIndex => -1;

        public override float Calculate(IActor user, IActor target, float accumulator)
        {
            return accumulator;
        }
    }
}