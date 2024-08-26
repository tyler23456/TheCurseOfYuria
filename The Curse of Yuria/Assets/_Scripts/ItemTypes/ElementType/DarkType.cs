using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ItemTypes
{
    [CreateAssetMenu(fileName = "Dark", menuName = "ElementType/Dark")]
    public class DarkType : ElementBase
    {
        public override int weaknessIndex => 4;
    }
}