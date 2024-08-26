using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ItemTypes
{
    [CreateAssetMenu(fileName = "Light", menuName = "ElementType/Light")]
    public class LightType : ElementBase
    {
        public override int weaknessIndex => 3;
    }
}