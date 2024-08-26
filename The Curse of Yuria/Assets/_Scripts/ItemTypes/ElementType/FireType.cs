using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.ItemTypes
{
    [CreateAssetMenu(fileName = "Fire", menuName = "ElementType/Fire")]
    public class FireType : ElementBase
    {
        public override int weaknessIndex => 0;
    }
}