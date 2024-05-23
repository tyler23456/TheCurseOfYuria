using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ice", menuName = "ElementType/Ice")]
public class IceType : ElementTypeBase
{
    public override int weaknessIndex { get; protected set; } = 1;
}
