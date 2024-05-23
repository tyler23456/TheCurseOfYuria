using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Light", menuName = "ElementType/Light")]
public class LightType : ElementTypeBase
{
    public override int weaknessIndex { get; protected set; } = 3;
}
