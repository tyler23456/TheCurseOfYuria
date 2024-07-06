using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Light", menuName = "ElementType/Light")]
public class LightType : ElementBase
{
    public override int weaknessIndex => 3;
}
