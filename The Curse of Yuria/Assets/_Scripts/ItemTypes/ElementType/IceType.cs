using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ItemTypes
{
    [CreateAssetMenu(fileName = "Ice", menuName = "ElementType/Ice")]
    public class IceType : ElementBase
    {
        public override int weaknessIndex => 1;
    }
}