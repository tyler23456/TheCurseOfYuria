using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ItemTypes
{
    [CreateAssetMenu(fileName = "Thunder", menuName = "ElementType/Thunder")]
    public class ThunderType : ElementBase
    {
        public override int weaknessIndex => 2;
    }
}